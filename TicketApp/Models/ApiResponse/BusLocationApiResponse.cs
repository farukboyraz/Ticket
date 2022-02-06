using Newtonsoft.Json;

namespace TicketApp.Models.ApiResponse
{
    public class BusLocationApiResponse: GeneralApiResponse
    {
        [JsonProperty("data")]
        public List<BusLocationResponseData> Data { get; set; }
    }


    public class BusLocationResponseData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parent-id")]
        public int? ParentId { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation? GeoLocation { get; set; }

        [JsonProperty("tz-code")]
        public string? TzCode { get; set; }

        [JsonProperty("weather-code")]
        public string? WeatherCode { get; set; }

        [JsonProperty("rank")]
        public int? Rank { get; set; }

        [JsonProperty("reference-code")]
        public string? ReferenceCode { get; set; }

        [JsonProperty("keywords")]
        public string? Keywords { get; set; }

    }

    public class GeoLocation
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("zoom")]
        public string Zoom { get; set; }

    }
}
