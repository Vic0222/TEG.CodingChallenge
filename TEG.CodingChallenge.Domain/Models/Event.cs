using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG.CodingChallenge.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; }
    }

}
