using SocialService.Common.Interfaces;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Repository;
using SocialService.Post.CreatePost;
using SocialService.Post.DeleteCommentOnPost;
using SocialService.Post.DeletePost;
using SocialService.Post.DeleteReactionFromPost;
using SocialService.Post.EditCommentOnPost;
using SocialService.Post.EditPost;
using SocialService.Post.GetPost;
using SocialService.Post.GetPostComments;
using SocialService.Post.GetReactionsOnPost;
using SocialService.Post.ReactToPost;
using SocialService.Post.UploadPostImages;

namespace SocialService.Post;

/// <summary>
///     Modulo para resolver as dependências relacionadas a postagens
/// </summary>
public static class PostModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a postagens
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigurePostRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        #region Post

        services.AddScoped<IHandler<PostDto, GetPostQuery>, GetPostQueryHandler>();
        services.AddScoped<IHandler<PostDto, CreatePostCommand>, CreatePostCommandHandler>();
        services.AddScoped<IHandler<bool, UploadPostImageCommand>, UploadPostImageCommandHandler>();
        services.AddScoped<IHandler<bool, DeletePostCommand>, DeletePostCommandHandler>();
        services.AddScoped<IHandler<PostDto, EditPostCommand>, EditPostCommandHandler>();

        #endregion

        #region Comment

        services.AddScoped<IHandler<bool, CommentOnPostCommand>, CommentOnPostCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<Comment>, GetPostCommentsQuery>, GetPostCommentsQueryHandler>();
        services.AddScoped<IHandler<bool, EditCommentOnPostCommand>, EditCommentOnPostCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteCommentOnPostCommand>, DeleteCommentOnPostCommandHandler>();

        #endregion

        #region Reaction

        services.AddScoped<IHandler<bool, ReactToPostCommand>, ReactToPostCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<Reaction>, GetReactionsOnPostQuery>, GetReactionsOnPostQueryHandler>();
        services.AddScoped<IHandler<bool, DeleteReactionFromPostCommand>, DeleteReactionFromPostCommandHandler>();

        #endregion

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostGraphRepository, PostGraphRepository>();

        return services;
    }
}