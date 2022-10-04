namespace Domain.DTOs;

public class CommentCreationDto
{
    public CommentCreationDto(int ownerId, int postId, string body, int? commentParentId)
    {
        OwnerId = ownerId;
        PostId = postId;
        Body = body;
        CommentParentId = commentParentId;
    }

    public int OwnerId { get; }
    public int PostId { get; }
    public string Body { get; }
    public int? CommentParentId { get; }
}