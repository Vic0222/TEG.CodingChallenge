using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Application.Contracts.Services;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Domain.Repositories;

namespace TEG.CodingChallenge.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IVenueRepository _venueRepository;

        public EventService(IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
        }

        public async Task<Event?> GetEvent(int id, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetById(id, cancellationToken);
            if (@event != null)
            {
                @event.Venue = await _venueRepository.GetVenueByIdAsync(@event.VenueId, cancellationToken);
            }

            return @event;
        }

        public  Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId, CancellationToken cancellationToken)
        {
            return _eventRepository.GetEventsByVenueIdAsync(venueId, cancellationToken);
           
        }
    }
}
