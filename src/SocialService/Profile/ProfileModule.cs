using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.GetProfilePictures;
using SocialService.Profile.SearchProfileByName;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;
using SocialService.Profile.ViewProfileSimplified;

namespace SocialService.Profile;

/// <summary>
///     Modulo para resolver as dependências relacionadas a perfil
/// </summary>
public static class ProfileModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a perfil
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureProfileRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ProfileDto, CreateProfileCommand>, CreateProfileCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteProfileCommand>, DeleteProfileCommandHandler>();
        services.AddScoped<IHandler<ProfileDto, EditProfileCommand>, EditProfileCommandHandler>();
        services.AddScoped<IHandler<ProfileDto, UploadProfilePictureCommand>, UploadProfilePictureCommandHandler>();
        services.AddScoped<IHandler<ProfileDto?, ViewProfileQuery>, ViewProfileQueryHandler>();
        services.AddScoped<IHandler<ProfileSimplifiedDto, ViewProfileSimplifiedQuery>, ViewProfileSimplifiedQueryHandler>();
        services
            .AddScoped<IHandler<IEnumerable<ProfilePicture>, GetProfilePicturesQuery>,
                GetProfilePicturesQueryHandler>();
        services
            .AddScoped<IHandler<IEnumerable<ProfileSimplifiedDto>, SearchProfileByNameQuery>,
                SearchProfileByNameQueryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProfileGraphRepository, ProfileGraphRepository>();

        return services;
    }
}