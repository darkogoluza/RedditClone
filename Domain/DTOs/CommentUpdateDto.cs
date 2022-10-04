namespace Domain.DTOs;

public class CommentUpdateDto
{
    public CommentUpdateDto(int? ownerId, int? postId, string? body, int id, int? commentParentId)
    {
        OwnerId = ownerId;
        PostId = postId;
        Body = body;
        Id = id;
        CommentParentId = commentParentId;
    }

    public int? OwnerId { get; }
    public int? PostId { get; }
    public string? Body { get; }
    public int Id { get; }
    public int? CommentParentId { get; }
}