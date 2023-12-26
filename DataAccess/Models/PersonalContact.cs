using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("PersonalContact", Schema = "dbo")]
    public class PersonalContact
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("type")]
        public required ContactType Type { get; set; }

        [Required]
        [Column("contact_info")]
        [MaxLength(100)]
        public required string ContactInfo { get; set; }


        [Required]
        [Column("distributor_id")]
        public int DistributorId { get; set; }


        [ForeignKey("DistributorId")]
        public virtual Distributor? Distributor { get; set; }

    }
}
