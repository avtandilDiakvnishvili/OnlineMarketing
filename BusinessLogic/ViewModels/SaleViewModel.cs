using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class SaleViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("distributor_id")]
        public int? DistributorId { get; set; }

        [JsonPropertyName("distributor_name")]
        public string? DistributorName { get; set; }

        [JsonPropertyName("tdate")]
        public DateTime TDate { get; set; }

        [JsonPropertyName("total")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("products")]
        public ICollection<SaleProductsViewModel>? SaleProducts { get; set; }

    }
}
