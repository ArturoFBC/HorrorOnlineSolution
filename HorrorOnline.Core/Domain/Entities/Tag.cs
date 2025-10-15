using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorrorOnline.Core.Domain.Entities
{
    public class Tag
    {
        public const int MinTagLength = 1;
        public const int MaxTagLength = 40;

        [Key]
        public Guid TagId { get; set; }

        [Required]
        [Length(MinTagLength, MaxTagLength)]
        public string? TagName { get; set; } = string.Empty;

        public virtual ICollection<Story>? Stories { get; } = new List<Story>();
    }
}
