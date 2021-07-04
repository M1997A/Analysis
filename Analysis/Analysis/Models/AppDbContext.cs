using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AnalysisType> AnalysisType { get; set; }
        public DbSet<AnalysisFeatures> AnalysisFeatures { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAnalysis> ClientAnalysis { get; set; }
        public DbSet<Results> Results { get; set; }


        public static async Task CreateAdminAccount(IServiceProvider provider, IConfiguration configuration)
        {
            UserManager<IdentityUser> userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            string UserName = configuration["Data:AdminUser:Name"];
            string Email = configuration["Data:AdminUser:Email"];
            string Password = configuration["Data:AdminUser:Password"];
            string Role = configuration["Data:AdminUser:Role"];

            if(await userManager.FindByNameAsync(UserName) == null)
            {
                if(await roleManager.FindByNameAsync(Role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(Role));
                }
                IdentityUser user = new IdentityUser
                {
                    UserName = UserName,
                    Email = Email
                };
                IdentityResult result = await userManager.CreateAsync(user,Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role);
                }
            }
        }
    }
   
}
