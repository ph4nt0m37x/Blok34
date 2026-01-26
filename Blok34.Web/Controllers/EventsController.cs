using Blok34.Domain.DomainModels;
using Blok34.Service.Implementation;
using Blok34.Service.Interface;
using Blok34.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blok34.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;
        private readonly IEventAttendanceService _eventAttendanceService;

        public EventsController(IEventService eventService,
            IVenueService venueService,
            IEventAttendanceService eventAttendanceService)
        {
            _eventService = eventService;
            _venueService = venueService;
            _eventAttendanceService = eventAttendanceService;
        }

        // GET: Events
        public IActionResult Index()
        {
            return View(_eventService.GetAllEvents());
        }

        // GET: Events/Details/5
        public IActionResult Details(Guid id)
        {
            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
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
            if (ModelState.IsValid)
            {
                _eventService.Insert(@event);
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public IActionResult Edit(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var venues = _venueService.GetAllVenues()
            .Where(v => v.IsPublic || v.VenueManagerId == userId)
            .ToList();

            
            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
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

            _eventService.Update(@event);
            return RedirectToAction(nameof(Index));
          //  return View(@event);
        }

        // GET: Events/Delete/5
        public IActionResult Delete(Guid id)
        {
            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _eventService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
