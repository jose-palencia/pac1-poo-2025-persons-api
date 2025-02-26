using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Countries
{
    public class CountryCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(100, ErrorMessage = "El {0} no puede tener mas de 100 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Codigo Alfanumerico 3")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(3, ErrorMessage = "El {0} debe tener {1} caracteres", MinimumLength = 3)]
        public string AlphaCode3 { get; set; }
    }
}
