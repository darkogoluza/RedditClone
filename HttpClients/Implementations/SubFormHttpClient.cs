using System.Text.Json;
using Domain;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SubFormHttpClient : ISubFormService
{
    
    private readonly HttpClient client;

    public SubFormHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<ICollection<SubForum>> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/SubForms");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<SubForum>? subForums = JsonSerializer.Deserialize<ICollection<SubForum>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return subForums;
    }
}