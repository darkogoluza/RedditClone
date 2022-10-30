using System.Text.Json;
using Domain;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<Post>?> GetAsync(string? type)
    {
        HttpResponseMessage response = await client.GetAsync("/Posts/?subForm=" + type);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post>? posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return posts;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync("/Post/?id=" + id);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post? post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return post;
    }
}