using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace HorrorOnline.Infrastructure.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ITagRepository _tagRepository;

        private readonly ApplicationDbContext _db;


        public StoryRepository(ApplicationDbContext db, ITagRepository tagRepository)
        {
            _db = db;
            _tagRepository = tagRepository;
        }

        public async Task<Story> AddStory(Story story)
        {
            await _db.Stories.AddAsync(story);
            await _db.SaveChangesAsync();

            return story;
        }

        public async Task<bool> DeleteStoryByID(Guid storyID)
        {
            _db.Stories.RemoveRange(_db.Stories.Where(item => item.StoryId == storyID));

            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<IEnumerable<Story>> GetAllStories()
        {
            return await _db.Stories
                .Include(story => story.Tags)
                .Include(story => story.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetFilteredStories(Expression<Func<Story, bool>> predicate)
        {
            IEnumerable<Story> filteredStories = await _db.Stories
                .Where(predicate)
                .Include(story => story.Tags)
                .Include(story => story.Author)
                .ToListAsync();

            return filteredStories;
        }

        public async Task<Story?> GetStoryByID(Guid storyID)
        {
            Story? foundStory = await _db.Stories
                .Include(story => story.Tags)
                .Include(story => story.Author)
                .FirstOrDefaultAsync(item => item.StoryId == storyID);

            return foundStory;
        }

        public async Task<IEnumerable<Story>> GetStoryByTag(Guid tagID)
        {
            IEnumerable<Story> storiesWithTag = await _db.Stories
                .Include(story => story.Tags)
                .Where( story => 
                    story.Tags.Any(
                        tag => tag.TagId == tagID
                        )
                    )
                .Include(story => story.Author)
                .ToListAsync();

            return storiesWithTag;
        }
    }
}
