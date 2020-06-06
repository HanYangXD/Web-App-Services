using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Views.StaffBookings
{
    public class StaffBookingsController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffBookingsController(EventsDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> StaffFilteredIndex(int id/*, string customerName, string customerSurname*/)
        {
            var eventsDbContext = _context.StaffBookings
                .Include(t => t.Staff)
                .Include(t => t.Event)
                .Where(t => t.StaffId == id);

            var indexVm = new ThAmCo.Events.ViewModels.Staffs.Index(
                await eventsDbContext.ToListAsync(),
                id,
                0);

            return View("StaffIndex", indexVm);
        }


        // GET: StaffBookings
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.StaffBookings.Include(s => s.Event).Include(s => s.Staff);
            return View(await eventsDbContext.ToListAsync());
        }

        // GET: StaffBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffBooking = await _context.StaffBookings
                .Include(s => s.Event)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffBooking == null)
            {
                return NotFound();
            }

            return View(staffBooking);
        }

        // GET: StaffBookings/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email");
            return View();
        }

        // POST: StaffBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,EventId")] StaffBooking staffBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffBooking.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffBooking.StaffId);
            return View(staffBooking);
        }

        // GET: StaffBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffBooking = await _context.StaffBookings.FindAsync(id);
            if (staffBooking == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffBooking.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffBooking.StaffId);
            return View(staffBooking);
        }

        // POST: StaffBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,EventId")] StaffBooking staffBooking)
        {
            if (id != staffBooking.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffBookingExists(staffBooking.StaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffBooking.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffBooking.StaffId);
            return View(staffBooking);
        }

        // GET: StaffBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffBooking = await _context.StaffBookings
                .Include(s => s.Event)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffBooking == null)
            {
                return NotFound();
            }

            return View(staffBooking);
        }

        // POST: StaffBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffBooking = await _context.StaffBookings.FindAsync(id);
            _context.StaffBookings.Remove(staffBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffBookingExists(int id)
        {
            return _context.StaffBookings.Any(e => e.StaffId == id);
        }
    }
}
