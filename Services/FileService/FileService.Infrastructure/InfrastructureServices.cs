using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileService.Application.Core.Interfaces;
using FileService.Infrastructure.Repositories;

namespace FileService.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
    {
        //Repositories
        services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));
        services.AddScoped<IUploadFile, UploadFileRepository>();
        services.AddScoped<IUserFile, UserFileRepository>();
        return services;
    }
}