using HorrorOnline.Core.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HorrorOnline.UI.Models
{
    public class StoryWithTagsModel
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

        [DisplayName("Etiquetas")]
        public string? Tags { get; set; }
    }
}
