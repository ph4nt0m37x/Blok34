using Blok34.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DomainModels
{
    public class Event : BaseEntity
    {

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid VenueId { get; set; }
        [Required]
        public Venue? Venue { get; set; }
        [Required]
        public EventCategory Category { get; set; }
        public string? CreatedByUserId { get; set; }
        public virtual ICollection<EventAttendance>? Attendees { get; set; }

    }
}
