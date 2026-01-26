using Blok34.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Interface
{
    public interface IEventAttendanceService
    {
        void AttendEvent(Guid eventId, string userId);
        void MarkInterested(Guid eventId, string userId);
        void RemoveAttendance(Guid eventId, string userId);
        List<Event> GetAttendingEvents(string userId);
        List<Event> GetInterestedEvents(string userId);
        bool IsAttending(Guid eventId, string userId);
        bool IsInterested(Guid eventId, string userId);
    }
}
