using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Tour
    {
        [Key]
        public int TourId { get; set; }
        public string TourTitle { get; set; }
        public byte[] TourPicture { get; set; }
        public string TourName { get; set; }
        public string TourDestinations { get; set; }
        public string TourLocation { get; set; }
        public double TourPrice { get; set; }
        public int TourTicketsRemaining { get; set; }
        public bool isActive { get; set; }
        public List<TourBooking> TourBookings { get; set; }
    }
}