using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmployeeDeactivation.Models;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.BusinessLayer;
using EmployeeDeactivation.Data;
using System.Threading;
using System.Timers;


namespace EmployeeDeactivation
{
    public class Startup
    {
        private IPdfDataOperation _pdfDataOperation;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<EmployeeDeactivationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EmployeeDeactivationContext")));
           
            services.AddScoped<IEmployeeDataOperation, EmployeeDataOperation>();
            services.AddScoped<IPdfDataOperation, PdfDataOperation>();

        }

        
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env , Employee employee )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            employee.SetTimer();

            app.UseMvc(
                routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Employees}/{action=Create}/{id?}");
            });
        }
    }
}
