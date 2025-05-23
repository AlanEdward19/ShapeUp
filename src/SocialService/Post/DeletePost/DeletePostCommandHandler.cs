﻿using SharedKernel.Exceptions;
using SharedKernel.Utils;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.DeletePost;

/// <summary>
///     Handler para o comando de deletar um post.
/// </summary>
/// <param name="repository"></param>
public class DeletePostCommandHandler(IPostGraphRepository repository, IBlobStorageProvider blobStorageProvider)
    : IHandler<bool, DeletePostCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de deletar um post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(DeletePostCommand command, CancellationToken cancellationToken)
    {
        if (!await repository.PostExistsAsync(command.PostId))
            throw new NotFoundException($"Post with id: '{command.PostId}' not found.");

        await repository.DeletePostAsync(command.PostId);

        var containerName = ProfileContext.ProfileId.ToString();
        var blobName = $"post-images/{command.PostId}";
        await blobStorageProvider.DeleteFolderAsync(blobName, containerName);

        return true;
    }
}