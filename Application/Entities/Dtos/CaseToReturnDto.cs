using Newtonsoft.Json;

namespace Application.Entities.Dtos
{
    public class CaseToReturnDto
    {
        [JsonProperty("observation_date")]
        public string ObservationDate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("confirmed")]
        public int Confirmed { get; set; }

        [JsonProperty("deaths")]
        public int Death { get; set; }

        [JsonProperty("recovered")]
        public int Recovered { get; set; }
    }
}
