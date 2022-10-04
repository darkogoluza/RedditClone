﻿namespace Domain.DTOs;

public class UserUpdateDto
{
    public string? Username { get; }
    public string? Password { get; }
    public int Id { get; }

    public UserUpdateDto(string username, string password, int id)
    {
        Username = username;
        Password = password;
        Id = id;
    }
}