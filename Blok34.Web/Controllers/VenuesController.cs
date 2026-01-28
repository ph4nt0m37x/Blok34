using Blok34.Domain.DomainModels;
using Blok34.Service.Implementation;
using Blok34.Service.Interface;
using Blok34.Web.Data;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
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
    public class VenuesController : Controller
    {
        private readonly IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;

        }

        // GET: Venues
        public IActionResult Index(string searchQuery, string venueType)
        {
            var venues = _venueService.GetAllVenues();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                venues = _venueService.SearchVenues(searchQuery);
            }



            if (!string.IsNullOrEmpty(venueType))
            {
                if (venueType.ToLower() == "public")
                {
                    venues = venues.Where(v => v.IsPublic).ToList();
                }
                else if (venueType.ToLower() == "private")
                {
                    venues = venues.Where(v => !v.IsPublic).ToList();
                }
            }


            return View(venues);
        }
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction(nameof(Index));
            }

            var venues = _venueService.SearchVenues(query);
            return View("Index", venues);
        }

        // GET: Venues/Details/5
        public IActionResult Details(Guid id)
        {
            var venue = _venueService.GetVenueById(id);
            if (venue == null)
            {
                return NotFound();
            };

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Category,Description,Address,BannerPath,Phone,IsPublic,VenueManagerId,Id")] Venue venue, IFormFile BannerFile)
        {


            if (ModelState.IsValid)
            {
                if (BannerFile != null && BannerFile.Length > 0)
                {
                    // Ensure the uploads folder exists
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    // Create a unique file name to avoid overwriting
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        BannerFile.CopyTo(stream);
                    }

                    // Save the relative path in the model
                    venue.BannerPath = "/uploads/" + uniqueFileName;
                }


                _venueService.Insert(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public IActionResult Edit(Guid id)
        {
            var venue = _venueService.GetVenueById(id);


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (venue.VenueManagerId != userId)
            {
                return Forbid();
            }

            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Category,Description,Address,BannerPath,Phone,IsPublic,VenueManagerId,Id")] Venue venue, IFormFile BannerFile)
        {
            if (id != venue.Id)
            {
                return NotFound();
            }


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (@venue.VenueManagerId != userId)
            {
                return Forbid();
            }


            var existingVenue = _venueService.GetVenueById(id);

            if (BannerFile != null && BannerFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                // Delete old banner if exists
                if (!string.IsNullOrEmpty(existingVenue.BannerPath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingVenue.BannerPath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new file
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BannerFile.CopyTo(stream);
                }

                venue.BannerPath = "/uploads/" + uniqueFileName;
            }
            else
            {
                // Keep old banner if no new file is uploaded
                venue.BannerPath = existingVenue.BannerPath;
            }


            _venueService.Update(venue);
            return RedirectToAction("Venues", "Profile", new { id = userId });
        }

        // GET: Venues/Delete/5
        public IActionResult Delete(Guid id)
        {
            var venue = _venueService.GetVenueById(id);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (venue.VenueManagerId != userId)
            {
                return Forbid();
            }

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var venue = _venueService.GetVenueById(id);


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (venue.VenueManagerId != userId)
            {
                return Forbid();
            }



            if (!string.IsNullOrEmpty(venue.BannerPath))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", venue.BannerPath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _venueService.DeleteById(id);

            return RedirectToAction("Venues", "Profile", new { id = userId });
        }
    }
}
