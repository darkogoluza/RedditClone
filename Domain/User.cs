﻿namespace Domain;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    private User(int id, string userName, string password)
    {
        Id = id;
        UserName = userName;
        Password = password;
    }
}