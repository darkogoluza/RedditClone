using System.Data.Common;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Shared.Dtos;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        return await userDao.GetAsync();
    }

    public async Task<User> CreateAsync(UserCreationDto userCreationDto)
    {
        return await userDao.CreateAsync(userCreationDto);
    }

    public async Task DeleteAsync(int id)
    {
        User? exists = await userDao.GetByIdAsync(id);
        if (exists == null)
        {
            throw new Exception($"User with id {id} does not exist.");
        }

        await userDao.DeleteAsync(id);
    }

    public async Task UpdateAsync(UserUpdateDto updateDto)
    {
        User? exists = await userDao.GetByIdAsync(updateDto.Id);
        if (exists == null)
        {
            throw new Exception($"User with id {updateDto.Id} does not exist.");
        }

        string usernameToUse = updateDto.Username ?? exists.UserName;
        string passwordToUse = updateDto.Password ?? exists.Password;

        User updated = new(usernameToUse, passwordToUse)
        {
            Id = exists.Id
        };

        await userDao.UpdateAsync(updated);
    }

    public async Task<User> ValidateUser(UserLoginDto userLoginDto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(userLoginDto.Username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(userLoginDto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
}