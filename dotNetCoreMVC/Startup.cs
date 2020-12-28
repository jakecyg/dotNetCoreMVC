using dotNetCoreMVC.Common.Contexts;
using dotNetCoreMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Register services here through dependency injection

            // Setup connectionstring from appsetting.json file; avaiilable through injected IConfiguration
            services.AddDbContext<dbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // Support for MVC framework
            services.AddControllersWithViews();
            services.AddScoped<IPieRepo, PieRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            //services.AddSingleton create a single instance for the entire app and reuse that single instace
            //services.AddTransient create a new instance everytime you ask for you
            //services.AddScoped create new instance PER REQUEST- and the isntance remains active throughout the request(a singleton per request)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Setting up middlewares
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Redirects HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable app to serve static files(js, css, etc.)
            // Default to serve files in wwwroot folder
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
