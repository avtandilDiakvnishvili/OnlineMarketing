using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Address", Schema = "dbo")]
    public class DistributorAddress
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("address")]
        public required string Name { get; set; }


        [Required]
        [Column("type")]
        public required AddressType Type { get; set; }

        [Required]
        [Column("distributor_id")]

        public int DistributorId { get; set; }


        [ForeignKey("DistributorId")]
        public virtual Distributor? Distributor { get; set; }
    }
}
