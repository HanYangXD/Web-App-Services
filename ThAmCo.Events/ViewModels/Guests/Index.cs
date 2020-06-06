using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.Guests
{
    public class Index
    {
        public Index(List<Data.GuestBooking> customerList, int customerId, int eventId, string customername,string customersurname, string eventtitle)
        {
            CustomerList = customerList;
            CustomerId = customerId;
            EventId = eventId;
            CustomerName = customername;
            EventTitle = eventtitle;
            CustomerSurname = customersurname;
            //Attended = attended;
        }

        public List<Data.GuestBooking> CustomerList { get; set; }

        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public bool Attended { get; set; }

        public string CustomerName { get; set; }

        public string EventTitle { get; set; }

        public string CustomerSurname { get; set; }


    }
}
