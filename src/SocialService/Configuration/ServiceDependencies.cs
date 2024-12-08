using Gremlin.Net.Driver;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Connections;
using SocialService.Database.Graph;
using SocialService.Database.Sql;
using SocialService.Follow;
using SocialService.Follow.Common.Repository;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;
using SocialService.Friends;
using SocialService.Friends.AddFriend;
using SocialService.Friends.CheckFriendRequestStatus;
using SocialService.Friends.Common.Repository;
using SocialService.Friends.ListFriends;
using SocialService.Friends.ManageFriendRequests;
using SocialService.Friends.RemoveFriend;
using SocialService.Friends.RemoveFriendRequest;
using SocialService.Post;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Repository;
using SocialService.Post.CreatePost;
using SocialService.Post.DeletePost;
using SocialService.Post.EditPost;
using SocialService.Post.GetPostComments;
using SocialService.Post.LikePost;
using SocialService.Post.UploadPostImages;
using SocialService.Profile;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;
using SocialService.Storage;

namespace SocialService.Configuration;

/// <summary>
/// Classe para resolver as dependências de serviços
/// </summary>
public static class ServiceDependencies
{
    /// <summary>
    ///     Método para resolver as dependências de serviços
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection SolveServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureConnections(configuration)
            .ConfigureProfileRelatedDependencies()
            .ConfigureFollowRelatedDependencies()
            .ConfigureFriendsRelatedDependencies()
            .ConfigurePostRelatedDependencies();

        return services;
    }
}