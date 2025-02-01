using System.Text.RegularExpressions;
using SocialService.Common.Services.BrasilApi;
using SocialService.Profile.Common.Enums;
using SocialService.Profile.CreateProfile;

namespace SocialService.Profile;

/// <summary>
///     Classe que representa o perfil do usuário.
/// </summary>
public class Profile : GraphEntity
{
    /// <summary>
    ///     Construtor padrão.
    /// </summary>
    public Profile()
    {
    }

    /// <summary>
    ///     Construtor para criação de um novo perfil através de um comando.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    /// <param name="locationInfo"></param>
    public Profile(CreateProfileCommand command, Guid id, LocationInfoDto locationInfo)
    {
        Id = id;
        UpdateFirstName(command.FirstName, false);
        UpdateLastName(command.LastName, false);
        UpdateDisplayName(command.DisplayName, false);
        UpdateCountry(command.Country, false);
        UpdatePostalCode(command.PostalCode, false);
        UpdateGender(command.Gender, false);
        UpdateBirthDate(command.BirthDate, false);
        UpdateBio(command.Bio, false);
        UpdateEmail(command.Email, false);
        UpdateLatitude(double.Parse(locationInfo.Location.Coordinates.Latitude), false);
        UpdateLongitude(double.Parse(locationInfo.Location.Coordinates.Longitude), false);
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Email do perfil.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    ///     Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    ///     Sobrenome do perfil.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Pais do perfil.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    ///     CEP do perfil.
    /// </summary>
    public string PostalCode { get; private set; }
    
    /// <summary>
    /// Latitude do perfil.
    /// </summary>
    public double Latitude { get; private set; }
    
    /// <summary>
    ///    Longitude do perfil.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    ///     Nome de exibição do perfil.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Quantidade de seguidores do perfil.
    /// </summary>
    public int Followers { get; private set; }

    /// <summary>
    /// Quantidade de pessoas que o perfil segue.
    /// </summary>
    public int Following { get; private set; }

    /// <summary>
    /// Quantidade de posts do perfil.
    /// </summary>
    public int Posts { get; private set; }

    /// <summary>
    ///     Url da imagem do perfil no blob storage
    /// </summary>
    public string? ImageUrl { get; private set; }

    /// <summary>
    ///     Biografia do perfil.
    /// </summary>
    public string? Bio { get; private set; }

    /// <summary>
    ///     Data de nascimento do perfil.
    /// </summary>
    public DateTime? BirthDate { get; private set; }

    /// <summary>
    ///     Gênero do perfil.
    /// </summary>
    public EGender? Gender { get; private set; }

    /// <summary>
    ///     Data de criação do perfil.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Data de atualização do perfil.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para o perfil
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Email = result["email"].ToString();
        FirstName = result["firstName"].ToString();
        LastName = result["lastName"].ToString();
        Gender = string.IsNullOrWhiteSpace(result["gender"].ToString())
            ? null
            : Enum.Parse<EGender>(result["gender"].ToString()!);
        DisplayName = result["displayName"].ToString();
        Country = result["country"].ToString();
        PostalCode = result["postalCode"].ToString();
        ImageUrl = result["imageUrl"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        UpdatedAt = DateTime.Parse(result["updatedAt"].ToString());
        BirthDate = string.IsNullOrWhiteSpace(result["birthDate"].ToString())
            ? null
            : DateTime.Parse(result["birthDate"].ToString());
        Bio = result["bio"].ToString();
        Latitude = double.Parse(result["latitude"].ToString());
        Longitude = double.Parse(result["longitude"].ToString());
        Following = result.TryGetValue("following", out var followingCount) ? int.Parse(followingCount.ToString()!) : 0;
        Followers = result.TryGetValue("followers", out var followerCount) ? int.Parse(followerCount.ToString()!) : 0;
        Posts = result.TryGetValue("posts", out var postCount) ? int.Parse(postCount.ToString()!) : 0;
        
        base.MapToEntityFromNeo4j(result);
    }

    /// <summary>
    ///     Método para atualizar o primeiro nome do perfil.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="isUpdate"></param>
    public void UpdateFirstName(string? firstName, bool isUpdate = true)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.");

        if (FirstName != firstName)
            FirstName = firstName;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar o sobrenome do perfil.
    /// </summary>
    /// <param name="lastName"></param>
    /// <param name="isUpdate"></param>
    public void UpdateLastName(string? lastName, bool isUpdate = true)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.");

        if (LastName != lastName)
            LastName = lastName;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar o email do perfil.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="isUpdate"></param>
    public void UpdateEmail(string? email, bool isUpdate = true)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (string.IsNullOrWhiteSpace(email) || !regex.IsMatch(email))
            throw new ArgumentException("Invalid email format.");

        if (Email != email)
            Email = email;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar a imagem do perfil.
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <param name="isUpdate"></param>
    public void UpdateImage(string? imageUrl, bool isUpdate = true)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl) && ImageUrl != imageUrl)
            ImageUrl = imageUrl;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar a biografia do perfil.
    /// </summary>
    /// <param name="bio"></param>
    /// <param name="isUpdate"></param>
    public void UpdateBio(string? bio, bool isUpdate = true)
    {
        if (!string.IsNullOrWhiteSpace(bio) && Bio != bio)
            Bio = bio;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar o gênero do perfil.
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="isUpdate"></param>
    public void UpdateGender(EGender? gender, bool isUpdate = true)
    {
        if (gender != null && Gender != gender.Value)
            Gender = gender.Value;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar a data de nascimento do perfil.
    /// </summary>
    /// <param name="birthDate"></param>
    /// <param name="isUpdate"></param>
    public void UpdateBirthDate(DateTime? birthDate, bool isUpdate = true)
    {
        if (birthDate != null && BirthDate != birthDate.Value)
            BirthDate = birthDate.Value;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar a cidade do perfil.
    /// </summary>
    /// <param name="displayName"></param>
    /// <param name="isUpdate"></param>
    public void UpdateDisplayName(string? displayName, bool isUpdate = true)
    {
        if (!string.IsNullOrWhiteSpace(displayName) && DisplayName != displayName)
            DisplayName = displayName;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar o país do perfil.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="isUpdate"></param>
    public void UpdateCountry(string? country, bool isUpdate = true)
    {
        if (!string.IsNullOrWhiteSpace(country) && Country != country)
            Country = country;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }

    /// <summary>
    ///     Método para atualizar o CEP do perfil.
    /// </summary>
    /// <param name="postalCode"></param>
    /// <param name="isUpdate"></param>
    public void UpdatePostalCode(string? postalCode, bool isUpdate = true)
    {
        if (!string.IsNullOrWhiteSpace(postalCode) && PostalCode != postalCode)
            PostalCode = postalCode;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar a latitude do perfil.
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="isUpdate"></param>
    public void UpdateLatitude(double latitude, bool isUpdate = true)
    {
        if (Latitude != latitude)
            Latitude = latitude;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar a longitude do perfil.
    /// </summary>
    /// <param name="longitude"></param>
    /// <param name="isUpdate"></param>
    public void UpdateLongitude(double longitude, bool isUpdate = true)
    {
        if (Longitude != longitude)
            Longitude = longitude;

        if (isUpdate)
            UpdatedAt = DateTime.Now;
    }
}