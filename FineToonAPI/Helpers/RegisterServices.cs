using FTBusiness.BaseRepository;
using FTBusiness.Logger;
using FTBusiness.ServiceManager.ServiceAction;
using FTBusiness.ServiceManager.ServiceInterface;
using FTBusiness.TokenAuth;
using FTCommon.Email;
using FTCommon.Helpers;
using FTCommon.Utils;
using FTData.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace FineToonAPI.Helpers
{
    public static class RegisterServices
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {


            #region Configure Package Services
            services.AddDbContext<SLCContext>(options => options.UseSqlServer(UtilityHelper._config["ConnectionStrings:DefaultConnection"]));

            //services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders();
            //services.AddSingleton<IUserStore<ApplicationUser>, ExampleUserStore>();
            //services.AddSingleton<IRoleStore<IdentityRole>, ExampleRoleStore>();

            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = true;
            //    options.Password.RequiredLength = 4;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireDigit = false;
            //});
            //.AddEntityFrameworkStores<SLCContext>()
            //.AddDefaultTokenProviders();

            //services.AddAuthorization(options =>
            //{
            //    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            //});

            //services.AddAuthentication().AddCookie().AddJwtBearer(config =>
            //{
            //    config.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidIssuer = JWTConfiguration.JWTIssuer,
            //        ValidAudience = JWTConfiguration.JWTAudience,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfiguration.JWTKey)),
            //        ClockSkew = TimeSpan.Zero,
            //        LifetimeValidator = TokenLifetimeValidator.Validate
            //    };
            //});

            // Without Authentication
            //services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            // With Authentication
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FineToonAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                //  c.OperationFilter<SwaggerCustomHeaderParameter>();
                c.CustomSchemaIds(type => type.ToString());
            });
            #endregion

            #region Configure Custom Services
            //services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<IEstimateRepo, EstimateRepo>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IDriverTypeRepo, DriverTypeRepo>();
            services.AddScoped<ITicketRepo, TicketRepo>();
            services.AddScoped<IJobRepo, JobRepo>();
            services.AddScoped<ITruckRepo, TruckRepo>();
            services.AddScoped<ICompanyRepo, CompanyRepo>();
            services.AddScoped<IReportRepo, ReportRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IApplicationUserRepo, ApplicationUserRepo>();
            services.AddScoped<UserSessionProfileService>();
            services.AddScoped<AdoRepository>();
            #endregion
        }
    }
}
