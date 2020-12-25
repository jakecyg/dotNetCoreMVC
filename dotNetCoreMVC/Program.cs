using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setting up host/server/request processing pipeline
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Kestrel server hosted as default/ integration with IIS applied.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specifying a startup class/provides the configuration of the app
                    webBuilder.UseStartup<Startup>();
                });
    }
    // When program starts:
    // 1. Main method executed
    // 2. StartClass called
    // 3. ConfigureServices method gets called to register all the services
    // 4. Configure method gets called to setup middleware
    // 5. Application is now ready to process client request
}
