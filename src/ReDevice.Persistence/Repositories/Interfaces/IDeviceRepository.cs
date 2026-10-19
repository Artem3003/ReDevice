using ReDevice.Database.Entities;

namespace ReDevice.Persistence.Repositories.Interfaces;

public interface IDeviceRepository : IRepository<Device>
{
    Task<Device?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Device>> GetAvailableDevicesAsync(CancellationToken cancellationToken = default);
}

