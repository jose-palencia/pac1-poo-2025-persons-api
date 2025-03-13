using Persons.API.Database.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persons.API.Database.Entities
{
    [Table("persons")]
    public class PersonEntity : BaseEntity
    {        

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("dni")]
        [Required]
        public string DNI { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("country_id")]
        public Guid? CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual CountryEntity Country { get; set; }

        public virtual IEnumerable<FamilyMemberEntity> FamilyGroup { get; set; }

    }
}
