using System.Text.Json;
using Domain;

namespace FileDataAccess;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }
    public ICollection<SubForum> SubForums
    {
        get
        {
            LoadData();
            return dataContainer!.SubForums;
        }
    }
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }
    public ICollection<Comment> Comments
    {
        get
        {
            LoadData();
            return dataContainer!.Comments;
        }
    }

    private void LoadData()
    {
        if(dataContainer != null) return;

        if (!File.Exists(filePath))
        {
            dataContainer = new()
            {
                Users = new List<User>(),
                SubForums = new List<SubForum>(),
                Posts = new List<Post>(),
                Comments = new List<Comment>()
            };
            
            return;
        }

        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}