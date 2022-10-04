using Domain;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
    Task<IEnumerable<Comment>> GetAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task UpdateAsync(Comment updated);
    Task DeleteAsync(int id);
}