using Microsoft.EntityFrameworkCore;
using ReDevice.Database.Context;
using ReDevice.Database.Entities;
using ReDevice.Persistence.Repositories.Interfaces;

namespace ReDevice.Persistence.Repositories;

public class DeviceRepository : Repository<Device>, IDeviceRepository
{
    public DeviceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Device?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.SerialNumber == serialNumber, cancellationToken);
    }

    public async Task<IReadOnlyList<Device>> GetAvailableDevicesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(d => d.IsAvailable)
            .OrderByDescending(d => d.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}

