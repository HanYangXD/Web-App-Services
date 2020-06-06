using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Key, MinLength(13), MaxLength(13)]
        [Display(Name = "Reservation Reference")]
        public string Reference { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Venue Code")]
        public string VenueCode { get; set; }

        [Display(Name = "Reservation Made")]
        public DateTime WhenMade { get; set; }

        [Required]
        [Display(Name = "Reservation Staff")]
        public string StaffId { get; set; }
    }
}
