using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.Enums
{
    public enum VenueCategory
    {
        Bar = 1,

        Cafe,

        Cinema,

        Club,

        [Display(Name = "Community Center")]
        CommunityCenter,

        [Display(Name = "Concert Hall")]
        ConcertHall,

        [Display(Name = "Coworking Space")]
        CoworkingSpace,

        [Display(Name = "Cultural Center")]
        CulturalCenter,

        Gallery,

        Garden,

        Hotel,

        Library,

        Lounge,

        Museum,

        [Display(Name = "Open Air Stage")]
        OpenAirStage,

        Park,

        Pub,

        Rooftop,

        Square,

        Stadium,

        Theater,

        University,

        Other
    }


}
