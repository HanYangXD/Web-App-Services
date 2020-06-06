using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Data
{
    public class StaffBooking
    {
        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}
