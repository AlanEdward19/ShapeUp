﻿using System.ComponentModel.DataAnnotations;
using SocialService.Profile.Common.Enums;
using SocialService.Profile.CreateProfile;

namespace SocialService.Profile;

public class Profile
{
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

    [Key] public Guid ObjectId { get; private set; }

    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Bio { get; private set; }
    public DateTime BirthDate { get; private set; }
    public EGender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

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