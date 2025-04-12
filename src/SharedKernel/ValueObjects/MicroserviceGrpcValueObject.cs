using Grpc.Core;

namespace SharedKernel.ValueObjects;

public record MicroserviceGrpcValueObject<T>
{
    public T Client { get; set; }
    public Metadata Metadata { get; set; }
}