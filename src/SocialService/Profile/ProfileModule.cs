using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;

namespace SocialService.Profile;

/// <summary>
/// Modulo para resolver as dependências relacionadas a perfil
/// </summary>
public static class ProfileModule
{
    /// <summary>
    /// Método para resolver as dependências relacionadas a perfil
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
        services.AddScoped<IHandler<ProfileAggregate, CreateProfileCommand>, CreateProfileCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteProfileCommand>, DeleteProfileCommandHandler>();
        services.AddScoped<IHandler<ProfileAggregate, EditProfileCommand>, EditProfileCommandHandler>();
        services.AddScoped<IHandler<bool, UploadProfilePictureCommand>, UploadProfilePictureCommandHandler>();
        services.AddScoped<IHandler<ProfileAggregate, ViewProfileQuery>, ViewProfileQueryHandler>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProfileGraphRepository, ProfileGraphRepository>();
        
        return services;
    }
}