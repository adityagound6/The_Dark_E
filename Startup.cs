using FoodRest.Models;
using FoodRest.Models.SqlRepositry;
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

namespace FoodRest
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration _config)
        {
            this._config = _config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContextPool<AppDbContext>(option =>
            option.UseSqlServer(_config.GetConnectionString("FoodieDBConnection")));
            services.AddScoped<IRepositry, SqlRepositry>();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc(route => route.MapRoute("Default", "{Controller=home}/{action=Index}/{id?}"));
        }
    }
}
