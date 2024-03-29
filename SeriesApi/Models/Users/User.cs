﻿using System.ComponentModel.DataAnnotations;
using SeriesApi.Enums.Users;
using SeriesApi.Models.Middle;

namespace SeriesApi.Models.Users;

public class User
{
    [Key]
    public int Id { get; set; }

    public Guid Guid { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string? Gender { get; set; }
    public string? BirthDate { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }

    public string? VkProfile { get; set; }
    public string? OkProfile { get; set; }


    public string? Token { get; set; }
    public string? TokenExpire { get; set; }

    public UserType Type { get; set; }

    public IList<UserFavoriteMovie> FavoriteMovies { get; set; } = new List<UserFavoriteMovie>();
}