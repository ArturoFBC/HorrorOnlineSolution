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
    public class BookMark
    {
        [Key]
        public Guid BookMarkId { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        [Range(1, 1000)]
        public int MarkedLocation { get; set; }

        [ForeignKey("StoryId")]
        public virtual Story? Story { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}
