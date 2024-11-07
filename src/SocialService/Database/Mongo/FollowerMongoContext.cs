﻿using MongoDB.Driver;
using SocialService.Database.Mongo.Contracts;
using SocialService.Follow;
using SocialService.Friends;

namespace SocialService.Database.Mongo;

/// <summary>
/// Contexto para operações de banco de dados relacionadas a seguidores.
/// </summary>
public class FollowerMongoContext : IFollowerMongoContext
{
    private readonly IMongoCollection<ProfileConnections> _collection;
    private readonly ILogger<FollowerMongoContext> _logger;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="databaseName"></param>
    /// <param name="collectionName"></param>
    /// <param name="logger"></param>
    public FollowerMongoContext(string connectionString, string databaseName, string collectionName, ILogger<FollowerMongoContext> logger)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<ProfileConnections>(collectionName);
        _logger = logger;
    }

    #region Profile

    /// <summary>
    /// Método para adicionar um documento de perfil.
    /// </summary>
    /// <param name="document"></param>
    public async Task AddProfileDocumentAsync(ProfileConnections document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding profile document.");
            throw;
        }
    }

    /// <summary>
    /// Método para obter um documento de perfil por Id.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<ProfileConnections> GetProfileDocumentByIdAsync(Guid profileId)
    {
        try
        {
            var filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, profileId.ToString());
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting profile document by ID.");
            return default!;
        }
    }

    /// <summary>
    /// Método para deletar um documento de perfil por Id.
    /// </summary>
    /// <param name="objectId"></param>
    public async Task DeleteProfileDocumentByIdAsync(Guid objectId)
    {
        try
        {
            var filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, objectId.ToString());
            var result = await _collection.DeleteOneAsync(filter);
            _logger.LogInformation(result.DeletedCount > 0 ? "Document deleted successfully." : "No document was deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting profile document.");
        }
    }

    #endregion

    #region Follower

    /// <summary>
    /// Método para seguir um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="followId"></param>
    public async Task FollowProfileAsync(Guid userId, Guid followId)
    {
        try
        {
            var filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, userId.ToString());
            var update = Builders<ProfileConnections>.Update.Push(x => x.Following, followId.ToString());
            await _collection.UpdateOneAsync(filter, update);
            
            filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, followId.ToString());
            update = Builders<ProfileConnections>.Update.Push(x => x.Followers, userId.ToString());
            await _collection.UpdateOneAsync(filter, update);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error following profile.");
            throw;
        }
    }
    
    /// <summary>
    /// Método para deixar de seguir um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="followId"></param>
    public async Task UnfollowProfileAsync(Guid userId, Guid followId)
    {
        try
        {
            var filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, userId.ToString());
            var update = Builders<ProfileConnections>.Update.Pull(x => x.Following, followId.ToString());
            await _collection.UpdateOneAsync(filter, update);
            
            filter = Builders<ProfileConnections>.Filter.Eq(x => x.ProfileId, followId.ToString());
            update = Builders<ProfileConnections>.Update.Pull(x => x.Followers, userId.ToString());
            await _collection.UpdateOneAsync(filter, update);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unfollowing profile.");
            throw;
        }
    }

    #endregion
}