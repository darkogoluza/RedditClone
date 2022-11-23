using Application.DaoInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class SubForumEfcDao : ISubForumDao
{
    private readonly RedditContext context;

    public SubForumEfcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<SubForum> CreateAsync(SubForum subForum)
    {
        EntityEntry<SubForum> createdSubForum = await context.SubForums.AddAsync(subForum);
        await context.SaveChangesAsync();
        return createdSubForum.Entity;
    }

    public async Task<IEnumerable<SubForum>> GetAsync()
    {
        return await context.SubForums
            .Include(sb => sb.CreatedBy)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        SubForum? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"SubForum with id {id} not found");
        }

        context.SubForums.Remove(existing);
        await context.SaveChangesAsync();    
    }

    public async Task<SubForum?> GetByTypeAsync(string type)
    {
        return await context.SubForums.FirstOrDefaultAsync(sf => sf.Type.Equals(type));
    }

    public async Task<SubForum?> GetByIdAsync(int belongsToId)
    {
        return await context.SubForums.FindAsync(belongsToId);
    }

    public async Task UpdateAsync(SubForum updated)
    {
        context.ChangeTracker.Clear();
        context.SubForums.Update(updated);
        await context.SaveChangesAsync();
    }
}