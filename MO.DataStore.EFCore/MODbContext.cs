using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MO.Core;

namespace MO.DataStore.EFCore
{
    public class MODbContext : IdentityDbContext
    {
        public MODbContext(DbContextOptions<MODbContext> options)
            : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostMedia> PostMedia { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Error Fix: The entity type ‘IdentityUserLogin‘ requires a primary
            //  key to be defined. If you intended to use a keyless entity
            //  type call ‘HasNoKey()’.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<PostMedia>().ToTable("PostMedia");
            modelBuilder.Entity<PostTag>().ToTable("PostTag");
        }
    }
}