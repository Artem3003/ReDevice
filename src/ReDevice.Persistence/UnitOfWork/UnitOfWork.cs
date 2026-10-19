using ReDevice.Database.Context;
using ReDevice.Persistence.Repositories.Interfaces;

namespace ReDevice.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, IDeviceRepository deviceRepository)
    {
        _context = context;
        Devices = deviceRepository;
    }

    public IDeviceRepository Devices { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}

