using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HorrorOnline.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Story> Stories { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<BookMark> BookMarks { get; set; }

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor accessor) : base(options)
        {
            var conn = Database.GetDbConnection() as SqlConnection;
            conn.AccessToken = accessor.HttpContext.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Story>().ToTable(nameof(Stories));
            builder.Entity<Tag>().ToTable(nameof(Tags));
            builder.Entity<Review>() .ToTable(nameof(Reviews));
            builder.Entity<BookMark>().ToTable(nameof(BookMarks));

            SeedData(builder);

            //Table Relations
            builder.Entity<Story>(story =>
            {
                story.HasOne(story => story.Author)
                .WithMany(user => user.Stories)
                .HasForeignKey(story => story.AuthorId);

                story.HasMany(story => story.Tags)
                .WithMany(tag => tag.Stories)
                .UsingEntity<StoryTag>();

                story.HasMany(story => story.Reviews)
                .WithOne(review => review.Story).HasForeignKey(review => review.ReviewId);

                story.HasMany(story => story.BookMarks)
                .WithOne(bm => bm.Story).HasForeignKey(bm => bm.StoryId);
            });

            builder.Entity<Review>(review =>
            {
                review.HasOne(review => review.Author)
                .WithMany(user => user.Reviews).HasForeignKey(review => review.AuthorId);
            });

            builder.Entity<BookMark>(bm =>
            {
                bm.HasOne(bm => bm.Story)
                .WithMany(story => story.BookMarks)
                .HasForeignKey(bm => bm.StoryId);

                bm.HasOne(bm => bm.User)
                .WithMany(user => user.BookMarks)
                .HasForeignKey(bm => bm.UserId);
            });
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            //Seed data
            DeserializeFromFile<Tag>("tags.json", modelBuilder);

            string storiesJson = File.ReadAllText("stories.json");
            List<Story> stories = JsonSerializer.Deserialize<List<Story>>(storiesJson);

            stories.ForEach(story => story.Text = story.Text.Replace("\\n", "\n"));

            foreach (Story story in stories)
            {
                modelBuilder.Entity<Story>().HasData(story);
            }

            DeserializeFromFile<StoryTag>("storyTag.json", modelBuilder);

            DeserializeFromFile<ApplicationUser>("users.json", modelBuilder);
        }

        private static void DeserializeFromFile<T>(string fileName, ModelBuilder modelBuilder) where T : class
        {
            string dataJson = File.ReadAllText(fileName);
            List<T> dataEntries = JsonSerializer.Deserialize<List<T>>(dataJson);

            foreach (T dataEntry in dataEntries)
                modelBuilder.Entity<T>().HasData(dataEntry);
        }
    }
}
