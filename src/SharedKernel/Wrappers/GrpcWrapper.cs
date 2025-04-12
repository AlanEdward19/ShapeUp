using Grpc.Core;
using Grpc.Net.Client;
using SharedKernel.ValueObjects;

namespace SharedKernel.Wrappers;

public static class GrpcWrapper<T>
{
    public static MicroserviceGrpcValueObject<T> GetClientAndMetadataByGprc(string microserviceId, Func<ChannelBase, T> createClient, string? daprGrpcPort = null)
    {
        string daprPort = daprGrpcPort ?? "3500";
        var channel = GrpcChannel.ForAddress($"http://localhost:{daprPort}");

        Metadata metadata = new()
        {
            {
                "dapr-app-id", microserviceId
            }
        };

        var client = createClient(channel);

        return new() { Client = client, Metadata = metadata };
    }
}