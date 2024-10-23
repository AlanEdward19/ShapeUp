using MongoDB.Driver;

namespace SocialService.Database.Mongo;

public class MongoContext<T> : IMongoContext<T>
{
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<MongoContext<T>> _logger;

    public MongoContext(string connectionString, string databaseName, string collectionName,
        ILogger<MongoContext<T>> logger)
    {
        MongoClient client = new(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);

        _logger = logger;
    }

    public async Task AddDocumentAsync(T document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
            Console.WriteLine("Documento adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar documento: {ex.Message}");
        }
    }


    public async Task<List<T>> GetDocumentsAsync()
    {
        try
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler documentos: {ex.Message}");
            return new List<T>();
        }
    }

    public async Task<T> GetDocumentAsync(FilterDefinition<T> filter)
    {
        try
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler documento: {ex.Message}");
            return default;
        }
    }

    public async Task UpdateDocumentAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        try
        {
            var result = await _collection.UpdateOneAsync(filter, update);
            if (result.ModifiedCount > 0)
                Console.WriteLine("Documento atualizado com sucesso!");
            else
                Console.WriteLine("Nenhum documento foi atualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar documento: {ex.Message}");
        }
    }

    public async Task DeleteDocumentAsync(FilterDefinition<T> filter)
    {
        try
        {
            var result = await _collection.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
                Console.WriteLine("Documento apagado com sucesso!");
            else
                Console.WriteLine("Nenhum documento foi apagado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao apagar documento: {ex.Message}");
        }
    }
}