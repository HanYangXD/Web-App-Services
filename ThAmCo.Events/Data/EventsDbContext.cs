﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace ThAmCo.Events.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestBooking> Guests { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffBooking> StaffBookings { get; set; }

        private IHostingEnvironment HostEnv { get; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options,
                               IHostingEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.events");

            builder.Entity<GuestBooking>()
                   .HasKey(b => new { b.CustomerId, b.EventId });

            builder.Entity<Customer>()
                   .HasMany(c => c.Bookings)
                   .WithOne(b => b.Customer)
                   .HasForeignKey(b => b.CustomerId);

            builder.Entity<Event>()
                   .HasMany(e => e.Bookings)
                   .WithOne(b => b.Event)
                   .HasForeignKey(b => b.EventId);

            builder.Entity<Event>()
                   .Property(e => e.TypeId)
                   .IsFixedLength();

            builder.Entity<StaffBooking>()
                    .HasKey(b => new { b.StaffId, b.EventId });

            builder.Entity<Staff>()
                    .HasMany(a => a.StaffBooking)
                    .WithOne(b => b.Staff)
                    .HasForeignKey(c => c.StaffId);

            // seed data for debug / development testing
            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                builder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Surname = "Robertson", FirstName = "Robert", Email = "bob@example.com" },
                    new Customer { Id = 2, Surname = "Thornton", FirstName = "Betty", Email = "betty@example.com" },
                    new Customer { Id = 3, Surname = "Jellybeans", FirstName = "Jin", Email = "jin@example.com" }
                );

                builder.Entity<Event>().HasData(
                    new Event { Id = 1, Title = "Bob's Big 50", Date = new DateTime(2016, 4, 12), Duration = new TimeSpan(6, 0, 0), TypeId = "PTY" },
                    new Event { Id = 2, Title = "Best Wedding Yet", Date = new DateTime(2018, 12, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "WED" }
                );

                builder.Entity<GuestBooking>().HasData(
                    new GuestBooking { CustomerId = 1, EventId = 1, Attended = true },
                    new GuestBooking { CustomerId = 2, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 1, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 3, EventId = 2, Attended = false }
                );
                builder.Entity<Staff>().HasData(
                    new Staff { StaffId = 1, Surname = "Choo", FirstName = "Han Yang", Email = "hyhy@fcuc.com", FirstAid = true },
                    new Staff { StaffId = 2, Surname = "Law", FirstName = "Ben Ben", Email = "Benben@fcuc.com", FirstAid = false },
                    new Staff { StaffId = 3, Surname = "Nero", FirstName = "Padoru", Email = "Padoru@fcuc.com", FirstAid = true }
                );

                builder.Entity<StaffBooking>().HasData(
                    new StaffBooking { StaffId = 1, EventId = 1 },
                    new StaffBooking { StaffId = 2, EventId = 1 },
                    new StaffBooking { StaffId = 1, EventId = 2 },
                    new StaffBooking { StaffId = 3, EventId = 2 }
                );
            }
        }
    }
}
