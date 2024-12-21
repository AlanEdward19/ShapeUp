using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Friends.Common.Repository;
using SocialService.Friends.FriendRequest.CheckFriendRequestStatus;
using SocialService.Friends.FriendRequest.ManageFriendRequests;
using SocialService.Friends.FriendRequest.RemoveFriendRequest;
using SocialService.Friends.FriendRequest.SendFriendRequest;
using SocialService.Friends.Friendship.ListFriends;
using SocialService.Friends.Friendship.RemoveFriend;

namespace SocialService.Friends;

/// <summary>
///     Modulo para resolver as dependências relacionadas a amizades
/// </summary>
public static class FriendsModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a amizades
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

        services.AddScoped<IHandler<bool, SendFriendRequestCommand>, SendFriendRequestCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>,
                CheckFriendRequestStatusQueryHandler>();
        services.AddScoped<IHandler<bool, RemoveFriendRequestCommand>, RemoveFriendRequestCommandHandler>();
        services.AddScoped<IHandler<bool, ManageFriendRequestsCommand>, ManageFriendRequestsCommandHandler>();

        #endregion

        #region Friendship

        services.AddScoped<IHandler<bool, RemoveFriendCommand>, RemoveFriendCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<ProfileBasicInformation>, ListFriendsQuery>,
                ListFriendsQueryHandler>();

        #endregion

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFriendshipGraphRepository, FriendshipGraphRepository>();

        return services;
    }
}