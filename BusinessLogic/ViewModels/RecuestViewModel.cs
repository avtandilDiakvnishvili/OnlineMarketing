using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModels
{
    public class RecuestViewModel
    {
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        //[JsonPropertyName("ids")]
        //public List<int> Ids { get; set; }
    }
}
