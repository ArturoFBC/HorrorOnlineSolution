using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Domain.Entities
{
    public class Review
    {
        public const int MaxReviewLength = 1000;

        [Key]
        public Guid ReviewId { get; set; }

        [Range(1,10)]
        public int Score { get; set; }

        [StringLength(MaxReviewLength)]
        public string ReviewText { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public Guid? StoryId { get; set; }

        public Guid? AuthorId { get; set; }

        public virtual Story? Story { get; set; }

        public virtual ApplicationUser? Author { get; set; }
    }
}
