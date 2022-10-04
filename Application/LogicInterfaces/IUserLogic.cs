using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<IEnumerable<User>> GetAsync();
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UserUpdateDto updateDto);
}