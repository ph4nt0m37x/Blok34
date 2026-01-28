using Blok34.Domain.DomainModels;
using Blok34.Repository.Interface;
using Blok34.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Implementation
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Venue> _venueRepository;

        public VenueService(IRepository<Venue> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public List<Venue> GetAllVenues()
        {
            return _venueRepository.GetAll(
                v => v,
                orderBy: q => q.OrderBy(v => v.Name)
            ).ToList();
        }

        public Venue? GetVenueById(Guid id)
        {
            return _venueRepository.Get(
            v => v,
            v => v.Id == id,
            include: q => q.Include(v => v.Events).ThenInclude(e => e.Attendees)
    );
        }
        public List<Venue> SearchVenues(string query)
        {
            return _venueRepository.GetAll(
                v => v,
                v => v.Name.Contains(query) || v.Address.Contains(query),
                orderBy: q => q.OrderBy(v => v.Name)
            ).ToList();
        }

        public Venue Insert(Venue venue)
        {
            venue.Id = Guid.NewGuid();
            return _venueRepository.Insert(venue);
        }

        public Venue Update(Venue venue)
        {
            return _venueRepository.Update(venue);
        }

        public Venue DeleteById(Guid id)
        {
            var venue = GetVenueById(id);
            if (venue == null) throw new Exception("Venue not found");

            return _venueRepository.Delete(venue);
        }

        public List<Venue> GetVenuesByOwner(string userId)
        {
            return _venueRepository.GetAll(
                v => v,
                v => v.VenueManagerId == userId
            ).ToList();
        }
    }
}
