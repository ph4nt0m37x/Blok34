using Blok34.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? AvatarPath { get; set; }
        public ICollection<EventAttendance>? EventsAttending { get; set; }
        public ICollection<Venue>? ManagedVenues { get; set; }


    }
}
