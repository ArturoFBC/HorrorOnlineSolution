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
        public Guid StoryId { get; set; }

        public string? Title { get; set; }

        public string? Summary { get; set; }

        public string? Text { get; set; }

        public DateTime DateUploaded { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<TagResponse>? Tags { get; set; }

        public int? Reviews { get; set; }

        public StoryUpdateRequest ToStoryUpdateRequest()
        {
            return new StoryUpdateRequest
            {
                StoryId = StoryId,
                Title = Title,
                Summary = Summary,
                Text = Text,
            //    Author = AuthorName,
                Tags = Tags,
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
                AuthorName = story.Author?.UserName,
                Tags = story.Tags?.Select(tag => tag.ToTagResponse()),
            };
        }
    }
}
