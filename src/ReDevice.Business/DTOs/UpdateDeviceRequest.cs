using System.ComponentModel.DataAnnotations;

namespace ReDevice.Business.DTOs;

public record UpdateDeviceRequest(
    [Required, MaxLength(200)] string Name,
    [Required, MaxLength(100)] string Category,
    [Required, MaxLength(50)] string Condition,
    [Range(0, double.MaxValue)] decimal Price,
    bool IsAvailable);

