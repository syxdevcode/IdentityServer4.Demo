﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace JavaScript.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "JavaScript.Client";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5011")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
