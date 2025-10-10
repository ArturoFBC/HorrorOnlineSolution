using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HorrorOnline.Infrastructure.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ITagRepository _tagRepository;

        public static List<Story> Stories = new List<Story>();

        public StoryRepository(ITagRepository tagRepository)
        {
            //Seed data
            string storiesJson = File.ReadAllText("stories.json");
            Stories = JsonSerializer.Deserialize<List<Story>>(storiesJson);
            _tagRepository = tagRepository;
        }

        public async Task<Story> AddStory(Story story)
        {
            Stories.Add(story);

            return story;
        }

        public async Task<bool> DeleteStoryByID(Guid storyID)
        {
            Story? foundStory = Stories.Find(item => item.StoryId == storyID);

            if (foundStory == null)
            {
                return false;
            }
            else
            {
                return Stories.Remove(foundStory);
            }

        }

        public async Task<IEnumerable<Story>> GetAllStories()
        {
            foreach (Story story in Stories)
            {
                await FillTags(story);
            }

            return new List<Story>(Stories);
        }

        private async Task FillTags(Story story)
        {
            if (story.Tags == null || story.Tags.Any() == false)
            {
                story.Tags = new List<Tag>();
                foreach (Guid tagId in story.TagIds)
                {
                    story.Tags.Add(await _tagRepository.GetTagByID(tagId));
                }
            }
        }

        public async Task<IEnumerable<Story>> GetFilteredStories(Expression<Func<Story, bool>> predicate)
        {
            IEnumerable<Story> filteredStories = Stories.Where(predicate.Compile());

            foreach (Story story in filteredStories)
            {
                await FillTags(story);
            }

            return filteredStories;
        }

        public async Task<Story?> GetStoryByID(Guid storyID)
        {
            Story? foundStory = Stories.Find(item => item.StoryId == storyID);

            if (foundStory != null)
                await FillTags(foundStory);

            return foundStory;
        }

        public async Task<IEnumerable<Story>> GetStoryByTag(Guid tagID)
        {
            IEnumerable<Story> storiesWithTag = Stories.Where( story => 
            story.TagIds != null && story.TagIds.Contains(tagID));

            foreach (Story story in storiesWithTag)
            {
                await FillTags(story);
            }

            return storiesWithTag;
        }
    }
}
