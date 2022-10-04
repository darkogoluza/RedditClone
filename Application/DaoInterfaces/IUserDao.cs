using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task<IEnumerable<User>> GetAsync();
    Task<User?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(User updated);
}