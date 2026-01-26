using Blok34.Domain.DomainModels;
using Blok34.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DTO
{
    public class SearchDTO
    {
        public string Query { get; set; }

        public List<ApplicationUser> Users { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<Venue> Venues { get; set; } = new();
    }
}
