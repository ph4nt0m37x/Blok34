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
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public List<Event> GetAllEvents()
        {
            return _eventRepository.GetAll(
                e => e,
                include: q => q.Include(e => e.Venue),
                orderBy: q => q.OrderByDescending(e => e.StartDate)
            ).ToList();
        }

        public Event? GetEventById(Guid id)
        {
            return _eventRepository.Get(
                e => e,
                e => e.Id == id,
                include: q => q.Include(e => e.Venue).Include(e => e.Attendees)
            );
        }

        public List<Event> SearchEvents(string query)
        {
            return _eventRepository.GetAll(
                e => e,
                e => e.Title.Contains(query) || e.Description.Contains(query),
                orderBy: q => q.OrderByDescending(e => e.StartDate),
                include: q => q.Include(e => e.Venue)
            ).ToList();
        }

        public Event Insert(Event e)
        {
            e.Id = Guid.NewGuid();
            return _eventRepository.Insert(e);
        }

        public Event Update(Event e)
        {
            return _eventRepository.Update(e);
        }

        public Event DeleteById(Guid id)
        {
            var e = GetEventById(id);
            if (e == null) throw new Exception("Event not found");

            return _eventRepository.Delete(e);
        }

        public List<Event> GetEventsByCreator(string userId)
        {
            return _eventRepository.GetAll(
                e => e,
                e => e.CreatedByUserId == userId,
                orderBy: q => q.OrderByDescending(e => e.StartDate)
            ).ToList();
        }
    }
}
