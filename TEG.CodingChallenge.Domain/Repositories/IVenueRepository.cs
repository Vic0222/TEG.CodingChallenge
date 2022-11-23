using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;

namespace TEG.CodingChallenge.Domain.Repositories
{
    public interface IVenueRepository
    {
        Task<Venue> GetVenueByIdAsync(int venueId, CancellationToken cancellationToken);

        Task<IEnumerable<Venue>> GetVenuesAsync(CancellationToken cancellationToken = default);
    }
}
