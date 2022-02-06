using Newtonsoft.Json;

namespace TicketApp.Models.Journey
{
    public class JourneyDetail
    {
        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public string StartHour { get; set; }

        public string EndHour { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string Price { get; set; }



    }
}
