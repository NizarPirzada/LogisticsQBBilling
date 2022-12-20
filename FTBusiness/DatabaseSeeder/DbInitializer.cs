using FTCommon.Utils;
using FTData.Context;
using FTData.Model.Entities;
using FTEnum.ApplicationRoleEnum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FTBusiness.DatabaseSeeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly UserManager<LicenseUser> _userManager;
        private readonly RoleManager<LicenseUserRole> _roleManager;
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment _env;

        public DbInitializer(IServiceScopeFactory scopeFactory, RoleManager<LicenseUserRole> roleManager,  UserManager<LicenseUser> userManager, IConfiguration config, IHostingEnvironment env)
        {
            this._scopeFactory = scopeFactory;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.configuration = config;
            this._env = env;
        }

        public async Task Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SLCContext>())
                {
                    await context.Database.MigrateAsync();
                }


            }
        }
        public async Task SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {

                using (var context = serviceScope.ServiceProvider.GetService<SLCContext>())
                {
                    foreach (var _roleName in System.Enum.GetNames(typeof(ApplicationRoleEnum)))
                    {
                        await AddRole(_roleName, context);
                    }
                    await AddSuperadmin(context);
                    await context.SaveChangesAsync();
                }
            }
        }
        private async Task AddSuperadmin(SLCContext context)
        {

            string _UserName = UtilityHelper._config["SuperadminUser:UserName"];
            string _Password = UtilityHelper._config["SuperadminUser:Password"];
            string _Email = UtilityHelper._config["SuperadminUser:Email"];
            string _FirstName = UtilityHelper._config["SuperadminUser:FirstName"];
            string _LastName = UtilityHelper._config["SuperadminUser:LastName"];

            var user = new LicenseUser
            {
                FirstName = "Super",
                LastName = "Admin",
                UserRole = ApplicationRoleEnum.SuperAdmin,
                UserName = _UserName,
                NormalizedUserName = _UserName.ToUpper(),
                Email = _Email,
                NormalizedEmail = _Email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<LicenseUser>();
                var hashed = password.HashPassword(user, _Password);
                user.PasswordHash = hashed;
                await _userManager.CreateAsync(user);
                var roleName = ApplicationRoleEnum.SuperAdmin.ToString().ToUpper();
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
        private async Task AddRole(string roleName, LicenseDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                await _roleManager.CreateAsync(new LicenseUserRole { Name = roleName, NormalizedName = roleName.ToUpper() });
            }
        }
    }
}
