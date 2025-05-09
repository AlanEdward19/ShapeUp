namespace SocialService.Post.UploadPostImages;

/// <summary>
///     Comando para criação de post.
/// </summary>
public class UploadPostImageCommand
{
    /// <summary>
    ///     Id do post.
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Lista de imagens a serem enviadas.
    /// </summary>
    public List<(string imageName, MemoryStream fileContent)> Images { get; private set; }
    
    /// <summary>
    /// Lista de Ids de imagens a serem mantidas.
    /// </summary>
    public List<Guid> FilesToKeep { get; private set; }

    /// <summary>
    ///     Método para setar as imagens.
    /// </summary>
    /// <param name="images"></param>
    /// <param name="cancellationToken"></param>
    public async Task SetImages(IEnumerable<IFormFile> images, CancellationToken cancellationToken)
    {
        Images = new List<(string imageName, MemoryStream fileContent)>();

        foreach (var image in images)
        {
            var fileContent = new MemoryStream();
            await image.CopyToAsync(fileContent, cancellationToken);
            Images.Add((image.FileName, fileContent));
        }
    }

    /// <summary>
    ///     Método para setar o id do post.
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
    
    /// <summary>
    /// Método para setar os ids de imagens a serem mantidas.
    /// </summary>
    /// <param name="filesToKeep"></param>
    public void SetFilesToKeep(List<Guid> filesToKeep)
    {
        FilesToKeep = filesToKeep;
    }
}