using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Persons
{
    public class FamilyMemberCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Los {0} son requerido")]
        [StringLength(30, 
                ErrorMessage = "Los {0} deben tener entre {2} y {1} caracteres", 
                MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los {0} son requerido")]
        [StringLength(30,
                ErrorMessage = "Los {0} deben tener entre {2} y {1} caracteres",
                MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Parentesco")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(20, 
                ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres", 
                MinimumLength = 3)]
        public string RelationShip { get; set; }
    }
}
