using Domain;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<ICollection<Comment>> GetAllCommentsFromAPostAsync(int id);
    Task<ICollection<Comment>> GetAllSubCommentsAsync(int commentId);
    Task PublishAsync(string body, int ownerId, int? commentId, int postId);
}