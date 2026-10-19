using ReDevice.Business.DTOs;
using ReDevice.Business.Services.Interfaces;
using ReDevice.Database.Entities;
using ReDevice.Persistence.UnitOfWork;

namespace ReDevice.Business.Services;

public class DeviceService : IDeviceService
{
    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var devices = await _unitOfWork.Devices.GetAllAsync(cancellationToken);
        return devices.Select(MapToDto).ToList();
    }

    public async Task<DeviceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        return device is null ? null : MapToDto(device);
    }

    public async Task<DeviceDto> CreateAsync(CreateDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Devices.GetBySerialNumberAsync(request.SerialNumber, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException($"A device with serial number '{request.SerialNumber}' already exists.");
        }

        var device = new Device
        {
            Name = request.Name.Trim(),
            SerialNumber = request.SerialNumber.Trim(),
            Category = request.Category.Trim(),
            Condition = request.Condition.Trim(),
            Price = request.Price,
            IsAvailable = request.IsAvailable
        };

        await _unitOfWork.Devices.AddAsync(device, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(device);
    }

    public async Task<DeviceDto?> UpdateAsync(Guid id, UpdateDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        if (device is null)
        {
            return null;
        }

        device.Name = request.Name.Trim();
        device.Category = request.Category.Trim();
        device.Condition = request.Condition.Trim();
        device.Price = request.Price;
        device.IsAvailable = request.IsAvailable;

        _unitOfWork.Devices.Update(device);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(device);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        if (device is null)
        {
            return false;
        }

        _unitOfWork.Devices.Remove(device);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DeviceDto MapToDto(Device device) => new(
        device.Id,
        device.Name,
        device.SerialNumber,
        device.Category,
        device.Condition,
        device.Price,
        device.IsAvailable,
        device.CreatedAtUtc,
        device.UpdatedAtUtc);
}

