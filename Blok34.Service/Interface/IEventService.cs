using Blok34.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Interface
{
    public interface IEventService
    {
        List<Event> GetAllEvents();
        Event? GetEventById(Guid id);

        List<Event> SearchEvents(string query);

        Event Insert(Event e);
        Event Update(Event e);
        Event DeleteById(Guid id);

        List<Event> GetEventsByCreator(string userId);
    }
}
