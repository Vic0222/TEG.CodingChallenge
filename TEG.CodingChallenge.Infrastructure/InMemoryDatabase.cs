using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TEG.CodingChallenge.Application.Configs;
using TEG.CodingChallenge.Domain.Models;

namespace TEG.CodingChallenge.Infrastructure
{
    public class InMemoryDatabase
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<EventDataSettings> _eventDataSettings;
        private readonly ILogger<InMemoryDatabase> _logger;

        private IEnumerable<Venue>? _venues;

        private IEnumerable<Event>? _events;

        public InMemoryDatabase(HttpClient httpClient, IOptions<EventDataSettings> eventDataSettings, ILogger<InMemoryDatabase> logger)
        {
            _httpClient = httpClient;
            _eventDataSettings = eventDataSettings;
            _logger = logger;
        }

        private async Task loadData(CancellationToken cancellationToken)
        {
            bool eventsIsLoaded = (_events?.Count() ?? 0) > 0;
            bool venuesIsLoaded = (_venues?.Count() ?? 0) > 0;
            if (eventsIsLoaded && venuesIsLoaded)
            {
                return; 
            }

            _logger.LogInformation("Downloading event data");

            var dataUrl = _eventDataSettings.Value.DataUrl;

            var dataResponse = await _httpClient.GetAsync(dataUrl, cancellationToken);
            dataResponse.EnsureSuccessStatusCode();

            JObject jObject = JObject.Parse(await dataResponse.Content.ReadAsStringAsync(cancellationToken));

            _events = jObject["events"]?.ToObject<List<Event>>() ?? Enumerable.Empty<Event>();
            _venues = jObject["venues"]?.ToObject<List<Venue>>() ?? Enumerable.Empty<Venue>();

            _logger.LogInformation("Downloading event data done.");
        }

        public async Task<IEnumerable<Venue>> GetVenues(CancellationToken cancellationToken = default)
        {
            await loadData(cancellationToken);
            return _venues ?? Enumerable.Empty<Venue>();
        }

        public async Task<IEnumerable<Event>> GetEvents(CancellationToken cancellationToken = default)
        {
            await loadData(cancellationToken);
            return _events ?? Enumerable.Empty<Event>();
        }
    }
}
