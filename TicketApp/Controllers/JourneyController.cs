using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.Journey;
using TicketApp.Models.Message;

namespace TicketApp.Controllers
{
    public class JourneyController : Controller
    {
        private ApiController api = new ApiController();
        public IActionResult Index(GeneralMessage msg)
        {
            Journey journey = new Journey()
            {
                StartPoint = msg.StartPointText,
                EndPoint = msg.EndPointText,
                DeviceId = msg.DeviceId,
                SessionId = msg.SessionId,
                Date = msg.Date,
                Journeys = api.GetBusLocations(msg).Result
            };

            return View(journey);
        }

        public IActionResult BusDetail(string originId, string destinationId, string sessionId, string deviceId, string date)
        {
            api.GetJourneys(new GeneralMessage() { SessionId = sessionId, DeviceId = deviceId, Date = date }, Convert.ToInt32(originId), Convert.ToInt32(destinationId));
            return View();
        }
    }
}
