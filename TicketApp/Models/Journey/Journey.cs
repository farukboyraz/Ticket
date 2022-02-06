using TicketApp.Models.Message;

namespace TicketApp.Models.Journey
{
    public class Journey: GeneralMessage
    {
        public List<JourneyDetail> Journeys { get; set; } 
    }
}
