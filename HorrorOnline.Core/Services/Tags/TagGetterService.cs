using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Services.Tags
{
    public class TagGetterService : ITagGetterService
    {
        public readonly ITagRepository _tagRepository;

        public TagGetterService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagResponse>> GetAllTags()
        {
            List<Tag> tags = [.. await _tagRepository.GetAllTags()];

            return tags.Select( item => item.ToTagResponse() );
        }

        public async Task<TagResponse?> GetTagByID(Guid? id)
        {
            if (id == null) 
                return null;

            Tag? tag = await _tagRepository.GetTagByID(id.Value);

            if (tag == null)
                return null;

            return tag.ToTagResponse();
        }

        public async Task<TagResponse?> GetTagByName(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            Tag? tag = await _tagRepository.GetTagByName(name);

            if (tag == null)
                return null;

            return tag.ToTagResponse();
        }
    }
}
