using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class ContactViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public required ContactType Type { get; set; }

        [JsonPropertyName("contact_info")]
        public required string ContactInfo { get; set; }


        [JsonPropertyName("distributor_id")]
        public int DistributorId { get; set; }

    }
}
