using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernel.Enums;
using SharedKernel.ValueObjects;
using SharedKernel.Wrappers;
using AuthService.Protos;

namespace SharedKernel.Providers.Grpc;

public class GrpcProvider : IGrpcProvider
    {
        private readonly MicroserviceGrpcValueObject<AuthService.Protos.AuthService.AuthServiceClient> _authService;
        private readonly ILogger<GrpcProvider> _logger;

        public GrpcProvider(IConfiguration config, ILogger<GrpcProvider> logger)
        {
            string daprPort = config["DAPR_GRPC_PORT"]!;
            string authServiceAppId = config.GetSection("DaprConfig")["AUTH_SERVICE_APP_ID"]!;

            _authService = GrpcWrapper<AuthService.Protos.AuthService.AuthServiceClient>.GetClientAndMetadataByGprc(authServiceAppId,
                channel => new AuthService.Protos.AuthService.AuthServiceClient(channel), daprPort);

            _logger = logger;
        }

        public async Task<bool> CheckUserPermission(Guid objectId, EPermissionAction action, string theme, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation("CheckUserPermission: objectId: {objectId}, action: {action}, theme: {theme}", objectId, action, theme);
            
            var result = await _authService.Client.CheckPermissionAsync(new CheckPermissionRequest
            {
                Theme = theme,
                ObjectId = objectId.ToString(),
                Action = (int)action
            }, _authService.Metadata);

            return result.IsAllowed;
        }
    }