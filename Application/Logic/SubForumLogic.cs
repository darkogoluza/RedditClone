using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class SubForumLogic : ISubForumLogic
{
    private readonly ISubForumDao subForumDao;
    private readonly IUserDao userDao;

    public SubForumLogic(ISubForumDao subForumDao, IUserDao userDao)
    {
        this.subForumDao = subForumDao;
        this.userDao = userDao;
    }

    public async Task<SubForum> CreateAsync(SubForumCreationDto subForumCreationDto)
    {
        SubForum? exists = await subForumDao.GetByTypeAsync(subForumCreationDto.Type);
        if (exists != null)
            throw new Exception($"Sub forum with type {subForumCreationDto.Type} already exits");

        User? user = await userDao.GetByIdAsync(subForumCreationDto.CreatorId);
        if (user == null)
            throw new Exception($"User with id {subForumCreationDto.CreatorId} does not exist");

        SubForum subForum = new SubForum(user, subForumCreationDto.Type);


        return await subForumDao.CreateAsync(subForum);
    }

    public async Task<IEnumerable<SubForum>> GetAsync()
    {
        return await subForumDao.GetAsync();
    }
}