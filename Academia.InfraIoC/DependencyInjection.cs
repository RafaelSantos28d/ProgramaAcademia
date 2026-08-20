
using Academia.Application.Interfaces;
using Academia.Application.Mappings;
using Academia.Application.Services;
using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using Academia.Infrastructure.Context;
using Academia.Infrastructure.IdentityService;
using Academia.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Academia.InfraIoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AplicationUser,IdentityRole>().
                AddEntityFrameworkStores<BancoContext>().
                AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;//em produção é true
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"])),
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Name

                };

            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Employee"));
            });

            services.AddDbContext<BancoContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")),

                b => b.MigrationsAssembly(typeof(BancoContext).Assembly.FullName)
                ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(
                cfg => { },
                typeof(DomainMappingProfile).Assembly
            );
            services.AddScoped<IStudentRepository,StudentRepository>();
            services.AddScoped<IStudentService,StudentService>();

            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IIdentityservice,IdentityService>();
            services.AddScoped<ITokenService,TokenService>();

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                ReferenceHandler.IgnoreCycles;
            });
            return services;
        }
    }
}
