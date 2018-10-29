using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Venues.Data
{
    public class Venue
    {
        [Key, MinLength(5), MaxLength(5)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Capacity { get; set; }

        public List<Suitability> SuitableEventTypes { get; set; }

        public List<Availability> AvailableDates { get; set; }
    }
}
