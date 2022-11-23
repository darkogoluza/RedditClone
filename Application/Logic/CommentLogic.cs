using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly ICommentDao commentDao;
    private readonly IUserDao userDao;
    private readonly IPostDao postDao;

    public CommentLogic(ICommentDao commentDao, IUserDao userDao, IPostDao postDao)
    {
        this.commentDao = commentDao;
        this.userDao = userDao;
        this.postDao = postDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto creationDto)
    {
        Post? post =  await postDao.GetByIdAsync(creationDto.PostId);
        if (post == null)
            throw new Exception($"Post with id {creationDto.PostId} does not exist");

        User? user = await userDao.GetByIdAsync(creationDto.OwnerId);
        if (user == null)
            throw new Exception($"User with id {creationDto.OwnerId} does not exist");

        Comment? parentComment = null;
        if (creationDto.CommentParentId != null)
        {
            parentComment = await commentDao.GetByIdAsync(creationDto.CommentParentId);
            if (parentComment == null)
                throw new Exception($"Parent comment with id {creationDto.CommentParentId} does not exist");
        }

        Comment comment = new Comment(user, post, creationDto.Body, parentComment);

        return await commentDao.CreateAsync(comment);
    }

    public async Task<IEnumerable<Comment>> GetAsync(int? postId)
    {
        return await commentDao.GetAsync(postId);
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        Comment? comment = await commentDao.GetByIdAsync(id);
        if (comment == null)
            throw new Exception($"Comment with the id {id} does not exist.");

        return comment;
    }

    public async Task UpdateAsync(CommentUpdateDto commentUpdateDto)
    {
        Comment? existing = await commentDao.GetByIdAsync(commentUpdateDto.Id);
        if (existing == null)
            throw new Exception($"Comment with Id {commentUpdateDto.Id} does not exist");

        Post? post = null;
        if (commentUpdateDto.PostId != null)
        {
            post = await postDao.GetByIdAsync(commentUpdateDto.PostId);
            if (post == null)
                throw new Exception($"Post with Id {commentUpdateDto.PostId} does not exist");
        }

        User? user = null;
        if (commentUpdateDto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int) commentUpdateDto.OwnerId);
            if (user == null)
                throw new Exception($"User with Id {commentUpdateDto.OwnerId} does not exist");
        }

        Comment? parentComment = null;
        if (commentUpdateDto.CommentParentId != null)
        {
            parentComment = await commentDao.GetByIdAsync(commentUpdateDto.CommentParentId);
            if (parentComment == null)
                throw new Exception($"Parent comment with id {commentUpdateDto.CommentParentId} does not exist");
        }

        User userToUse = user ?? existing.WrittenBy;
        Post postIdToUse = post ?? existing.PostedOn;
        string bodyToUse = commentUpdateDto.Body ?? existing.Body;
        Comment? parentIdToUse = parentComment ?? existing;

        Comment updated = new(userToUse, postIdToUse, bodyToUse, parentIdToUse)
        {
            Id = existing.Id
        };

        await commentDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Comment? comment = await commentDao.GetByIdAsync(id);
        if (comment == null)
            throw new Exception($"Comment with the id {id} does not exist.");

        await commentDao.DeleteAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetSubCommentsAsync(int id)
    {
        return await commentDao.GetSubCommentsAsync(id);
    }
}