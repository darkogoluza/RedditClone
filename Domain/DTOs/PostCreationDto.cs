namespace Domain.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public string Body { get; }
    public int BelongsToId { get; }
    public int OwnerId { get; }

    public PostCreationDto(string title, string body, int belongsToId, int ownerId)
    {
        Title = title;
        Body = body;
        BelongsToId = belongsToId;
        OwnerId = ownerId;
    }
}