using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Venues.Data
{
    public class EventType
    {
        [MinLength(3), MaxLength(3)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Suitability> SuitableVenues { get; set; }
    }
}
