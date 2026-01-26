using Blok34.Domain.DomainModels;
using Blok34.Domain.Enums;
using Blok34.Repository;
using Blok34.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Implementation
{
    public class EventAttendanceService : IEventAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public EventAttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AttendEvent(Guid eventId, string userId)
        {
            var existing = _context.Attendances
                .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (existing == null)
            {
                _context.Attendances.Add(new EventAttendance
                {
                    EventId = eventId,
                    UserId = userId,
                    Status = AttendanceStatus.Attending
                });
            }
            else
            {
                existing.Status = AttendanceStatus.Attending;
                _context.Attendances.Update(existing);
            }

            _context.SaveChanges();
        }

        public void MarkInterested(Guid eventId, string userId)
        {
            var existing = _context.Attendances
                .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (existing == null)
            {
                _context.Attendances.Add(new EventAttendance
                {
                    EventId = eventId,
                    UserId = userId,
                    Status = AttendanceStatus.Interested
                });
            }
            else
            {
                existing.Status = AttendanceStatus.Interested;
                _context.Attendances.Update(existing);
            }

            _context.SaveChanges();
        }

        public void RemoveAttendance(Guid eventId, string userId)
        {
            var existing = _context.Attendances
                .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (existing != null)
            {
                _context.Attendances.Remove(existing);
                _context.SaveChanges();
            }
        }

        public List<Event> GetAttendingEvents(string userId)
        {
            return _context.Attendances
                .Include(ea => ea.Event)
                .ThenInclude(ev => ev.Venue)
                .Where(x => x.UserId == userId && x.Status == AttendanceStatus.Attending)
                .Select(x => x.Event)
                .ToList();
        }

        public List<Event> GetInterestedEvents(string userId)
        {
            return _context.Attendances
                .Include(ea => ea.Event)
                .ThenInclude(ev => ev.Venue)
                .Where(x => x.UserId == userId && x.Status == AttendanceStatus.Interested)
                .Select(x => x.Event)
                .ToList();
        }

        public bool IsAttending(Guid eventId, string userId)
        {
            return _context.Attendances.Any(x => x.EventId == eventId && x.UserId == userId && x.Status == AttendanceStatus.Attending);
        }

        public bool IsInterested(Guid eventId, string userId)
        {
            return _context.Attendances.Any(x => x.EventId == eventId && x.UserId == userId && x.Status == AttendanceStatus.Interested);
        }
    }
}
