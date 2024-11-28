using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPostComments;

public class GetPostCommentsQueryHandler(IPostGraphRepository repository) : IHandler<IEnumerable<Comment>, GetPostCommentsQuery>
{
    public async Task<IEnumerable<Comment>> HandleAsync(GetPostCommentsQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetPostCommentsAsync(query.PostId);
    }
}