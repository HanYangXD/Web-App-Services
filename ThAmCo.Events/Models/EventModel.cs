using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        [Required, MaxLength(3), MinLength(3)]
        public string TypeId { get; set; }


        public List<GuestBooking> GuestBookings { get; set; }

        public List<StaffBooking> StaffBookings { get; set; }

        public string VenueCode { get; set; }
    }

    public class EventDbContext : DbContext
    {
        public DbSet<EventModel> Events { get; set; }
    }
}
