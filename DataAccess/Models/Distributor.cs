using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Distributor", Schema = "dbo")]

    public class Distributor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public required string LastName { get; set; }

        [Required]
        [Column("birth_date")]
        public required DateTime BirthDate { get; set; }

        [Column("recommender")]
        public int? Recommender { get; set; }

        [Required]
        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("img_path")]
        public string? ImgPath { get; set; }

        [Column("recommended_count")]
        public int RecommendedCount { get; set; }


        [Column("level")]
        public int Level { get; set; } = 1;


        public virtual ICollection<PersonalDocument>? PersonalDocuments { get; set; }
        public virtual ICollection<PersonalContact>? PersonalContacts { get; set; }
        public virtual ICollection<DistributorAddress>? DistributorAddress { get; set; }


    }


}
