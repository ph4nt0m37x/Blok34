using Blok34.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Interface
{
    public interface IVenueService
    {
        List<Venue> GetAllVenues();
        Venue? GetVenueById(Guid id);

        List<Venue> SearchVenues(string query);

        Venue Insert(Venue venue);
        Venue Update(Venue venue);
        Venue DeleteById(Guid id);

        List<Venue> GetVenuesByOwner(string userId);
    }
}
