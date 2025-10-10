using HorrorOnline.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HorrorOnline.Core.DTO
{
    public class TagResponse
    {
        public Guid TagId { get; set; }

        [Required]
        [Length(Tag.MinTagLength, Tag.MaxTagLength)]
        public string TagName { get; set; } = string.Empty;

        public ICollection<Story>? Stories { get; set; }
    }

    public static class ToTagResponseExtension
    {
        public static TagResponse ToTagResponse(this Tag tag)
        {
            return new TagResponse {
                TagId = tag.TagId,
                TagName = tag.TagName,
                Stories = tag.Stories
            };
        }
    }
}
