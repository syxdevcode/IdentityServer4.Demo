using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using IdentityServer4.MongoDB.Server.Extensions;
using IdentityServer4.MongoDB.Server.Services;
using IdentityServer4.Stores;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.MongoDB.Server.Models;
using IdentityServer4.MongoDB.Server.UI;

namespace IdentityServer4.MongoDB.Server
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
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            // 证书
            var cert = new X509Certificate2(Path.Combine(env.ContentRootPath, "idsrv4test.pfx"), "idsrv3test");

            var builder = services.AddIdentityServer();
            //.AddSigningCredential(cert)
            builder.Services.AddTransient<IRepository, MongoDbRepository>();
            builder.Services.AddTransient<IClientStore, MongoDbClientStore>();
            builder.Services.AddTransient<IProfileService, MongoDbProfileService>();
            builder.Services.AddTransient<IResourceOwnerPasswordValidator, MongoDbResourceOwnerPasswordValidator>();
            builder.Services.AddTransient<IPasswordHasher<MongoDbUser>, PasswordHasher<MongoDbUser>>();
            builder.Services.Configure<MongoDbRepositoryConfiguration>(Configuration.GetSection("MongoDbRepository"));

            builder.AddExtensionGrantValidator<CustomGrantValidator>();

            // for the UI
            services
                .AddMvc()
                .AddRazorOptions(razor =>
                {
                    razor.ViewLocationExpanders.Add(new CustomViewLocationExpander());
                });

            services.AddTransient<LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
