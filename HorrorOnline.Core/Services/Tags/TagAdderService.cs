using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;

namespace HorrorOnline.Core.Services.Tags
{
    public class TagAdderService : ITagAdderService
    {
        public readonly ITagRepository tagRepository;

        public TagAdderService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<TagResponse> AddTag(TagAddRequest tagAddRequest)
        {
            Tag tagToAdd = tagAddRequest.ToTag();
            tagToAdd.TagId = Guid.NewGuid();

            Tag addedTag = await tagRepository.AddTag(tagToAdd);

            return addedTag.ToTagResponse();
        }
    }
}
