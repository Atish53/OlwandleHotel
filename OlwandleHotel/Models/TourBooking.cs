using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class TourBooking
    {
        [Key]
        public int TourBookingId { get; set; }
        public int TourId { get; set; }
        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //
        public string AttendDateAndTime { get; set; } //
        public string TicketNumber { get; set; } //
        public virtual Tour Tour { get; set; } //
    }
}