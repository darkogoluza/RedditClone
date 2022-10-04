namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public User WrittenBy { get; }
    public int PostedOn { get; }
    public string Body { get; set; }
    public int? ParentCommentId { get; }

    public Comment(User writtenBy, int postedOn, string body, int? parentCommentId)
    {
        WrittenBy = writtenBy;
        Body = body;
        ParentCommentId = parentCommentId;
        PostedOn = postedOn;
    }
    
}