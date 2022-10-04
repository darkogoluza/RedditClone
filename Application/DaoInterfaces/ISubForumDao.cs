using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ISubForumDao
{
    Task<SubForum> CreateAsync(SubForum subForum);
    Task<IEnumerable<SubForum>> GetAsync();
    Task DeleteAsync(int id);
    Task<SubForum?> GetByTypeAsync(string type);
    Task<SubForum?> GetByIdAsync(int belongsToId);
}