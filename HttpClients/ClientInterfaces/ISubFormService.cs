using Domain;

namespace HttpClients.ClientInterfaces;

public interface ISubFormService
{
    Task<ICollection<SubForum>> GetAsync();
}