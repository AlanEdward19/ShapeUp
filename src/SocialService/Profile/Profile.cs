using System.ComponentModel.DataAnnotations;
using SocialService.Profile.Common.Enums;
using SocialService.Profile.CreateProfile;

namespace SocialService.Profile;

/// <summary>
/// Classe que representa o perfil do usuário.
/// </summary>
public class Profile
{
    /// <summary>
    /// Construtor padrão.
    /// </summary>
    /// <param name="objectId"></param>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="imageUrl"></param>
    /// <param name="createdAt"></param>
    /// <param name="updatedAt"></param>
    /// <param name="gender"></param>
    /// <param name="birthDate"></param>
    /// <param name="bio"></param>
    public Profile(Guid objectId, string email, string firstName, string lastName, string? imageUrl, DateTime createdAt, DateTime updatedAt,
        EGender gender, DateTime birthDate, string? bio)
    {
        FirstName = firstName;
        LastName = lastName;
        ObjectId = objectId;
        Email = email;
        ImageUrl = imageUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Gender = gender;
        BirthDate = birthDate;
        Bio = bio;
    }

    /// <summary>
    /// Construtor para criação de um novo perfil através de um comando.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public Profile(CreateProfileCommand command, Guid id)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Bio = command.Bio;
        ObjectId = id;
        Email = command.Email;
        CreatedAt = command.CreatedAt;
        UpdatedAt = command.CreatedAt;
    }

    /// <summary>
    /// Id do perfil
    /// </summary>
    [Key] public Guid ObjectId { get; private set; }

    /// <summary>
    /// Email do perfil.
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Sobrenome do perfil.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Url da imagem do perfil no blob storage
    /// </summary>
    public string? ImageUrl { get; private set; }
    
    /// <summary>
    /// Biografia do perfil.
    /// </summary>
    public string? Bio { get; private set; }
    
    /// <summary>
    /// Data de nascimento do perfil.
    /// </summary>
    public DateTime BirthDate { get; private set; }
    
    /// <summary>
    /// Gênero do perfil.
    /// </summary>
    public EGender Gender { get; private set; }
    
    /// <summary>
    /// Data de criação do perfil.
    /// </summary>
    public DateTime CreatedAt { get; private set; }
    
    /// <summary>
    /// Data de atualização do perfil.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Método para atualizar o perfil com base em um objeto de valor.
    /// </summary>
    /// <param name="profile"></param>
    public void UpdateBasedOnValueObject(ProfileAggregate profile)
    {
        FirstName = profile.FirstName;
        LastName = profile.LastName;
        Gender = profile.Gender;
        BirthDate = profile.BirthDate;
        Bio = profile.Bio;
        Email = profile.Email;
        ImageUrl = profile.ImageUrl;
        UpdatedAt = DateTime.Now;
    }
}