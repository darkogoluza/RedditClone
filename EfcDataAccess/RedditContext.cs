using Domain;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class RedditContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SubForum> SubForums { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = C:\\Users\\Darko\\Desktop\\VIA\\Semester 3\\DNP1\\Assigment 1\\RedditClone\\EfcDataAccess\\Reddit.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Users
        modelBuilder.Entity<User>().HasKey(user => user.Id);

        // SubForums
        modelBuilder.Entity<SubForum>().HasKey(subForum => subForum.Id);
        modelBuilder.Entity<SubForum>(entity =>
        {
            entity.HasOne(subForum => subForum.CreatedBy);
        });
        
        // Posts
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(post => post.Owner);
            entity.HasOne(post => post.BelongsTo);

        });
        
        // Comments
        modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(comment => comment.WrittenBy);
            entity.HasOne(comment => comment.PostedOn);
            entity.HasOne(comment => comment.ParentComment);
        });

    }
}