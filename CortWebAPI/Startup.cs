using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using CortWebAPI.Services;
using CortWebAPI.Exceptions;
using CortWebAPI.Services.CortWebAPI.Services;

namespace CortWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            try
            {
                services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader());
                });

                services.AddTransient<IBballService, BballService>();
                services.AddTransient<IUserService, UserService>();

                services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json.ReferenceLoopHandling.Ignore)
                    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                    ;

                services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            }
            catch (Exception e)
            {

            }

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            try
            {
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                if (env.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();

                }

                app.UseRouting();

                // app.UseAuthentication();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/hello/{name:alpha}", async context =>
                    {
                        var name = context.Request.RouteValues["name"];
                        await context.Response.WriteAsync($"Hello {name}!");
                    });
                    endpoints.MapControllers();
                });
            } catch (Exception e)
            {

            }
            
        }
    }
}
