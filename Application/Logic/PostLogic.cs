using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly ISubForumDao subForumDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, ISubForumDao subForumDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.subForumDao = subForumDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto postCreationDto)
    {
        SubForum? subForum = await subForumDao.GetByIdAsync(postCreationDto.BelongsToId);
        if(subForum == null)
            throw new Exception($"Sub forum with id {postCreationDto.BelongsToId} does not exist");
        
        
        User? user = await userDao.GetByIdAsync(postCreationDto.OwnerId);
        if (user == null)
            throw new Exception($"User with id {postCreationDto.OwnerId} does not exist");

        Post post = new Post(user, subForum, postCreationDto.Title, postCreationDto.Body);

        return await postDao.CreateAsync(post);
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        return await postDao.GetAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = await postDao.GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with Id {id} does not exist");
        }

        return existing;
    }

    public async Task UpdateAsync(PostUpdateDto postUpdateDto)
    {
        Post? existing = await postDao.GetByIdAsync(postUpdateDto.Id);
        if (existing == null)
        {
            throw new Exception($"Post with Id {postUpdateDto.Id} does not exist");
        }

        User? user = null;
        if (postUpdateDto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int) postUpdateDto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {postUpdateDto.OwnerId} was not found.");
            }
        }

        SubForum? subForum = null;
        if (postUpdateDto.BelongsToId != null)
        {
            subForum = await subForumDao.GetByIdAsync((int) postUpdateDto.BelongsToId);
            if (subForum == null)
            {
                throw new Exception($"Sub forum with id {postUpdateDto.BelongsToId} was not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        SubForum subForumToUse = subForum ?? existing.BelongsTo;
        string titleToUse = postUpdateDto.Title ?? existing.Title;
        string bodyToUse = postUpdateDto.Body ?? existing.Body;
        

        Post updated = new(userToUse, subForumToUse, titleToUse, bodyToUse)
        {
            Id = existing.Id
        };

        await postDao.UpdateAsync(updated);

    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await postDao.GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with Id {id} does not exist");
        }

        await postDao.DeleteAsync(id);
    }
}