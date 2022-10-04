using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;

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
        User ToCraete = new User(userCreationDto.UserName, userCreationDto.Password);

        int id = 0;
        if (context.Users.Any())
        {
            int max = context.Users.Max(u => u.Id);
            id = max + 1;
        }

        ToCraete.Id = id;
        
        context.Users.Add(ToCraete);
        context.SaveChanges();

        return Task.FromResult(ToCraete);
    }

    public Task<IEnumerable<User>> GetAsync()
    {
        IEnumerable <User> users = context.Users.AsEnumerable();
        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User ToGet = null;
        foreach (var user in context.Users)
        {
            if (user.Id == id)
            {
                ToGet = user;
                break;
            }
        }

        return Task.FromResult(ToGet);
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
}