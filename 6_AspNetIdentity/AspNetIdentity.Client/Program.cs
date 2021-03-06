﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace AspNetIdentity.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "AspNetIdentity.Client";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5010")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
