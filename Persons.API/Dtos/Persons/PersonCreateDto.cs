using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Persons
{
    public class PersonCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string LastName { get; set; }

        [Display(Name = "Documento Nacional de Identidad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string DNI { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(1, ErrorMessage = "El {0} solo acepta {1} caracter.")]
        public string Gender { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid? CountryId { get; set; } = null;
    }
}
