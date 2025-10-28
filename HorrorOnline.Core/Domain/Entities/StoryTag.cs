using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Domain.Entities
{
    [PrimaryKey(nameof(StoryId),nameof(TagId))]
    public class StoryTag
    {
        public Guid StoryId { get; set; }

        public Guid TagId { get; set; }
    }
}
