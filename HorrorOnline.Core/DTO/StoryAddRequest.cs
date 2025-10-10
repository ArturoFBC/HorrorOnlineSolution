using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HorrorOnline.Core.DTO
{
    public class StoryAddRequest
    {
        [DisplayName("Título")]
        [Required(ErrorMessage = "El título no puede estar en blanco")]
        [StringLength(Story.MaxTitleLength, ErrorMessage = "El título es demasiado largo")]
        public string? Title { get; set; }

        [DisplayName("Resumen")]
        [StringLength(Story.MaxSummaryLength, ErrorMessage = "El resumen es demasiado largo")]
        public string? Summary { get; set; }

        [DisplayName("Relato")]
        [StringLength(Story.MaxTextLength, MinimumLength = Story.MinTextLength, ErrorMessage = "El relato es demasiado largo o corto")]
        [Required(ErrorMessage = "El texto del relato está en blanco")]
        public string? Text { get; set; }

        public Guid? AuthorId { get; set; }

        [DisplayName("Etiquetas")]
        [Required]
        public IEnumerable<Guid>? TagIds { get; set; }

        public Story ToStory()
        {
            return new Story
            {
                Title = Title,
                Summary = Summary,
                Text = Text,
                TagIds = TagIds,
                AuthorId = AuthorId
            };
        }
    }
}
