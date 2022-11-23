using Application.DaoInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly RedditContext context;

    public CommentEfcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> createdComment = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return createdComment.Entity;
    }

    public async Task<IEnumerable<Comment>> GetAsync(int? postId)
    {
        IQueryable<Comment> commentsQuery = context.Comments
            .Include(c => c.PostedOn)
            .Include(c => c.WrittenBy)
            .Include(c => c.ParentComment)
            .AsQueryable();

        if (postId != null)
        {
            commentsQuery = commentsQuery.Where(c => c.PostedOn.Id == postId);
        }

        IEnumerable<Comment> result = await commentsQuery.ToListAsync();
        return result;
    }

    public async Task<Comment?> GetByIdAsync(int? id)
    {
        return await context.Comments
            .Include(c => c.PostedOn)
            .Include(c => c.WrittenBy)
            .Include(c => c.ParentComment)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateAsync(Comment updated)
    {
        context.ChangeTracker.Clear();
        context.Comments.Update(updated);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Comment with id {id} not found");
        }

        context.Comments.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetSubCommentsAsync(int id)
    {
        IQueryable<Comment> commentsQuery = context.Comments
            .Include(c => c.PostedOn)
            .Include(c => c.WrittenBy)
            .Include(c => c.ParentComment)
            .AsQueryable();


        commentsQuery = commentsQuery.Where(c => c.ParentComment != null && c.ParentComment.Id == id);

        IEnumerable<Comment> result = await commentsQuery.ToListAsync();
        return result;
    }
}