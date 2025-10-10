using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Services.Tags
{
    public class TagDeleterService : ITagDeleterService
    {
        public readonly ITagRepository tagRepository;

        public TagDeleterService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        public async Task<bool> DeleteTagById(Guid? tagID)
        {
            if (tagID == null)
                return false;

            Tag? tagToDelete = await tagRepository.GetTagByID(tagID.Value);

            if (tagToDelete == null)
                return false;

            return await tagRepository.DeleteTagByID(tagID.Value);
        }
    }
}
