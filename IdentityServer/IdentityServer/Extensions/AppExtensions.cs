using IdentityServer.DbContext;
using IdentityServer.Models;
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

            //Razor
            services.AddRazorPages();
            //AddIdentity
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
               .AddDeveloperSigningCredential();


            
                
               


            return services;
        }



    }
}
