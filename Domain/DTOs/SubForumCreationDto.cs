namespace Domain.DTOs;

public class SubForumCreationDto
{
    
    public string Type { get; set; }
    public int CreatorId { get; set; }


    public SubForumCreationDto(string type, int creatorId)
    {
        Type = type;
        CreatorId = creatorId;
    }
}