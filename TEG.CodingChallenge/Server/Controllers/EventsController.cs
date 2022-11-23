using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TEG.CodingChallenge.Application.Contracts.Services;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Shared.Dtos;

namespace TEG.CodingChallenge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IMapper mapper, IEventService eventService, ILogger<EventsController> logger)
        {
            _mapper = mapper;
            _eventService = eventService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a single event.
        /// </summary>
        /// <param name="id">The id of the event.</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(EventDetailsDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting event details for event with id {eventId}", id);

            var @event = await _eventService.GetEvent(id, cancellationToken);

            if (@event == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EventDetailsDto>(@event));
        }
    }
}
