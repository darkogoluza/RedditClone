namespace Domain;

public class SubForum
{
    public int Id { get; }
    public User CreatedBy { get; }
    public string Type { get; }
    public List<Post> Posts { get; }

    public SubForum(int id, User createdBy, string type)
    {
        Id = id;
        CreatedBy = createdBy;
        Type = type;
        Posts = new List<Post>();
    }
    
}