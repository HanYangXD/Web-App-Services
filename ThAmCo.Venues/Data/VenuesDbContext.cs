using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Venues.Data
{
    public class VenuesDbContext : DbContext
    {
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Suitability> Suitabilities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        private readonly IHostingEnvironment _hostEnv;

        public VenuesDbContext(DbContextOptions<VenuesDbContext> options,
                               IHostingEnvironment env) : base(options)
        {
            _hostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.venues");

            builder.Entity<Suitability>()
                   .HasKey(s => new { s.EventTypeId, s.VenueCode });

            builder.Entity<EventType>()
                   .Property(e => e.Id)
                   .IsFixedLength();

            builder.Entity<EventType>()
                   .HasMany(e => e.SuitableVenues)
                   .WithOne(s => s.EventType)
                   .HasForeignKey(s => s.EventTypeId);

            builder.Entity<Venue>()
                   .Property(v => v.Code)
                   .IsFixedLength();

            builder.Entity<Venue>()
                   .HasMany(v => v.SuitableEventTypes)
                   .WithOne(s => s.Venue)
                   .HasForeignKey(s => s.VenueCode);

            builder.Entity<Venue>()
                   .HasMany(v => v.AvailableDates)
                   .WithOne(a => a.Venue)
                   .HasForeignKey(a => a.VenueCode);

            builder.Entity<Availability>()
                   .HasKey(a => new { a.Date, a.VenueCode });

            builder.Entity<Reservation>()
                   .Property(r => r.Reference)
                   .IsFixedLength();

            builder.Entity<Reservation>()
                   .HasOne(r => r.Availability)
                   .WithOne(a => a.Reservation)
                   .IsRequired()
                   // prevent an Availability being deleted if there's a Reservation
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EventType>()
                   .HasData(
                        new EventType { Id = "CON", Title = "Conference" },
                        new EventType { Id = "MET", Title = "Meeting" },
                        new EventType { Id = "PTY", Title = "Party" },
                        new EventType { Id = "WED", Title = "Wedding" }
                    );

            if (_hostEnv != null && _hostEnv.IsDevelopment())
            {
                builder.Entity<Venue>()
                       .HasData(
                            new Venue { Code = "CRKHL", Name = "Crackling Hall", Description = "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", Capacity = 150 },
                            new Venue { Code = "TNDMR", Name = "Tinder Manor", Description = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", Capacity = 450 },
                            new Venue { Code = "FDLCK", Name = "The Fiddler's Cockatoo", Description = "Rustic pub set in ideallic countryside, the original venue of a notorious local musician and his parrot.", Capacity = 85 }
                       );

                builder.Entity<Suitability>()
                       .HasData(
                            new Suitability { VenueCode = "CRKHL", EventTypeId = "WED" },
                            new Suitability { VenueCode = "CRKHL", EventTypeId = "CON" },
                            new Suitability { VenueCode = "CRKHL", EventTypeId = "PTY" },
                            new Suitability { VenueCode = "TNDMR", EventTypeId = "WED" },
                            new Suitability { VenueCode = "TNDMR", EventTypeId = "CON" },
                            new Suitability { VenueCode = "TNDMR", EventTypeId = "MET" },
                            new Suitability { VenueCode = "FDLCK", EventTypeId = "WED" },
                            new Suitability { VenueCode = "FDLCK", EventTypeId = "PTY" }
                       );

                var rand = new Random(0);
                var startDate = new DateTime(2018, 10, 28);
                var dates = new List<Availability>();
                var venues = new [] {
                    new { Venue = "FDLCK", Cost = 30.0 },
                    new { Venue = "CRKHL", Cost = 50.0 },
                    new { Venue = "TNDMR", Cost = 70.0 }
                }.ToList();
                venues.ForEach(v =>
                {
                    var more = Enumerable.Range(0, 90)
                        .Select(i => new Availability
                        {
                            VenueCode = v.Venue,
                            Date = startDate.AddDays(i),
                            CostPerHour = Math.Round(v.Cost * (1.0 + rand.NextDouble()), 2)
                        });
                    dates.AddRange(more);
                });
                var availabilities = dates.Where(d => rand.NextDouble() < 0.3)
                                          .OrderBy(d => d.Date)
                                          .ToArray();
                builder.Entity<Availability>()
                       .HasData(availabilities);
            }
        }
    }
}
