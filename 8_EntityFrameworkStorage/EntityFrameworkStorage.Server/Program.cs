﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace EntityFrameworkStorage.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "EntityFrameworkStorage.Server";

            var host = new WebHostBuilder()
                .UseUrls("http://localhost:5000")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
