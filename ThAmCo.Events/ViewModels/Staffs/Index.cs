using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.Staffs
{
    public class Index
    {
        public Index(List<Data.StaffBooking> staffList, int staffId, int eventId)
        {
            StaffList = staffList;
            StaffId = staffId;
            EventId = eventId;
        }

        public List<Data.StaffBooking> StaffList { get; set; }
        public int StaffId { get; set; }
        public int EventId { get; set; }
    }
}
