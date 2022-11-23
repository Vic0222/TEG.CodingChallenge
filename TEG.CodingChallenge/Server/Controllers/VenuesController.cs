using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEG.CodingChallenge.Application.Contracts.Services;
using TEG.CodingChallenge.Application.Services;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Shared.Dtos;

namespace TEG.CodingChallenge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVenueService _venueService;
        private readonly IEventService _eventService;
        private readonly ILogger<VenuesController> _logger;

        public VenuesController(IMapper mapper, IVenueService venueService, IEventService eventService, ILogger<VenuesController> logger)
        {
            _mapper = mapper;
            _venueService = venueService;
            _eventService = eventService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the venues
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(IEnumerable<VenueSummaryDto>))]
        public async Task<IActionResult> GetVenues(CancellationToken cancellationToken = default)
        {
            var venues = await _venueService.GetAllVenuesAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<VenueSummaryDto>>(venues));
        }

        /// <summary>
        /// Gets all the events of the specified vendue id
        /// </summary>
        /// <param name="venueId">The id of the venue</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(EventSummaryDto))]
        [HttpGet("{venueId}/Events")]
        public async Task<IActionResult> GetVenueEvents(int venueId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting events for venue with id {venueId}", venueId);

            var events = await _eventService.GetEventsByVenueIdAsync(venueId, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<EventSummaryDto>>(events));
        }
    }
}
