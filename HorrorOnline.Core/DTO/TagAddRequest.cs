using HorrorOnline.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.DTO
{
    public class TagAddRequest
    {
        [Required]
        [Length(Tag.MinTagLength, Tag.MaxTagLength)]
        public string? TagName { get; set; }

        public Tag ToTag()
        {
            return new Tag()
            {
                TagName = TagName
            };
        }
    }
}
