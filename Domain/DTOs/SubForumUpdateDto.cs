namespace Domain.DTOs;

public class SubForumUpdateDto
{
    public int Id { get; }
    public string Type { get; }

    public SubForumUpdateDto(int id, string type)
    {
        Id = id;
        Type = type;
    }
}