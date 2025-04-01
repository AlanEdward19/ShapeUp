using System.Security.Claims;
using SharedKernel.Enums;
using SharedKernel.ValueObjects;

namespace SharedKernel.Providers.Grpc;

public interface IGrpcProvider
{
    Task<bool> CheckUserPermission(Guid objectId, EPermissionAction action, string theme,
        CancellationToken cancellationToken);

    Task<UserValueObject> VerifyToken(string token, CancellationToken cancellationToken);
}