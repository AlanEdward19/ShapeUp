namespace SocialService.Profile.UploadProfilePicture;

public class UploadProfilePictureCommand
{
    public MemoryStream Image { get; private set; }
    public string ImageFileName { get; private set; }
    
    public async Task SetImage(IFormFile image, string imageFileName, CancellationToken cancellationToken)
    {
        Image = new();

        await image.CopyToAsync(Image, cancellationToken);
        ImageFileName = imageFileName;
    }
}