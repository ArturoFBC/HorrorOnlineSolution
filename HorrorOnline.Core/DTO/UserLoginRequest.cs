using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.DTO
{
    public class UserLoginRequest
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre no puede estar en blanco")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña no puede estar en blanco")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Recordar datos")]
        [DefaultValue(false)]
        public bool RememberMe { get; set; }
    }
}
