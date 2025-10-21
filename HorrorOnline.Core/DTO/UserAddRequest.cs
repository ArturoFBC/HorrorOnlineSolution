using HorrorOnline.Core.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using HorrorOnline.Core.Domain.Entities.IdentityEntities;

namespace HorrorOnline.Core.DTO
{
    public class UserAddRequest
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre no puede estar en blanco")]
        [Remote(action: "IsUserNameAlreadyRegistered", controller: "Account", ErrorMessage = "Ya existe una cuenta con ese nombre")]
        public string UserName { get; set; }

        [DisplayName("Correo")]
        [Required(ErrorMessage = "El correo no puede estar en blanco")]
        [EmailAddress(ErrorMessage = "La dirección no es válida")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Ya existe una cuenta con ese correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña no puede estar en blanco")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirmación contraseña")]
        [Required(ErrorMessage = "Confirma la contraseña")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "La contraseña y la confirmación no coinciden")]
        public string ConfirmPassword { get; set; }


        public UserTypeRole UserType { get; set; } = UserTypeRole.User;

        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser()
            {
                UserName = this.UserName,
                Email = this.Email
            };

        }
    }
}
