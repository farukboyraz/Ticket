using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketApp.Models;
using TicketApp.Models.Message;

namespace TicketApp.Controllers
{
    public class HomeController : Controller
    {
        private ApiController api = new ApiController();

        public IActionResult Index()
        {
            try
            {
                var sessionInfo = api.GetSession();
                if (string.IsNullOrEmpty(sessionInfo.Result.Data.SessionId) || string.IsNullOrEmpty(sessionInfo.Result.Data.DeviceId))
                    throw new Exception("SessionId and DeviceId must not Null variable");

                ViewBag.Today = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                ViewBag.SessionId = sessionInfo.Result.Data.SessionId;
                ViewBag.DeviceID = sessionInfo.Result.Data.DeviceId;

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured when API were calling. Error: "+ ex.Message,ex);
            }
        }
        [HttpPost]
        public IActionResult Index(GeneralMessage message)
        {
            return RedirectToAction("Index", "Journey", message);
        }
    }
}