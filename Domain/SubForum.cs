namespace Domain;

public class SubForum
{
    public int Id { get; set; }
    public User CreatedBy { get; }
    public string Type { get; set; }

    public SubForum(User createdBy, string type)
    {
        CreatedBy = createdBy;
        Type = type;
    }

    private SubForum(int id, string type)
    {
        Id = id;
        Type = type;
    }
}