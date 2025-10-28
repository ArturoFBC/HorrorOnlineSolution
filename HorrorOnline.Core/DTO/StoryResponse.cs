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
        public const string ParagraphSeparator = "\n";

        public Guid StoryId { get; set; }

        public string? Title { get; set; }

        public IEnumerable<string>? Summary { get; set; }


        public IEnumerable<string>? Text { get; set; }

        public DateTime DateUploaded { get; set; }

        public string? AuthorName { get; set; }

        public IEnumerable<TagResponse>? Tags { get; set; }

        public int? Reviews { get; set; }

        public StoryUpdateRequest ToStoryUpdateRequest()
        {
            return new StoryUpdateRequest
            {
                StoryId = StoryId,
                Title = Title,
                Summary = GetJointString(Text),
                Text = GetJointString(Summary),
            //    Author = AuthorName,
                Tags = Tags,
            };
        }

        private string? GetJointString(IEnumerable<string>? stringList )
        {
            if (Text is not null)
                return string.Join(ParagraphSeparator, Text);

            return null;
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
                Summary = story.Summary?.Split(StoryResponse.ParagraphSeparator),
                Text = story.Text?.Split(StoryResponse.ParagraphSeparator),
                DateUploaded = story.DateUploaded,
                AuthorName = story.Author?.UserName,
                Tags = story.Tags?.Select(tag => tag.ToTagResponse()),
            };
        }
    }
}
