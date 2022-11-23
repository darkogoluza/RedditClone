using Application.DaoInterfaces;
using Domain;

namespace FileDataAccess.DAOs;

public class PostFileDao : IPostDao
{
    public readonly FileContext context;
    
    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<Post> CreateAsync(Post post)
    {
        int id = 0;
        if (context.Posts.Any())
        {
            int max = context.Posts.Max(p => p.Id);
            id = max + 1;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(string? subForm)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();

        if (subForm != null)
        {
            posts = posts.Where(post => post.BelongsTo.Type.Equals(subForm));
        }

        return Task.FromResult(posts);
    }

    public Task<Post?> GetByIdAsync(int? id)
    {
        Post? toGet = null;
        foreach (var post in context.Posts)
        {
            if (post.Id == id)
            {
                toGet = post;
                break;
            }
        }

        return Task.FromResult(toGet);
    }

    public Task DeleteAsync(int id)
    {
        foreach (var post in context.Posts)
        {
            if (post.Id == id)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Post updated)
    {
        foreach (var post in context.Posts)
        {
            if (post.Id == updated.Id)
            {
                context.Posts.Remove(post);
                context.Posts.Add(updated);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }
}