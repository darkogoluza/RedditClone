namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public User WrittenBy { get; }
    public Post PostedOn { get; }
    public string Body { get; set; }
    public Comment? ParentComment { get; }

    public Comment(User writtenBy, Post postedOn, string body, Comment? parentComment)
    {
        WrittenBy = writtenBy;
        Body = body;
        ParentComment = parentComment;
        PostedOn = postedOn;
    }

    private Comment(int id, string body)
    {
        Id = id;
        Body = body;
    }
}