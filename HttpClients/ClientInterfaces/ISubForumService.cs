using Domain;

namespace HttpClients.ClientInterfaces;

public interface ISubForumService
{
    Task<ICollection<SubForum>> GetAsync();
    Task CreateAsync(string name, int id);
}