using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;

namespace FileDataAccess.DAOs;

public class SubForumFileDao : ISubForumDao
{
    private readonly FileContext context;

    public SubForumFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<SubForum> CreateAsync(SubForum subForum)
    {
        int id = 0;
        if (context.SubForums.Any())
        {
            int max = context.SubForums.Max(sb => sb.Id);
            id = max + 1;
        }

        subForum.Id = id;
        
        context.SubForums.Add(subForum);
        context.SaveChanges();

        return Task.FromResult(subForum);
    }

    public Task<IEnumerable<SubForum>> GetAsync()
    {
        IEnumerable <SubForum> subForums = context.SubForums.AsEnumerable();
        return Task.FromResult(subForums);
    }

    public Task DeleteAsync(int id)
    {
        foreach (var subForum in context.SubForums)
        {
            if (subForum.Id == id)
            {
                context.SubForums.Remove(subForum);
                break;
            }
        }

        return Task.CompletedTask;
    }

    public Task<SubForum?> GetByTypeAsync(string type)
    {
        SubForum ToGet = null;
        foreach (var subForum in context.SubForums)
        {
            if (subForum.Type == type)
            {
                ToGet = subForum;
                break;
            }
        }

        return Task.FromResult(ToGet);
    }
}