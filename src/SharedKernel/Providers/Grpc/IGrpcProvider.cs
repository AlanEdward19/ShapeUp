using SharedKernel.ValueObjects;

namespace SharedKernel.Providers.Grpc;

public interface IGrpcProvider
{
    Task<UserValueObject> VerifyToken(string token, CancellationToken cancellationToken);
}