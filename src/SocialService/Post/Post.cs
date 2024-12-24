﻿using SocialService.Post.Common.Enums;
using SocialService.Post.CreatePost;

namespace SocialService.Post;

/// <summary>
///     Classe que representa um post.
/// </summary>
public class Post : GraphEntity
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public Post()
    {
    }

    /// <summary>
    ///     Construtor para criar um novo post.
    /// </summary>
    /// <param name="command"></param>
    public Post(CreatePostCommand command)
    {
        Id = Guid.NewGuid();
        UpdateVisibility(command.Visibility);
        UpdateContent(command.Content);
        Images = new List<Guid>();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility Visibility { get; private set; }

    /// <summary>
    ///     Data de criação do post.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Data de atualização do post.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    ///     Ids das imagens do post
    /// </summary>
    public IEnumerable<Guid> Images { get; private set; }

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para a entidade.
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Visibility = (EPostVisibility)Enum.Parse(typeof(EPostVisibility), result["visibility"].ToString()!);
        CreatedAt = DateTime.Parse(result["createdAt"].ToString()!);
        UpdatedAt = DateTime.Parse(result["updatedAt"].ToString()!);
        Content = result["content"].ToString()!;

        if (result.ContainsKey("images"))
            Images = result["images"] == null
                ? new List<Guid>()
                : ((List<object>)result["images"]).Select(id => Guid.Parse(id.ToString()!)).ToList();

        else
            Images = new List<Guid>();

        base.MapToEntityFromNeo4j(result);
    }

    /// <summary>
    ///     Método para atualizar a visibilidade do post.
    /// </summary>
    /// <param name="visibility"></param>
    /// <param name="isUpdate"></param>
    public void UpdateVisibility(EPostVisibility? visibility, bool isUpdate = true)
    {
        if (visibility == null)
            return;

        if (!Enum.IsDefined(typeof(EPostVisibility), visibility))
            throw new ArgumentException("Valor de visibilidade inválido.");

        if (Visibility != visibility)
            Visibility = visibility.Value;

        if (isUpdate)
            UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Método para atualizar o conteúdo do post.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="isUpdate"></param>
    public void UpdateContent(string? content, bool isUpdate = true)
    {
        if (string.IsNullOrWhiteSpace(content))
            return;

        if (Content != content)
            Content = content;

        if (isUpdate)
            UpdatedAt = DateTime.UtcNow;
    }
}