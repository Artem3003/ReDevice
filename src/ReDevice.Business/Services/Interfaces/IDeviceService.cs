using ReDevice.Business.DTOs;

namespace ReDevice.Business.Services.Interfaces;

public interface IDeviceService
{
    Task<IReadOnlyList<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DeviceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DeviceDto> CreateAsync(CreateDeviceRequest request, CancellationToken cancellationToken = default);
    Task<DeviceDto?> UpdateAsync(Guid id, UpdateDeviceRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

