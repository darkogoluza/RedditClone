using System.Text.Json;
using Domain;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient client;

    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task<ICollection<Comment>> GetAllCommentsFromAPostAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync("/Comments?postId=" + id);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment>? comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return comments;
    }

    public async Task<ICollection<Comment>> GetAllSubCommentsAsync(int commentId)
    {
        HttpResponseMessage response = await client.GetAsync("/SubComments?id=" + commentId);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment>? comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return comments;
    }
}