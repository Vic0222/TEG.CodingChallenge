using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;

namespace TEG.CodingChallenge.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event?> GetById(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Event>> GetEventsByVenueIdAsync(int venueId, CancellationToken cancellationToken);
    }
}
