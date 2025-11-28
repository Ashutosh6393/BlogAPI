using Microsoft.EntityFrameworkCore;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {

                entity.HasIndex(e => e.Email).IsUnique();

                // user -> post (one to many)
                entity
                    .HasMany(e => e.Posts)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // user -> comment (one to many)
                entity
                    .HasMany(e => e.Comments)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                    

            });

            modelBuilder.Entity<Post>(entity =>
            {
                // post -> comment (one to many)
                entity
                    .HasMany(e => e.Comment)
                    .WithOne(e => e.Post)
                    .HasForeignKey(e => e.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
