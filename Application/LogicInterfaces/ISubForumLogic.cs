using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ISubForumLogic
{
    Task<SubForum> CreateAsync(SubForumCreationDto subForumCreationDto);
    Task<IEnumerable<SubForum>> GetAsync();
}