using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Security.Roles
{
    public class RoleCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(256, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Description { get; set; }
    }
}
