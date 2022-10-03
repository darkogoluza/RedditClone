namespace Domain;

public class Post
{
    public int Id { get; }
    public User Owner { get; }
    public SubForum BelongsTo { get; }
    public string Title { get; set; }
    public string Password { get; set; }

    public Post(int id, User owner, SubForum belongsTo, string title, string password)
    {
        Id = id;
        Owner = owner;
        Title = title;
        Password = password;
        BelongsTo = belongsTo;
    }
}