using System.Text.Json.Serialization;

namespace OnlineMarketing.ViewModels
{
    public class ProductViewModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }
    }
}
