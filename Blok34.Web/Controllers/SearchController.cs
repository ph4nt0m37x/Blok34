using Blok34.Domain.DTO;
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
                return View(new List<SearchDTO>());

            var results = _searchService.Search(query);
            return View(results);
        }
    }
}
