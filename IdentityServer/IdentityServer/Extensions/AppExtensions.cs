using Duende.IdentityServer.Services;
using IdentityServer.DbContext;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddAppExtension(this IServiceCollection services, IConfiguration configuration)
        {
            //Database Services
            string connectionString = configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //Scoped
            services.AddScoped<IDbSeeder, DbSeeder>();
            services.AddScoped<IProfileService, ProfileService>();
            //Razor
            services.AddRazorPages();
            //AddIdentity
            services.AddLocalApiAuthentication();
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseErrorEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               .AddInMemoryIdentityResources(Configuration.GetIdentityResources)
               .AddInMemoryApiScopes(Configuration.GetApiScopes)
               .AddInMemoryClients(Configuration.GetClients)
               .AddAspNetIdentity<ApplicationUser>()
               .AddProfileService<ProfileService>()
               .AddDeveloperSigningCredential();
            return services;
        }



    }
}
