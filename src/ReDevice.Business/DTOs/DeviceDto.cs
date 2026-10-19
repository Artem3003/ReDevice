namespace ReDevice.Business.DTOs;

public record DeviceDto(
    Guid Id,
    string Name,
    string SerialNumber,
    string Category,
    string Condition,
    decimal Price,
    bool IsAvailable,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);

