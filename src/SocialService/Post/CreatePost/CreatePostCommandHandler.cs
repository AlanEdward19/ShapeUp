﻿using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.CreatePost;

/// <summary>
/// Handler para criação de post.
/// </summary>
/// <param name="repository"></param>
public class CreatePostCommandHandler(IPostGraphRepository repository)
    : IHandler<Post, CreatePostCommand>
{
    /// <summary>
    /// Método para criação de post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Post> HandleAsync(CreatePostCommand command, CancellationToken cancellationToken)
    {
        Guid postId = Guid.NewGuid();

        await repository.CreatePostAsync(ProfileContext.ProfileId, postId, command);

        return new(postId);
    }
}