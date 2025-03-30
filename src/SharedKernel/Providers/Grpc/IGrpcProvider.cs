using SharedKernel.Enums;

namespace SharedKernel.Providers.Grpc;

public interface IGrpcProvider
{
    Task<bool> CheckUserPermission(Guid objectId, EPermissionAction action, string theme,
        CancellationToken cancellationToken);
}