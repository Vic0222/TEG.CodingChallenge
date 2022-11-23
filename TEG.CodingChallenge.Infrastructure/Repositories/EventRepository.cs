using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Domain.Repositories;

namespace TEG.CodingChallenge.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly InMemoryDatabase _inMemoryDatabase;

        public EventRepository(InMemoryDatabase inMemoryDatabase)
        {
            _inMemoryDatabase = inMemoryDatabase;
        }

        public async Task<Event?> GetById(int id, CancellationToken cancellationToken)
        {
            var events = await _inMemoryDatabase.GetEvents(cancellationToken);
            return events.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId, CancellationToken cancellationToken)
        {
            var events = await _inMemoryDatabase.GetEvents(cancellationToken);
            return events.Where(e => e.VenueId == venueId).OrderBy(e => e.StartDate).ToList();
        }
    }
}
