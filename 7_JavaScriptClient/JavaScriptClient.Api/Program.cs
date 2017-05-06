using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace JavaScriptClient.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "JavaScriptClient.Api";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5013")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
