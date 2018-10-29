using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Venues.Data
{
    public class Suitability
    {
        public string EventTypeId { get; set; }

        public EventType EventType { get; set; }

        public string VenueCode { get; set; }

        public Venue Venue { get; set; }
    }
}
