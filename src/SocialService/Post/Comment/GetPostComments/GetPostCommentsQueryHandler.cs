using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.GetPostComments;

/// <summary>
///     Handler para obter os comentários de um post
/// </summary>
/// <param name="repository"></param>
public class GetPostCommentsQueryHandler(IPostGraphRepository repository, IBlobStorageProvider blobStorageProvider)
    : IHandler<IEnumerable<Comment>, GetPostCommentsQuery>
{
    /// <summary>
    ///     Método para obter os comentários de um post
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Comment>> HandleAsync(GetPostCommentsQuery query, CancellationToken cancellationToken)
    {
        var comments = await repository.GetPostCommentsByPostIdAsync(query.PostId);

        foreach (var comment in comments)
            if (!string.IsNullOrWhiteSpace(comment.ProfileImageUrl))
                comment.SetProfileImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(comment.ProfileImageUrl, $"{comment.ProfileId}"));
        
        return comments;
    }
}