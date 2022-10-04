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

    public async Task DeleteAsync(int id)
    {
        SubForum? exists = await subForumDao.GetByIdAsync(id);
        if (exists == null)
            throw new Exception($"Sub forum with type {id} does not exist");

        await subForumDao.DeleteAsync(id);
    }

    public async Task UpdateAsync(SubForumUpdateDto subForumUpdateDto)
    {
        SubForum? exists = await subForumDao.GetByIdAsync(subForumUpdateDto.Id);
        if (exists == null)
            throw new Exception($"Sub forum with type {subForumUpdateDto.Id} does not exist");

        SubForum updated = new(exists.CreatedBy, subForumUpdateDto.Type)
        {
            Id = exists.Id
        };

        await subForumDao.UpdateAsync(updated);
    }
}