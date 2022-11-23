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
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesAsync(CancellationToken cancellationToken)
        {
            return await _venueRepository.GetVenuesAsync(cancellationToken);
        }
    }
}
