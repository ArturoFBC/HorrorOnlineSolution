using HorrorOnline.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Stories
{
    public interface IStoryAdderService
    {
        public Task<StoryResponse> AddStory(StoryAddRequest storyAddRequest);
    }
}
