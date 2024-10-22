﻿using MongoDB.Driver;

namespace SocialService.Database.Mongo;

public class MongoContext<T> : IMongoContext<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoContext(string connectionString, string collectionName)
    {
        MongoClient client = new(connectionString);
        var database = client.GetDatabase("SocialService");
        _collection = database.GetCollection<T>(collectionName);
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
    
    public async Task UpdateDocumentAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        try
        {
            var result = await _collection.UpdateOneAsync(filter, update);
            if (result.ModifiedCount > 0)
            {
                Console.WriteLine("Documento atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhum documento foi atualizado.");
            }
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
            {
                Console.WriteLine("Documento apagado com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhum documento foi apagado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao apagar documento: {ex.Message}");
        }
    }
}