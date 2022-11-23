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

    public Task<IEnumerable<Comment>> GetAsync(int? postId)
    {
        IEnumerable<Comment> comments = context.Comments.AsEnumerable();

        if (postId != null)
        {
            comments = comments.Where(comment => comment.PostedOn.Id == postId);
            comments = comments.Where(comment => comment.ParentComment == null);
        }
        
        return Task.FromResult(comments);
    }
    
    public Task<IEnumerable<Comment>> GetSubCommentsAsync(int id)
    {
        IEnumerable<Comment> comments = context.Comments.AsEnumerable();
        comments = comments.Where(comment => comment.ParentComment.Id == id);
        return Task.FromResult(comments);
    }

    public Task<Comment?> GetByIdAsync(int? id)
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