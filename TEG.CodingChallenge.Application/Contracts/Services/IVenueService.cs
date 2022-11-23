using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG.CodingChallenge.Domain.Models;

namespace TEG.CodingChallenge.Application.Contracts.Services
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetAllVenuesAsync(CancellationToken cancellationToken = default);
    }
}
