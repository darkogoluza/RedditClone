namespace Domain.DTOs;

public class SubForumUpdateDto
{
    public SubForumUpdateDto(int id, string type)
    {
        Id = id;
        Type = type;
    }

    public int Id { get; }
    public string Type { get; }

}