using MongoDB.Driver;

namespace SocialService.Database.Mongo;

public interface IMongoContext<T>
{
    Task AddDocumentAsync(T document);
    Task<T> GetDocumentAsync(FilterDefinition<T> filter);
    Task<List<T>> GetDocumentsAsync();
    Task UpdateDocumentAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
    Task DeleteDocumentAsync(FilterDefinition<T> filter);
}