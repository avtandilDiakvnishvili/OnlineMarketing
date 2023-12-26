using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("UsedSale")]
    public class UsedSales
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("bonus_id")]
        [Required]
        public int BonusId { get; set; }

        [Column("sale_id")]
        [Required]
        public int SalesId { get; set; }

        [ForeignKey("BonusId")]
        public virtual DistributorBonus? DistributorBonus { get; set; }
    }
}
