using Blok34.Domain.DTO;
using Blok34.Repository;
using Blok34.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blok34.Service.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public SearchDTO Search(string query)
        {
            query = query.ToLower();

            return new SearchDTO
            {
                Query = query,

                Users = _context.Users
                    .Where(u => u.UserName.ToLower().Contains(query) || u.Name.ToLower().Contains(query))
                    .ToList(),


                Events = _context.Events
                    .Where(e =>
                        e.Title.ToLower().Contains(query) ||
                        e.Description.ToLower().Contains(query))
                    .Include(e => e.Venue)
                    .ToList(),

                Venues = _context.Venues
                    .Where(v =>
                        v.Name.ToLower().Contains(query) ||
                        v.Address.ToLower().Contains(query))
                    .ToList()
            };
        }

    }
}
