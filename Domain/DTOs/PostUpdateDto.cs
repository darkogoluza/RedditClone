namespace Domain.DTOs;

public class PostUpdateDto
{
    public int? BelongsToId { get; }
    public int? OwnerId { get; }
    public int Id { get; }
    public string? Title { get; }
    public string? Body { get; }

    public PostUpdateDto(int? belongsToId, int? ownerId, int id, string title, string body)
    {
        BelongsToId = belongsToId;
        OwnerId = ownerId;
        Id = id;
        Title = title;
        Body = body;
    }
}