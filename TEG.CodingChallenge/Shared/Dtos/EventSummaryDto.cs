using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG.CodingChallenge.Shared.Dtos
{
    public class EventSummaryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset StartDate { get; set; }
    }
}
