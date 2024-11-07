using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;
using SocialService.Friends;
using SocialService.Friends.AddFriend;
using SocialService.Friends.CheckFriendRequestStatus;
using SocialService.Friends.ListFriends;
using SocialService.Friends.ManageFriendRequests;
using SocialService.Friends.RemoveFriend;
using SocialService.Friends.RemoveFriendRequest;
using SocialService.Profile;
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

        #region Mongo Db

        string mongoConnectionString = configuration.GetConnectionString("MongoConnection")!;
        
        services.AddScoped<IFriendMongoContext>(provider =>
        {
            return new FriendMongoContext(mongoConnectionString, "SocialService", "Friends",
                provider.GetService<ILogger<FriendMongoContext>>()!);
        });
        
        services.AddScoped<IFollowerMongoContext>(provider =>
        {
            return new FollowerMongoContext(mongoConnectionString, "SocialService", "Followers",
                provider.GetService<ILogger<FollowerMongoContext>>()!);
        });

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

        #endregion

        return services;
    }
}