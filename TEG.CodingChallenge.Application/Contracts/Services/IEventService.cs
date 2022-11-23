using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;

namespace TEG.CodingChallenge.Application.Contracts.Services
{
    public interface IEventService
    {
        Task<Event?> GetEvent(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId, CancellationToken cancellationToken);
    }
}
