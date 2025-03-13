using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persons.API.Database.Entities
{
    [Table("family_group")]
    public class FamilyMemberEntity 
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("relation_ship")]
        public string RelationShip { get; set; }

        [Column("person_id")]
        public Guid PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual PersonEntity Person { get; set; }
    }
}
