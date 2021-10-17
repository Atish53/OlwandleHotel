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
    }
}