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
        Post? post = await postDao.GetByIdAsync(creationDto.PostId);
        if (post == null)
            throw new Exception($"Post with id {creationDto.PostId} does not exist");

        User? user = await userDao.GetByIdAsync(creationDto.OwnerId);
        if (user == null)
            throw new Exception($"User with id {creationDto.OwnerId} does not exist");

        if (creationDto.CommentParentId != null)
        {
            Comment? parentComment = await commentDao.GetByIdAsync((int) creationDto.CommentParentId);
            if (parentComment == null)
                throw new Exception($"Parent comment with id {creationDto.CommentParentId} does not exist");
        }

        Comment comment = new Comment(user, creationDto.PostId, creationDto.Body, creationDto.CommentParentId);

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
            post = await postDao.GetByIdAsync((int) commentUpdateDto.PostId);
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
        
        if (commentUpdateDto.CommentParentId != null)
        {
            Comment? parentComment = await commentDao.GetByIdAsync((int) commentUpdateDto.CommentParentId);
            if (parentComment == null)
                throw new Exception($"Parent comment with id {commentUpdateDto.CommentParentId} does not exist");
        }

        User userToUse = user ?? existing.WrittenBy;
        int postIdToUse = commentUpdateDto.PostId ?? existing.PostedOn;
        string bodyToUse = commentUpdateDto.Body ?? existing.Body;
        int? parentIdToUse = commentUpdateDto.CommentParentId ?? existing.ParentCommentId;

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