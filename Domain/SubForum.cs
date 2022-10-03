namespace Domain;

public class SubForum
{
    public int Id { get; set; }
    public User CreatedBy { get; }
    public string Type { get; }

    public SubForum(User createdBy, string type)
    {
        CreatedBy = createdBy;
        Type = type;
    }
    
}