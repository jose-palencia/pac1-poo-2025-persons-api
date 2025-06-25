﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persons.API.Dtos.Security.Users
{
    public class UserCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(50, ErrorMessage = "Los {0} no pueden tener más de {1} caracteres")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(50, ErrorMessage = "Los {0} no pueden tener más de {1} caracteres")]
        public string LastName { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato de correo válido")]
        [StringLength(256, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        public string Email { get; set; }

        //[StringLength(256)]
        //public string AvatarUrl { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(100, ErrorMessage = "La {0} no puede tener más de {1} caracteres")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no son iguales")]
        public string ConfirmPassword { get; set; }
    }
}
