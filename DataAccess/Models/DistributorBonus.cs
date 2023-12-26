using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("DistributorBonus")]
    public class DistributorBonus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("distributor_id")]
        public int DistributorId { get; set; }

        [Required]
        [Column("bonus")]
        public decimal Bonus { get; set; }


        [Column("start_date")]
        [Required]
        public DateTime StartDate { get; set; }


        [Column("end_date")]
        [Required]
        public DateTime EndDate { get; set; }

        virtual public ICollection<UsedSales>? UsedSales { get; set; }
    }
}
