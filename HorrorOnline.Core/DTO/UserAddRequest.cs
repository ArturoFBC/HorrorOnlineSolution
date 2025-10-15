using HorrorOnline.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HorrorOnline.Core.DTO
{
    public class UserAddRequest
    {
        [Required(ErrorMessage = "El nombre no puede estar en blanco")]
        [Remote(action: "IsUserNameAlreadyRegistered", controller: "Account", ErrorMessage = "Ya existe una cuenta con ese nombre")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El correo no puede estar en blanco")]
        [EmailAddress(ErrorMessage = "La dirección no es válida")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Ya existe una cuenta con ese correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar en blanco")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirma la contraseña")]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone should contain only numbers")]
        [Phone]
        public string Phone { get; set; }

        public UserTypeRole UserType { get; set; } = UserTypeRole.User;
    }
}
