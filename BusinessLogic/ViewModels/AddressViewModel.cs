using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class AddressViewModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [JsonPropertyName("address")]
        public required string Name { get; set; }


        [Required]
        [JsonPropertyName("type")]
        public required AddressType Type { get; set; }

        [Required]
        [JsonPropertyName("distributor_id")]

        public int DistributorId { get; set; }

    }
}
