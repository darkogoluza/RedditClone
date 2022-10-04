using Application.DaoInterfaces;
using Domain;

namespace FileDataAccess.DAOs;

public class CommentFileDao : ICommentDao
{
    public readonly FileContext context;

    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        int id = 0;
        if (context.Comments.Any())
        {
            int max = context.Comments.Max(c => c.Id);
            id = max + 1;
        }

        comment.Id = id;

        context.Comments.Add(comment);
        context.SaveChanges();

        return comment;
    }

    public Task<IEnumerable<Comment>> GetAsync()
    {
        IEnumerable<Comment> comments = context.Comments.AsEnumerable();
        return Task.FromResult(comments);
    }

    public Task<Comment?> GetByIdAsync(int id)
    {
        Comment? toGet = null;
        foreach (var comment in context.Comments)
        {
            if (comment.Id == id)
            {
                toGet = comment;
                break;
            }
        }

        return Task.FromResult(toGet);
    }

    public Task UpdateAsync(Comment updated)
    {
        foreach (var comment in context.Comments)
        {
            if (comment.Id == updated.Id)
            {
                context.Comments.Remove(comment);
                context.Comments.Add(updated);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        foreach (var comment in context.Comments)
        {
            if (comment.Id == id)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
                break;
            }
        }

        return Task.CompletedTask;
    }
}