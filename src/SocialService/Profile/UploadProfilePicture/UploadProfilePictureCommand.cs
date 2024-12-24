using System.Security.Cryptography;
using System.Text;

namespace SocialService.Profile.UploadProfilePicture;

/// <summary>
///     Comando para upload de foto de perfil.
/// </summary>
public class UploadProfilePictureCommand
{
    /// <summary>
    ///     Imagem a ser enviada.
    /// </summary>
    public MemoryStream Image { get; private set; } = new();

    /// <summary>
    ///     Nome do arquivo da imagem.
    /// </summary>
    public string ImageFileName { get; private set; } = "";

    public string ImageHash { get; private set; } = "";

    /// <summary>
    ///     Método para setar a imagem.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="imageFileName"></param>
    /// <param name="cancellationToken"></param>
    public async Task SetImage(IFormFile image, string imageFileName, CancellationToken cancellationToken)
    {
        Image = new MemoryStream();

        await image.CopyToAsync(Image, cancellationToken);
        ImageFileName = imageFileName;

        ImageHash = SetImageHash(Image);
    }

    private string SetImageHash(MemoryStream content)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(content.ToArray());
        StringBuilder hash = new StringBuilder();

        foreach (byte b in hashBytes)
            hash.Append(b.ToString("x2"));

        return hash.ToString();
    }
}