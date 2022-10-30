using Domain;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<IEnumerable<Post>?> GetAsync(string? type);
    Task<Post?> GetByIdAsync(int id);
}