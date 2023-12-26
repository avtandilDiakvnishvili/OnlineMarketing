using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("PersonalDocument", Schema = "dbo")]

    public class PersonalDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("type")]
        public required PersonDocumentType Type { get; set; }

        [Column("document_seria")]
        [MaxLength(10)]
        public string? DocumentSeria { get; set; }

        [Column("document_number")]
        [MaxLength(10)]
        public string? DocumentNumber { get; set; }

        [Required]
        [Column("release_date")]
        public required DateTime ReleaseDate { get; set; }


        [Required]
        [Column("due_date")]
        public required DateTime DueDate { get; set; }

        [Required]
        [Column("personal_number")]
        [MaxLength(50)]
        public required string PersonalNumber { get; set; }

        [Column("agency")]
        [MaxLength(100)]
        public required string Agency { get; set; }

        [Required]
        [Column("distributor_id")]
        public int DistributorId { get; set; }

        [ForeignKey("DistributorId")]
        public virtual Distributor? Distributor { get; set; }







    }
}
