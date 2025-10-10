using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Services.Stories
{
    public class StoryGetterService : IStoryGetterService
    {
        public readonly IStoryRepository _storyRepository;

        public StoryGetterService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<IEnumerable<StoryResponse>> GetAllStories()
        {
            List<Story> stories = [.. await _storyRepository.GetAllStories()];

            return stories.Select( item => item.ToStoryResponse() );
        }

        public async Task<IEnumerable<StoryResponse>> GetSelectedStories(string searchTerm, string searchField)
        {
            List<Story> selectedStories = searchField switch
            {
                nameof(Story.Title) => [.. await _storyRepository.GetFilteredStories
                (
                    item => item.Title.Contains(searchTerm)
                )],

                nameof(Story.Tags) => [.. await _storyRepository.GetFilteredStories
                (
                     item => item.Tags.First( tag => tag.TagName == searchField ) != null
                )]
            };

            return selectedStories.Select(item => item.ToStoryResponse()).ToList();
        }

        public async Task<IEnumerable<StoryResponse>> GetStoriesByTagId(Guid? tagId)
        {
            if (tagId == null)
                return null;

            IEnumerable<Story>? stories = await _storyRepository.GetStoryByTag(tagId.Value);

            if (stories == null)
                return null;

            return stories.Select(story => story.ToStoryResponse());
        }

        public async Task<StoryResponse?> GetStoryByID(Guid? id)
        {
            if (id == null) 
                return null;

            Story? story = await _storyRepository.GetStoryByID(id.Value);

            if (story == null)
                return null;

            return story.ToStoryResponse();
        }
    }
}
