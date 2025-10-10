using HorrorOnline.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Stories
{
    public interface IStoryGetterService
    {
        public Task<IEnumerable<StoryResponse>> GetAllStories();

        public Task<StoryResponse?> GetStoryByID(Guid? id);

        public Task<IEnumerable<StoryResponse>> GetSelectedStories(string searchTerm, string searchField);
        public Task<IEnumerable<StoryResponse>> GetStoriesByTagId(Guid? tagId);
    }
}
