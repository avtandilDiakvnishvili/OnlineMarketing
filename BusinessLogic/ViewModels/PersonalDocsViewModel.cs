using DataAccess.Enums;
using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class PersonalDocsViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("type")]
        public   PersonDocumentType Type { get; set; }

        [JsonPropertyName("document_seria")]
        public string? DocumentSeria { get; set; }

        [JsonPropertyName("document_number")]
        public string? DocumentNumber { get; set; }


        [JsonPropertyName("release_date")]
        public   DateTime ReleaseDate { get; set; }

        [JsonPropertyName("due_date")]
        public   DateTime DueDate { get; set; }

        [JsonPropertyName("personal_number")]
        public   string PersonalNumber { get; set; }

        [JsonPropertyName("agency")]
        public   string Agency { get; set; }

        [JsonPropertyName("distributor_id")]
        public int DistributorId { get; set; }
    }
}
