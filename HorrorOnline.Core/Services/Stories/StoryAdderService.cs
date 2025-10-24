using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;

namespace HorrorOnline.Core.Services.Stories
{
    public class StoryAdderService : IStoryAdderService
    {
        public readonly IStoryRepository _storyRepository;
        public readonly ITagRepository _tagRepository;

        public StoryAdderService(IStoryRepository storyRepository, ITagRepository tagRepository)
        {
            _storyRepository = storyRepository;
            _tagRepository = tagRepository;
        }

        public async Task<StoryResponse> AddStory(StoryAddRequest storyAddRequest)
        {
            Story storyToAdd = storyAddRequest.ToStory();
            storyToAdd.StoryId = Guid.NewGuid();
            storyToAdd.DateUploaded = DateTime.Now;
            storyToAdd.Tags = new List<Tag>();

            if (storyAddRequest.Tags != null)
            {
                foreach (TagResponse tagResponse in storyAddRequest.Tags)
                {
                    Tag? newTag = await _tagRepository.GetTagByID(tagResponse.TagId);

                    if (newTag is not null)
                        storyToAdd.Tags.Add(newTag);
                }
            }

            Story addedStory = await _storyRepository.AddStory(storyToAdd);

            return addedStory.ToStoryResponse();
        }


    }
}
