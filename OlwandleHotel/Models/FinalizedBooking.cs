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
    }
}