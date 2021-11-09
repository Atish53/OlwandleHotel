using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class RoomBooking
    {
        [Key]
        public int RoomBookingId { get; set; }
        public int RoomId { get; set; } //
        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //

        public string CheckoutDate { get; set; }
        public string CheckinDate { get; set; }

        public string InvoiceNumber { get; set; } //

        public virtual Room Room { get; set; }
    }
}