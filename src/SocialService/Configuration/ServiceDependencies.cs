using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;
using SocialService.Friends;
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

        services.AddScoped<IMongoContext<Friend>>(provider =>
        {
            var connectionString = configuration.GetConnectionString("MongoConnection")!;

            return new MongoContext<Friend>(connectionString, "SocialService", "Friends",
                provider.GetService<ILogger<MongoContext<Friend>>>()!);
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

        #endregion

        return services;
    }
}