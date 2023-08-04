using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using UserManagementService.API.Application.Behaviours;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;
using UserManagementService.API.Infrastructure.Database;

namespace UserManagementService.API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            //ApplicationLevel
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //Infrastructure Level

            //Database Services
            string connectionString = configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }

    }
}
