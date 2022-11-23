using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly RedditContext context;

    public UserEfcDao(RedditContext context)
    {
        this.context = context;
    }
    
    public async Task<User> CreateAsync(UserCreationDto userCreationDto)
    {
        
        User toCreate = new User(userCreationDto.UserName, userCreationDto.Password);

        EntityEntry<User> createdUser = await context.Users.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return createdUser.Entity;
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"User with id {id} not found");
        }

        context.Users.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User updated)
    {
        context.ChangeTracker.Clear();
        context.Users.Update(updated);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));
    }
}