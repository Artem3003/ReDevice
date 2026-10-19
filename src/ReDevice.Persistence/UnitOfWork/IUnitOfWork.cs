using ReDevice.Persistence.Repositories.Interfaces;

namespace ReDevice.Persistence.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IDeviceRepository Devices { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

