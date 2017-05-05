using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace AspNetIdentity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "AspNetIdentity.Api";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5009")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
