using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class UserLogic: IUserLogic
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
}