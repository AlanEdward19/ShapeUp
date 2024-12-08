﻿namespace SocialService.Post.DeleteCommentOnPost;

public class DeleteCommentOnPostCommand
{
    /// <summary>
    /// Id do comentário
    /// </summary>
    public Guid CommentId { get; private set; }
    
    /// <summary>
    /// Método para setar o id do comentário
    /// </summary>
    /// <param name="commentId"></param>
    public void SetCommentId(Guid commentId) => CommentId = commentId;
}