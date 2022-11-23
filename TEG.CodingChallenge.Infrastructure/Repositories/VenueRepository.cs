using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Domain.Repositories;

namespace TEG.CodingChallenge.Infrastructure.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly InMemoryDatabase _inMemoryDatabase;

        public VenueRepository(InMemoryDatabase inMemoryDatabase)
        {
            _inMemoryDatabase = inMemoryDatabase;
        }

        public async Task<Venue> GetVenueByIdAsync(int id, CancellationToken cancellationToken)
        {
            var venues = await _inMemoryDatabase.GetVenues(cancellationToken);
            return venues.FirstOrDefault(v => v.Id == id) ?? new Venue() {  };
        }

        public async Task<IEnumerable<Venue>> GetVenuesAsync(CancellationToken cancellationToken = default)
        {
            var venues = await _inMemoryDatabase.GetVenues(cancellationToken);
            return venues.OrderBy(v => v.Name).ToList();
        }

    }
}
