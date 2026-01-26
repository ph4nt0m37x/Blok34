using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blok34.Domain.Identity;
using Blok34.Domain.DomainModels;

namespace Blok34.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events {  get; set; }
        public virtual DbSet<Venue> Venues {  get; set; }
        public virtual DbSet<EventAttendance> Attendances { get; set; }

    }
}
