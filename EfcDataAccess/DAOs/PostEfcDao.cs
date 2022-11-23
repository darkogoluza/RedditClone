using Application.DaoInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly RedditContext context;

    public PostEfcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> createdPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return createdPost.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(string? subForm)
    {
        IQueryable<Post> postsQuery = context.Posts
            .Include(p => p.Owner)
            .Include(p => p.BelongsTo)
            .AsQueryable();

        if (subForm != null)
        {
            postsQuery = postsQuery
                .Where(p => p.BelongsTo.Type.Equals(subForm));
        }
        
        IEnumerable<Post> result = await postsQuery.ToListAsync();
        return result;
    }

    public async Task<Post?> GetByIdAsync(int? id)
    {
        return await context.Posts
            .Include(p => p.Owner)
            .Include(p => p.BelongsTo)
            .FirstOrDefaultAsync(sf => sf.Id == id);
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post updated)
    {
        context.ChangeTracker.Clear();
        context.Posts.Update(updated);
        await context.SaveChangesAsync();
    }
}