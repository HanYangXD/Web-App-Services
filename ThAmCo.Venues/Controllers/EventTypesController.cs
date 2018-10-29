using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;

namespace ThAmCo.Venues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypesController : ControllerBase
    {
        private readonly VenuesDbContext _context;

        public EventTypesController(VenuesDbContext context)
        {
            _context = context;
        }

        // GET: api/EventTypes
        [HttpGet]
        public async Task<IActionResult> GetEventTypes()
        {
            var dto = await _context.EventTypes
                                    .Select(e => new
                                    {
                                        e.Id,
                                        e.Title
                                    }).ToListAsync();
            return Ok(dto);
        }
    }
}
