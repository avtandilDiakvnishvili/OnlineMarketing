using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class SaleProductsViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("sale_id")]
        public int SaleId { get; set; }

        [JsonPropertyName("product_id")]
        public int ProductId { get; set; } 
        
        [JsonPropertyName("name")]
        public string? ProductName { get; set; }
        
        [JsonPropertyName("code")]
        public string? Code { get; set; }


        [JsonPropertyName("product_price")]
        public decimal ProductSalePrice { get; set; }

        [JsonPropertyName("product_self_cost")]
        public decimal ProductSelfCost { get; set; }

    }
}
