namespace Domain;

public class Comment
{
    public int Id { get; }
    public User WritenBy { get; }
    public string body { get; set; }
    public List<Comment> SubComments { get; }

    public Comment(int id, User writenBy, string body)
    {
        Id = id;
        WritenBy = writenBy;
        this.body = body;
        SubComments = new List<Comment>();
    }
}