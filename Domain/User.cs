﻿namespace Domain;

public class User
{
    public int Id { get;  }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(int id, string userName, string password)
    {
        Id = id;
        UserName = userName;
        Password = password;
    }
}