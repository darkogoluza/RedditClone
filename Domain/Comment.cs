namespace Domain;

public class Comment
{
    public int Id { get; }
    public User WrittenBy { get; }
    public Post PostedOn { get; }
    public string Body { get; set; }
    public List<Comment> SubComments { get; }

    public Comment(int id, User writtenBy, Post postedOn, string body)
    {
        Id = id;
        WrittenBy = writtenBy;
        Body = body;
        SubComments = new List<Comment>();
        PostedOn = postedOn;
    }
}