using SharedKernel.Exceptions;
using SharedKernel.Utils;
using SocialService.Common.Services.CepAwesomeApi;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.UploadProfilePicture;

/// <summary>
///     Handler para o comando de upload de foto de perfil.
/// </summary>
/// <param name="repository"></param>
/// <param name="blobStorageProvider"></param>
/// <param name="cepAwesomeApi"></param>
public class UploadProfilePictureCommandHandler(IProfileGraphRepository repository, IBlobStorageProvider blobStorageProvider, ICepAwesomeApi cepAwesomeApi)
    : IHandler<ProfileDto, UploadProfilePictureCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de upload de foto de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ProfileDto> HandleAsync(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {
        Profile? profile = await repository.GetProfileAsync(ProfileContext.ProfileId);
        
        if (profile is null)
            throw new NotFoundException("Profile not found");

        var containerName = $"{profile.Id}";

        var blobName = blobStorageProvider.SanitizeName(
            $"profile-pictures/{DateTime.Today:yyyy-MM-dd}/{command.ImageHash}.{command.ImageFileName.Split('.').Last()}",
            true);

        await blobStorageProvider.WriteBlobAsync(command.Image, blobName, containerName);

        profile.UpdateImage(blobName);

        await repository.UpdateProfileAsync(profile);
        
        string state = "", city = "";
        try
        {
            var locationInfo = await cepAwesomeApi.GetLocationInfoByPostalCodeAsync(profile.PostalCode);
            city = locationInfo.City;
            state = locationInfo.State;
        }
        catch
        {
        }

        ProfileDto profileDto = new(profile, state, city);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            profileDto.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}"));

        return profileDto;
    }
}