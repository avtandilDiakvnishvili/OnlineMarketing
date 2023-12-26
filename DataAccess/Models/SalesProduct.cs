using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("SalesProduct")]
    public class SalesProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }


        [Required]
        [Column("sale_id")]
        public int SaleId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("product_price")]
        public decimal ProductSalePrice { get; set; }

        [Required]
        [Column("product_self_cost")]
        public decimal ProductSelfCost { get; set; }

        [ForeignKey("SaleId")]
        public virtual Sale? Sale { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }




    }
}
