﻿using SocialService.Common.Entities;
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
    /// <param name="objectId"></param>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <param name="country"></param>
    /// <param name="imageUrl"></param>
    /// <param name="createdAt"></param>
    /// <param name="updatedAt"></param>
    /// <param name="gender"></param>
    /// <param name="birthDate"></param>
    /// <param name="bio"></param>
    public Profile(Guid objectId, string email, string firstName, string lastName, string state, string city, string country, string? imageUrl, DateTime createdAt,
        DateTime updatedAt,
        EGender gender, DateTime birthDate, string? bio)
    {
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        State = state;
        City = city;
        
        Id = objectId;
        Email = email;
        ImageUrl = imageUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Gender = gender;
        BirthDate = birthDate;
        Bio = bio;
    }

    /// <summary>
    ///     Construtor para criação de um novo perfil através de um comando.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    public Profile(CreateProfileCommand command, Guid id)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Country = command.Country;
        State = command.State;
        City = command.City;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Bio = command.Bio;
        Id = id;
        Email = command.Email;
        CreatedAt = command.CreatedAt;
        UpdatedAt = command.CreatedAt;
    }

    /// <summary>
    /// Construtor padrão.
    /// </summary>
    public Profile() { }

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
    /// Cidade do perfil
    /// </summary>
    public string City { get; private set; }
    
    /// <summary>
    /// Estado do perfil
    /// </summary>
    public string State { get; private set; }
    
    /// <summary>
    /// País do perfil
    /// </summary>
    public string Country { get; private set; }

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
    public DateTime BirthDate { get; private set; }

    /// <summary>
    ///     Gênero do perfil.
    /// </summary>
    public EGender Gender { get; private set; }

    /// <summary>
    ///     Data de criação do perfil.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Data de atualização do perfil.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    ///     Método para atualizar o perfil com base em um objeto de valor.
    /// </summary>
    /// <param name="profile"></param>
    public void UpdateBasedOnValueObject(ProfileDto profile)
    {
        FirstName = profile.FirstName;
        LastName = profile.LastName;
        Country = profile.Country;
        State = profile.State;
        City = profile.City;
        Gender = profile.Gender;
        BirthDate = profile.BirthDate;
        Bio = profile.Bio;
        Email = profile.Email;
        ImageUrl = profile.ImageUrl;
        UpdatedAt = DateTime.Now;
    }

    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Email = result["email"].ToString();
        FirstName = result["firstName"].ToString();
        LastName = result["lastName"].ToString();
        City = result["city"].ToString();
        State = result["state"].ToString();
        Country = result["country"].ToString();
        ImageUrl = result["imageUrl"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        UpdatedAt = DateTime.Parse(result["updatedAt"].ToString());
        BirthDate = DateTime.Parse(result["birthDate"].ToString());
        Bio = result["bio"].ToString();
        
        base.MapToEntityFromNeo4j(result);
    }
}