using Gremlin.Net.Driver;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Database.Graph;
using SocialService.Database.Sql;
using SocialService.Follow.Common.Repository;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;
using SocialService.Friends.AddFriend;
using SocialService.Friends.CheckFriendRequestStatus;
using SocialService.Friends.Common.Repository;
using SocialService.Friends.ListFriends;
using SocialService.Friends.ManageFriendRequests;
using SocialService.Friends.RemoveFriend;
using SocialService.Friends.RemoveFriendRequest;
using SocialService.Post.Common.Repository;
using SocialService.Post.CreatePost;
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
        #region SQL Server

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
        });

        #endregion

        #region Graph
        
        services.AddSingleton<IDriver>(_ =>
        {
            var uri = configuration["Neo4j:Uri"];
            var user = configuration["Neo4j:User"];
            var password = configuration["Neo4j:Password"];
            return GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        });
        
        services.AddScoped(typeof(GraphContext));

        #region Repositories

        services.AddScoped<IProfileGraphRepository, ProfileGraphRepository>();
        services.AddScoped<IFriendshipGraphRepository, FriendshipGraphRepository>();
        services.AddScoped<IFollowerGraphRepository, FollowerGraphRepository>();
        services.AddScoped<IPostGraphRepository, PostGraphRepository>();

        #endregion

        #endregion

        #region Storage

        services.AddScoped<IStorageProvider>(provider =>
        {
            var connectionString = configuration.GetConnectionString("StorageConnection")!;

            return new StorageProvider(connectionString, provider.GetService<ILogger<StorageProvider>>()!);
        });

        #endregion

        #region Handlers

        #region Profile

        services.AddScoped<IHandler<ProfileAggregate, CreateProfileCommand>, CreateProfileCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteProfileCommand>, DeleteProfileCommandHandler>();
        services.AddScoped<IHandler<ProfileAggregate, EditProfileCommand>, EditProfileCommandHandler>();
        services.AddScoped<IHandler<bool, UploadProfilePictureCommand>, UploadProfilePictureCommandHandler>();
        services.AddScoped<IHandler<ProfileAggregate, ViewProfileQuery>, ViewProfileQueryHandler>();

        #endregion

        #region Friend

        services.AddScoped<IHandler<bool, AddFriendCommand>, AddFriendCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>,
                CheckFriendRequestStatusQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<ProfileBasicInformationViewModel>, ListFriendsQuery>, ListFriendsQueryHandler>();
        services.AddScoped<IHandler<bool, ManageFriendRequestsCommand>, ManageFriendRequestsCommandHandler>();
        services.AddScoped<IHandler<bool, RemoveFriendCommand>, RemoveFriendCommandHandler>();
        services.AddScoped<IHandler<bool, RemoveFriendRequestCommand>, RemoveFriendRequestCommandHandler>();
        
        #endregion

        #region Follow

        services.AddScoped<IHandler<bool, FollowUserCommand>, FollowUserCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowersQuery>, GetFollowersQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowingQuery>, GetFollowingQueryHandler>();
        services.AddScoped<IHandler<bool, UnfollowUserCommand>, UnfollowUserCommandHandler>();

        #endregion

        #region Post

        services.AddScoped<IHandler<Post.Post, CreatePostCommand>, CreatePostCommandHandler>();
        services.AddScoped<IHandler<bool, UploadPostImageCommand>, UploadPostImageCommandHandler>();

        #endregion

        #endregion

        return services;
    }
}