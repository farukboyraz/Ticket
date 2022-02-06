using Newtonsoft.Json;

namespace TicketApp.Models.ApiResponse
{
    public class SessionApiResponse: GeneralApiResponse
    {
        [JsonProperty("data")]
        public SessionData Data { get; set; }

    }

    public class SessionData
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }

        [JsonProperty("affiliate")]
        public string? Affiliate { get; set; }

        [JsonProperty("device-type")]
        public int DeviceType { get; set; }

        [JsonProperty("device")]
        public string? Device { get; set; }
    }
}
