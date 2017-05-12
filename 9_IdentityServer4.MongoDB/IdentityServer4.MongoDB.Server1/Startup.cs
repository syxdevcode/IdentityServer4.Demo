using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.MongoDB.Server1.Quickstart.UI;
using IdentityServer4.Validation;
using Serilog.Events;
using Serilog;
using IdentityServer4.MongoDB.Interfaces;
using IdentityServer4.MongoDB.Server1.Configuration;
using IdentityServer4.MongoDB.Mappers;
using System.IO;
using IdentityServer4.MongoDB.Services;
using IdentityServer4.MongoDB.Validate;
using IdentityServer4.Services;
using IdentityServer4.MongoDB.Repository;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.MongoDB.Entities;

namespace IdentityServer4.MongoDB.Server1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //my user repository
            services.AddTransient<IPasswordHasher<MongoDBUser>, PasswordHasher<MongoDBUser>>();
            services.AddScoped<IRepository, MongoDbRepository>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()

                .AddSecretParser<ClientAssertionSecretParser>()
                .AddSecretValidator<PrivateKeyJwtSecretValidator>()

                .AddProfileService<ProfileService>()

                .AddConfigurationStore(Configuration.GetSection("MongoDB"))
                .AddOperationalStore(Configuration.GetSection("MongoDB"));
            
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // serilog filter
            Func<LogEvent, bool> serilogFilter = (e) =>
            {
                var context = e.Properties["SourceContext"].ToString();

                return (context.StartsWith("\"IdentityServer") ||
                        context.StartsWith("\"IdentityModel") ||
                        e.Level == LogEventLevel.Error ||
                        e.Level == LogEventLevel.Fatal);
            };

            var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Filter.ByIncludingOnly(serilogFilter)
                .WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}")
                .WriteTo.File(Directory.GetCurrentDirectory()+@"\logs\IdentityServer4.MongoDB.Host.txt")
                .CreateLogger();

            loggerFactory.AddSerilog(serilog);

            // Setup Databases
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                EnsureSeedData(serviceScope.ServiceProvider.GetService<IConfigurationDbContext>());
            }

            app.UseIdentityServer();
            app.UseIdentityServerMongoDBTokenCleanup(applicationLifetime);

            IdentityServerAuthenticationOptions identityServerValidationOptions = new IdentityServerAuthenticationOptions
            {
                //move host url into appsettings.json
                Authority = "http://localhost:5000/",
                ApiSecret = "secret",
                ApiName = "my.api.resource",
                AutomaticAuthenticate = true,
                SupportedTokens = SupportedTokens.Both,

                // required if you want to return a 403 and not a 401 for forbidden responses
                AutomaticChallenge = true,

                //change this to true for SLL
                RequireHttpsMetadata = false
            };

            app.UseIdentityServerAuthentication(identityServerValidationOptions);

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private static void EnsureSeedData(IConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Clients.Get().ToList())
                {
                    context.AddClient(client.ToEntity());
                }
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Resources.GetIdentityResources().ToList())
                {
                    context.AddIdentityResource(resource.ToEntity());
                }
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Resources.GetApiResources().ToList())
                {
                    context.AddApiResource(resource.ToEntity());
                }
            }

            if (!context.MongoDBUsers.Any())
            {
                foreach (var resource in TestUsers.Users)
                {
                    context.AddMongoDBUser(resource.ToEntity());
                }
            }
        }
    }
}
