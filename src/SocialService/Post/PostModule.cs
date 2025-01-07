using SocialService.Post.Comment.CommentOnPost;
using SocialService.Post.Comment.DeleteCommentOnPost;
using SocialService.Post.Comment.EditCommentOnPost;
using SocialService.Post.Comment.GetPostComments;
using SocialService.Post.Common.Repository;
using SocialService.Post.CreatePost;
using SocialService.Post.DeletePost;
using SocialService.Post.EditPost;
using SocialService.Post.GetPost;
using SocialService.Post.GetPostsByProfileId;
using SocialService.Post.React;
using SocialService.Post.React.DeleteReactionFromPost;
using SocialService.Post.React.GetReactionsOnPost;
using SocialService.Post.React.ReactToPost;
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
        services.AddScoped<IHandler<IEnumerable<PostDto>, GetPostsByProfileIdQuery>, GetPostsByProfileIdQueryHandler>();
        services.AddScoped<IHandler<PostDto, CreatePostCommand>, CreatePostCommandHandler>();
        services.AddScoped<IHandler<bool, UploadPostImageCommand>, UploadPostImageCommandHandler>();
        services.AddScoped<IHandler<bool, DeletePostCommand>, DeletePostCommandHandler>();
        services.AddScoped<IHandler<PostDto, EditPostCommand>, EditPostCommandHandler>();

        #endregion

        #region Comment

        services.AddScoped<IHandler<bool, CommentOnPostCommand>, CommentOnPostCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<Comment.Comment>, GetPostCommentsQuery>, GetPostCommentsQueryHandler>();
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