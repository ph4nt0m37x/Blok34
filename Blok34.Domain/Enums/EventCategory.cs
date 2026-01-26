using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.Enums
{
    public enum EventCategory
    {
        [Display(Name = "Art Exhibition")]
        ArtExhibition = 1,

        [Display(Name = "Beer Tasting")]
        BeerTasting,

        [Display(Name = "Board Game Night")]
        BoardGameNight,

        [Display(Name = "Career Fair")]
        CareerFair,

        Charity,

        [Display(Name = "Comedy Show")]
        ComedyShow,

        Concert,

        [Display(Name = "Community Gathering")]
        CommunityGathering,

        Conference,

        [Display(Name = "Cooking Class")]
        CookingClass,

        Cycling,

        [Display(Name = "Cultural Event")]
        CulturalEvent,

        [Display(Name = "DJ Set")]
        DJSet,

        ESports,

        Festival,

        [Display(Name = "Family Event")]
        FamilyEvent,

        [Display(Name = "Fitness Class")]
        FitnessClass,

        [Display(Name = "Food Tasting")]
        FoodTasting,

        [Display(Name = "Gaming Event")]
        GamingEvent,

        Hackathon,

        Hiking,

        [Display(Name = "Kids Workshop")]
        KidsWorkshop,

        [Display(Name = "Language Exchange")]
        LanguageExchange,

        Lecture,

        [Display(Name = "Live Music")]
        LiveMusic,

        Meetup,

        [Display(Name = "Movie Screening")]
        MovieScreening,

        Networking,

        [Display(Name = "Open Mic")]
        OpenMic,

        Other,

        Party,

        [Display(Name = "Product Launch")]
        ProductLaunch,

        Run,

        Seminar,

        [Display(Name = "Seasonal Event")]
        SeasonalEvent,

        [Display(Name = "Sports Event")]
        SportsEvent,

        [Display(Name = "Startup Event")]
        StartupEvent,

        [Display(Name = "Tech Talk")]
        TechTalk,

        Theater,

        Tournament,

        Training,

        [Display(Name = "Trivia Night")]
        TriviaNight,

        Wellness,

        Workshop,

        [Display(Name = "Wine Tasting")]
        WineTasting,

        Yoga
    }


}
