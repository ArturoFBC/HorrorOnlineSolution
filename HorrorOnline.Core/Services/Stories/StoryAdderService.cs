using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Services.Stories
{
    public class StoryAdderService : IStoryAdderService
    {
        public readonly IStoryRepository _storyRepository;

        public StoryAdderService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<StoryResponse> AddStory(StoryAddRequest storyAddRequest)
        {
            Story storyToAdd = storyAddRequest.ToStory();
            storyToAdd.StoryId = Guid.NewGuid();
            storyToAdd.DateUploaded = DateTime.Now;

            Story addedStory = await _storyRepository.AddStory(storyToAdd);

            return addedStory.ToStoryResponse();
        }


    }
}
