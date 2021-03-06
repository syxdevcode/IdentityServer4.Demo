﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace EntityFrameworkStorage.MvcClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "EntityFrameworkStorage.MvcClient";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5016")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
