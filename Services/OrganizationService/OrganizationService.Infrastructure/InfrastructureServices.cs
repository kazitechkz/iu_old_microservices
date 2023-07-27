using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Infrastructure.Repositories;

namespace OrganizationService.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
    {
        //Repositories
        services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));
        services.AddScoped<IArea, AreaRepository>();
        services.AddScoped<ILegalForm, LegalFormRepository>();
        services.AddScoped<ISchool, SchoolRepository>();
        return services;
    }
}