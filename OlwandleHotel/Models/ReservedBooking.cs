using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class ReservedBooking
    {
        [Key]
        public int ReservedBookingId { get; set; }

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

        public int RoomId { get; set; }

        public double ReservedCost { get; set; }

        public virtual Room Room { get; set; }
    }
}