using Domain;
using Domain.DTOs;
using Shared.Dtos;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<IEnumerable<User>> GetAsync();
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UserUpdateDto updateDto);
    Task<User> ValidateUser(UserLoginDto userLoginDto);
}