using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Friends.AddFriend;
using SocialService.Friends.CheckFriendRequestStatus;
using SocialService.Friends.Common.Repository;
using SocialService.Friends.ListFriends;
using SocialService.Friends.ManageFriendRequests;
using SocialService.Friends.RemoveFriend;
using SocialService.Friends.RemoveFriendRequest;
using SocialService.Profile;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;

namespace SocialService.Friends;

/// <summary>
/// Modulo para resolver as dependências relacionadas a amizades
/// </summary>
public static class FriendsModule
{
    /// <summary>
    /// Método para resolver as dependências relacionadas a amizades
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFriendsRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();
        
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        #region Friend Request

        services.AddScoped<IHandler<bool, AddFriendCommand>, AddFriendCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>,
                CheckFriendRequestStatusQueryHandler>();
        services.AddScoped<IHandler<bool, RemoveFriendRequestCommand>, RemoveFriendRequestCommandHandler>();
        services.AddScoped<IHandler<bool, ManageFriendRequestsCommand>, ManageFriendRequestsCommandHandler>();

        #endregion
        
        #region Friendship
        
        services.AddScoped<IHandler<bool, RemoveFriendCommand>, RemoveFriendCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<ProfileBasicInformationViewModel>, ListFriendsQuery>, ListFriendsQueryHandler>();
        
        #endregion
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFriendshipGraphRepository, FriendshipGraphRepository>();
        
        return services;
    }
}