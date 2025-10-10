using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.DTO
{
    public class StoryUpdateRequest : StoryAddRequest
    {
        [Required(ErrorMessage ="El Id del relato no puede estar en blanco")]
        public Guid StoryId { get; set; }

        public Guid? Author {  get; set; }

        public new Story ToStory()
        {
            Story story = base.ToStory();

            story.StoryId = StoryId;

            return story;
        }
    }
}
