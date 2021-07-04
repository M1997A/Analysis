using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analysis.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Analysis.Models.Repositories;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace Analysis
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientsRepository, ClientsRepository>();
            services.AddTransient<IResultsRepository, ResultsRepository>();
            services.AddTransient<IClientAnalysisRepository, ClientAnalysisRepository>();
            services.AddTransient<IAnalysisTypeRepository, AnalysisTypeRepository>();
            services.AddTransient<IAnalysisFeaturesRepository, AnalysisFeaturesRepository>();
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                
            }
                        
            
                        
            ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Home/Index");
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "x", template: "{controller}/{action}/{id?}/{*catchAll}");
            });
            RotativaConfiguration.Setup(env);
            AppDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
