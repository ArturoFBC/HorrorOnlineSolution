using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HorrorOnline.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Story> Stories { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<BookMark> BookMarks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        
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
                .UsingEntity(
            r => r.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsTagId"),
            l => l.HasOne(typeof(Story)).WithMany().HasForeignKey("StoriesStoryId"));

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
            string storiesJson = File.ReadAllText("stories.json");
            List<Story> stories = JsonSerializer.Deserialize<List<Story>>(storiesJson);

            foreach (Story story in stories)
                modelBuilder.Entity<Story>().HasData(story);

            string tagsJson = File.ReadAllText("tags.json");
            List<Tag> tags = JsonSerializer.Deserialize<List<Tag>>(tagsJson);

            foreach (Tag tag in tags)
                modelBuilder.Entity<Tag>().HasData(tag);
        }
    }
}
