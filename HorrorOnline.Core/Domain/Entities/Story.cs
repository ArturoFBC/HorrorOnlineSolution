using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorrorOnline.Core.Domain.Entities
{
    public class Story
    {
        public const int MaxTitleLength = 100;
        public const int MaxSummaryLength = 500;
        public const int MaxTextLength = 40000;
        public const int MinTextLength = 20;

        [Key]
        public Guid StoryId { get; set; }

        [StringLength(MaxTitleLength)]
        public string? Title { get; set; }

        [StringLength(MaxSummaryLength)]
        public string? Summary { get; set; }

        [StringLength(MaxTextLength, MinimumLength = MinTextLength)]
        // htmlUtility.htmlenconde(text) converts html as text so is safe to display
        public string? Text { get; set; }

        public Guid? AuthorId { get; set; }

        public DateTime DateUploaded { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser? Author { get; set; }

        public virtual ICollection<Tag>? Tags { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

        public virtual ICollection<BookMark>? BookMarks { get; set; }

    }
}
