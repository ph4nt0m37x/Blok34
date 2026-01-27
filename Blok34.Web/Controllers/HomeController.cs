using Blok34.Domain;
using Blok34.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blok34.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;
        private readonly IEventAttendanceService _eventAttendanceService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, IEventService eventService, IVenueService venueService, IEventAttendanceService eventAttendanceService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _eventService = eventService;
            _venueService = venueService;
            _eventAttendanceService = eventAttendanceService;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var weather = _weatherService.GetDailyWeather(41.9981, 21.4254);
                return View("IndexLoggedIn", weather);
            }

            return View("Index");

        }
        [Authorize]
        public IActionResult Results(string query)
        {
            return View("../Search/Index", query);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
