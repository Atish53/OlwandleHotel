using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class FinalizedBooking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 10)]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 10)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Invalid Identity Number.", MinimumLength = 13)]
        [Display(Name = "Identity Number")]
        public string IDNumber { get; set; }

        public virtual ReservedBooking ReservedBooking { get; set; }

        public List<FinalizedBookingDetail> FinalizedBookingDetails { get; set; }
    }
}