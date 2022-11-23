using System.Text;
using System.Text.Json;
using Domain;
using Domain.DTOs;
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

    public async Task PublishAsync(string body, int ownerId, int? commentId, int postId)
    {
        CommentCreationDto commentCreationDto = new(ownerId, postId, body, commentId);

        string subFormAsJson = JsonSerializer.Serialize(commentCreationDto);
        StringContent content = new(subFormAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://localhost:7207/Comments", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}