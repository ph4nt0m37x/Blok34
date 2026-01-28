using Blok34.Domain.Enums;
using Blok34.Domain.Identity;
using Blok34.Service.Implementation;
using Blok34.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blok34.Web.Controllers
{
    public class ProfileController : Controller
    {
       // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;
        private readonly IEventAttendanceService _eventAttendanceService;

        public ProfileController(IUserService userService, IEventService eventService, IVenueService venueService, IEventAttendanceService eventAttendanceService)
        {
            _userService = userService;
            _eventService = eventService;
            _venueService = venueService;
            _eventAttendanceService = eventAttendanceService;
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Events(string id)
        {
          
            return View(_eventService.GetEventsByCreator(id));
        }

        public IActionResult Venues(string id)
        {
            return View(_venueService.GetVenuesByOwner(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateAttendance(Guid eventId, AttendanceStatus status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (status == AttendanceStatus.Attending)
            {
                _eventAttendanceService.AttendEvent(eventId, userId);
            }
            else if (status == AttendanceStatus.Interested)
            {
                _eventAttendanceService.MarkInterested(eventId, userId);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CancelAttendance(Guid eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _eventAttendanceService.RemoveAttendance(eventId, userId);

            return Ok();
        }



    }
}
