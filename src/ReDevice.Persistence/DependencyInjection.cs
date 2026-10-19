using Microsoft.Extensions.DependencyInjection;
using ReDevice.Persistence.Repositories;
using ReDevice.Persistence.Repositories.Interfaces;
using ReDevice.Persistence.UnitOfWork;

namespace ReDevice.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}

