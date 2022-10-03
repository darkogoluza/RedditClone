using Domain;

namespace FileDataAccess;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<SubForum> SubForums { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }

}