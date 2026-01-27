using Blok34.Domain.DomainModels;
using Blok34.Domain.DTO;
using Blok34.Domain.Identity;
using Blok34.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Blok34.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {

                var emptySearch = new SearchDTO
                {
                    Query = query,
                    Users = new List<ApplicationUser>(),
                    Events = new List<Event>(),
                    Venues = new List<Venue>()
                };
                return View(emptySearch);

            }

            var results = _searchService.Search(query);
            return View(results);
        }
    }
}
