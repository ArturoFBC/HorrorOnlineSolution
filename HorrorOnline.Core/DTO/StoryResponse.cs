using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.DTO
{
    public class StoryResponse
    {
        [Required]
        public Guid StoryId { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Summary { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        public DateTime DateUploaded { get; set; }

        [Required]
        public Guid? Author { get; set; }

        [Required]
        public IEnumerable<Guid>? TagIds { get; set; }

        public IEnumerable<string?>? TagNames { get; set; }

        public int? Reviews { get; set; }

        public StoryUpdateRequest ToStoryUpdateRequest()
        {
            return new StoryUpdateRequest
            {
                StoryId = StoryId,
                Title = Title,
                Summary = Summary,
                Text = Text,
                Author = Author,
                TagIds = TagIds,
            };
        }
    }

    public static class ToStoryResponseExtension
    {
        public static StoryResponse ToStoryResponse(this Story story)
        {
            return new StoryResponse
            {
                StoryId = story.StoryId,
                Title = story.Title,
                Summary = story.Summary,
                Text = story.Text,
                DateUploaded = story.DateUploaded,
                Author = Guid.Parse("7382AD97-C28D-445F-8EBF-9F3B2825605B"), // story.Author?.Id,
                TagIds = [Guid.Parse( "5EE06366-A21B-496F-8B39-4FD0E75C5C31"),
                    Guid.Parse("49E17F63-2B52-4B62-9FEF-29522E62E059"),
                    Guid.Parse("427CCDB3-64E6-4796-9B67-BFCA6329C265")], //[.. story.Tags.Select(tag => tag.TagId)]
                TagNames = story.Tags?.Select(tag => tag.TagName)
            };
        }
    }
}
