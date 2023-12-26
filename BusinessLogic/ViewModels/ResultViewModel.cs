using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class ResultViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("inner_error_message")]
        public string? InnerMessage { get; set; }
    }
}
