using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Sale
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("distributor_id")]
        public int? DistributorId { get; set; }

        [Required]
        [Column("tdate")]
        public DateTime TDate { get; set; }

        [Required]
        [Column("total")]
        public decimal TotalPrice { get; set; }


        [Column("used")]
        public bool IsUsed { get; set; }

        [ForeignKey("DistributorId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public virtual Distributor? Distributor { get; set; }

        public virtual ICollection<SalesProduct>? ProductList { get; set; }
    }
}
