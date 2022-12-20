using FineToonAPI.Helpers;
using FTCommon.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace FineToonAPI
{
    public class Startup
    {
        readonly string _alloworigin = "alloworigin";
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            UtilityHelper._config = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("appSettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            IdentityModelEventSource.ShowPII = true;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidAudiences = new List<string>()
                {
                    "1fe322ff-bdbd-4050-8695-8998dc691160"
                }
            };

            services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, option =>
            {
                option.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            #region [@ENABLE CORS]
            services.AddCors(options =>
            {
                options.AddPolicy(_alloworigin,
                builder =>
                {
                    builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            #endregion

            services.AddMemoryCache();

            //services.AddSession(options => {
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //});

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".FineToon.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(5000);                
            //    options.Cookie.IsEssential = true;
            //});

            services.RegisterApplicationServices();

            //services.AddDbContext<SLCContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            services.AddAutoMapper((typeof(AutoMapperProfile)));

            //services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            */

            ServiceActivator.Configure(app.ApplicationServices);

            app.UseExceptionHandler("/api/Error/Error");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FineToonAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_alloworigin);

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseSession();

            //dbInitializer.Initialize().Wait();
            //dbInitializer.SeedData().Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
