using Microsoft.Extensions.DependencyInjection;
using ReDevice.Business.Services;
using ReDevice.Business.Services.Interfaces;

namespace ReDevice.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IDeviceService, DeviceService>();

        return services;
    }
}

