using DataAccess.Enums;
using System.Text.Json.Serialization;

namespace BusinessLogic.ViewModels
{
    public class DistributorViewModel
    {

        [JsonPropertyName("id")]

        public int Id { get; set; }


        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }


        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("recommender")]
        public int? Recommender { get; set; }



        [JsonPropertyName("recommenders")]
        public List<IdName>? Recommenders { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("img_path")]
        public string? ImgPath { get; set; }

        [JsonPropertyName("img_byte")]
        public byte[]? ImgByte { get; set; }

        [JsonPropertyName("contacts")]
        public ICollection<ContactViewModel>? Contacts { get; set; }

        [JsonPropertyName("documents")]
        public ICollection<PersonalDocsViewModel>? PersonalDocuments { get; set; }

        [JsonPropertyName("addresses")]
        public ICollection<AddressViewModel>? Addresses { get; set; }


        //[JsonPropertyName("recommended_count")]

        [JsonIgnore]
        public int RecommendedCount { get; set; }

        [JsonIgnore]
        public int Level { get; set; }


    }
}
