using AspNetCoreIdentity.Config;
using AspNetCoreIdentity.Data;
using AspNetCoreIdentity.Extension;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace AspNetCoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostingEnvironment)
        {
            //usado para pegar arquivo json conforme ambiente executado
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json",true,true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json",true,true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityConfig(Configuration);

            services.AddAuthorizationConfig();

            services.ResolveDependecies();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseKissLogMiddleware(options => {
                LogConfig.ConfigureKissLog(options, Configuration);
            });



            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();

            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
