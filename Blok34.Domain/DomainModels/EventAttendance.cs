using Blok34.Domain.Enums;
using Blok34.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DomainModels
{
    public class EventAttendance : BaseEntity
    {
        [Required]
        public Guid EventId { get; set; }
        public Event? Event { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public AttendanceStatus Status { get; set; }

    }
}
