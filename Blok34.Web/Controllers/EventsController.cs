using Blok34.Domain.DomainModels;
using Blok34.Domain.Enums;
using Blok34.Service.Implementation;
using Blok34.Service.Interface;
using Blok34.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blok34.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;
        private readonly IEventAttendanceService _eventAttendanceService;
        private readonly IUserService _userService;

        public EventsController(IEventService eventService, IVenueService venueService, IEventAttendanceService eventAttendanceService, IUserService userService)
        {
            _eventService = eventService;
            _venueService = venueService;
            _eventAttendanceService = eventAttendanceService;
            _userService = userService;
        }

        // GET: Events
        // Update your Index action in EventsController
        public IActionResult Index(string searchQuery, string eventStatus)
        {
            var events = _eventService.GetAllEvents();

            // Apply search if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                events = _eventService.SearchEvents(searchQuery);
            }

            // Apply event status filter if provided
            if (!string.IsNullOrEmpty(eventStatus))
            {
                var now = DateTime.UtcNow;

                switch (eventStatus.ToLower())
                {
                    case "upcoming":
                        events = events.Where(e => e.StartDate > now).ToList();
                        break;
                    case "ongoing":
                        events = events.Where(e => e.StartDate <= now &&
                                                 (!e.EndDate.HasValue || e.EndDate.Value >= now)).ToList();
                        break;
                    case "past":
                        events = events.Where(e => e.EndDate.HasValue && e.EndDate.Value < now).ToList();
                        break;
                }
            }

            return View(events);
        }

        // GET: Events/Details/5
        public IActionResult Details(Guid id)
        {

            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(@event.CreatedByUserId))
            {
                var creator = _userService.GetUserById(@event.CreatedByUserId);
                ViewBag.CreatorName = creator?.Name ?? "Event Organizer";
                ViewBag.CreatorUsername = creator?.UserName;
                ViewBag.CreatorProfilePicture = creator?.AvatarPath;
            }


            return View(@event);
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


        // GET: Events/Create
        public IActionResult Create()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var venues = _venueService.GetAllVenues()
            .Where(v => v.IsPublic || v.VenueManagerId == userId)
            .ToList();

            ViewData["VenueId"] = new SelectList(venues, "Id", "Name");

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,StartDate,EndDate,VenueId,Category,CreatedByUserId,Id")] Event @event)
        {
            ModelState.Remove("Venue");

            var venue = _venueService.GetVenueById(@event.VenueId);
            if (venue != null)
            {
                @event.Venue = venue;
            }

            if (ModelState.IsValid)
            {
                
                _eventService.Insert(@event);
                return RedirectToAction(nameof(Index));
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var venues = _venueService.GetAllVenues()
                            .Where(v => v.IsPublic || v.VenueManagerId == userId)
                            .ToList();

            // Provide the list again and set selected value to restore hidden select
            ViewData["VenueId"] = new SelectList(venues, "Id", "Name", @event?.VenueId);


            return View(@event);
        }

        // GET: Events/Edit/5
        public IActionResult Edit(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var @event = _eventService.GetEventById(id);

            if (@event.CreatedByUserId != userId)
            {
                return Forbid();
            }

            var venues = _venueService.GetAllVenues()
            .Where(v => v.IsPublic || v.VenueManagerId == userId)
            .ToList();

            if (@event == null)
            {
                return NotFound();
            }

            if (DateTime.UtcNow >= @event.StartDate)
            {
                return Forbid();
            }


            ViewData["VenueId"] = new SelectList(venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,StartDate,EndDate,VenueId,Category,CreatedByUserId,Id")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (@event.CreatedByUserId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                _eventService.Update(@event);

                return RedirectToAction("Events", "Profile", new { id = userId });
            }

            var venues = _venueService.GetAllVenues()
                .Where(v => v.IsPublic || v.VenueManagerId == userId)
                .ToList();

            ViewData["VenueId"] = new SelectList(venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public IActionResult Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.CreatedByUserId != userId)
            {
                return Forbid(); // or Unauthorized()
            }


            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var @event = _eventService.GetEventById(id);

            if (@event.CreatedByUserId != userId)
            {
                return Forbid();
            }

            _eventService.DeleteById(id);

            return RedirectToAction("Events", "Profile", new { id = userId });
        }

    }
}
