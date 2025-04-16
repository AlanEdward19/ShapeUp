using SharedKernel.Dtos;

namespace SharedKernel.Providers;

public interface IGrpcProvider
{
    Task<bool> SendNotification(NotificationDto notification, CancellationToken cancellationToken);
}