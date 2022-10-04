using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto postCreationDto);
    Task<IEnumerable<Post>> GetAsync();
    Task<Post?> GetByIdAsync(int id);
    Task UpdateAsync(PostUpdateDto postUpdateDto);
    Task DeleteAsync(int id);
}