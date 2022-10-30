using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;
using Shared.Dtos;

namespace FileDataAccess.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<User> CreateAsync(UserCreationDto userCreationDto)
    {
        User toCraete = new User(userCreationDto.UserName, userCreationDto.Password);

        int id = 0;
        if (context.Users.Any())
        {
            int max = context.Users.Max(u => u.Id);
            id = max + 1;
        }

        toCraete.Id = id;
        
        context.Users.Add(toCraete);
        context.SaveChanges();

        return Task.FromResult(toCraete);
    }

    public Task<IEnumerable<User>> GetAsync()
    {
        IEnumerable <User> users = context.Users.AsEnumerable();
        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User toGet = null;
        foreach (var user in context.Users)
        {
            if (user.Id == id)
            {
                toGet = user;
                break;
            }
        }

        return Task.FromResult(toGet);
    }

    public Task DeleteAsync(int id)
    {
        foreach (var user in context.Users)
        {
            if (user.Id == id)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User updated)
    {
        foreach (var user in context.Users)
        {
            if (user.Id == updated.Id)
            {
                context.Users.Remove(user);
                context.Users.Add(updated);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }

    public Task<User?> GetByUsernameAsync(string Username)
    {
        User toGet = null;
        foreach (var user in context.Users)
        {
            if (user.UserName ==  Username)
            {
                toGet = user;
                break;
            }
        }

        return Task.FromResult(toGet);
    }
}