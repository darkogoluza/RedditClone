namespace Domain;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; }
    public SubForum BelongsTo { get; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(User owner, SubForum belongsTo, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
        BelongsTo = belongsTo;
    }
}