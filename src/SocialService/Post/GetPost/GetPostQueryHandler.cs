using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPostInformations;

public class GetPostQueryHandler(IPostGraphRepository repository) : IHandler<Post, GetPostQuery>
{
    public async Task<Post> HandleAsync(GetPostQuery item, CancellationToken cancellationToken)
    {
        return await repository.GetPostAsync(item.PostId);
    }
}