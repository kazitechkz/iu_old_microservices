using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicYearService.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
    {
        //Repositories
        services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));
        services.AddScoped<IAcademicYear, AcademicYearRepository>();
        services.AddScoped<ITerm, TermRepository>();
        return services;
    }
}