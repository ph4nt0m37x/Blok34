using Blok34.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DomainModels
{
    public class Venue : BaseEntity
    {
        [Required]
        public String? Name {  get; set; }
        [Required]
        public VenueCategory Category { get; set; }

        [Required]
        public String? Description { get; set; }
        [Required]
        public String? Address { get; set; }
        public string? BannerPath { get; set; }
        [Required]
        public String? Phone { get; set; }
        public bool IsPublic { get; set; } = true;
        public string? VenueManagerId { get; set; }
        public virtual ICollection<Event>? Events { get; set; }

    }
}
