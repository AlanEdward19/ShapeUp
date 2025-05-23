﻿using SocialService.Profile.Common.Enums;

namespace SocialService.Profile;

/// <summary>
///     Objeto de transferência de dados do perfil
/// </summary>
/// <param name="profile"></param>
/// <param name="state"></param>
/// <param name="city"></param>
public class ProfileDto(Profile profile, string state, string city)
{
    /// <summary>
    ///     Id do perfil
    /// </summary>
    public string Id { get; private set; } = profile.Id;

    /// <summary>
    ///     Email do perfil
    /// </summary>
    public string Email { get; private set; } = profile.Email;

    /// <summary>
    ///     Primeiro nome do perfil
    /// </summary>
    public string FirstName { get; private set; } = profile.FirstName;

    /// <summary>
    ///     Sobrenome do perfil
    /// </summary>
    public string LastName { get; private set; } = profile.LastName;

    /// <summary>
    /// Pais do perfil
    /// </summary>
    public string Country { get; private set; } = profile.Country;
    
    /// <summary>
    ///     Cidade do perfil
    /// </summary>
    public string City { get; private set; } = city;
    
    /// <summary>
    /// Estado do perfil
    /// </summary>
    public string State { get; private set; } = state;

    /// <summary>
    ///     Nome de exibição do perfil
    /// </summary>
    public string DisplayName { get; private set; } = profile.DisplayName;
    
    /// <summary>
    /// Quantidade de seguidores do perfil.
    /// </summary>
    public int Followers { get; private set; } = profile.Followers;
    
    /// <summary>
    /// Quantidade de pessoas que o perfil segue.
    /// </summary>
    public int Following { get; private set; } = profile.Following;

    /// <summary>
    /// Quantidade de posts do perfil.
    /// </summary>
    public int Posts { get; private set; } = profile.Posts;

    /// <summary>
    ///     Url da imagem do perfil no blob storage
    /// </summary>
    public string? ImageUrl { get; private set; } = profile.ImageUrl;

    /// <summary>
    ///     Biografia do perfil
    /// </summary>
    public string? Bio { get; private set; } = profile.Bio;

    /// <summary>
    ///     Data de nascimento do perfil
    /// </summary>
    public DateTime? BirthDate { get; private set; } = profile.BirthDate;

    /// <summary>
    ///     Gênero do perfil
    /// </summary>
    public EGender? Gender { get; private set; } = profile.Gender;
    
    /// <summary>
    /// Se o perfil é amigo do usuário logado.
    /// </summary>
    public bool IsFriend { get; private set; } = profile.IsFriend;
    
    /// <summary>
    /// Se o perfil é seguido pelo usuário logado.
    /// </summary>
    public bool IsFollowing { get; private set; } = profile.IsFollowing;
    
    /// <summary>
    /// Método para setar a url da imagem do perfil.
    /// </summary>
    /// <param name="imageUrl"></param>
    public void SetImageUrl(string imageUrl) => ImageUrl = imageUrl;
}