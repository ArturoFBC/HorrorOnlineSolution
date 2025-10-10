using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.ServiceContracts.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Services.Stories
{
    public class StoryDeleterService : IStoryDeleterService
    {
        public readonly IStoryRepository storyRepository;

        public StoryDeleterService(IStoryRepository storyRepository)
        {
            this.storyRepository = storyRepository;
        }


        public async Task<bool> DeleteStoryById(Guid? storyID)
        {
            if (storyID == null)
                return false;

            Story? storyToDelete = await storyRepository.GetStoryByID(storyID.Value);

            if (storyToDelete == null)
                return false;

            return await storyRepository.DeleteStoryByID(storyID.Value);
        }
    }
}
