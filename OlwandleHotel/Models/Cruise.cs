using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Cruise
    {
        [Key]
        public int CruiseId { get; set; }
        public string CruiseTitle { get; set; }
        public byte[] CruisePicture { get; set; }
        public string CruiseName { get; set; }
        public string CruiseDestinations { get; set; }
        public string CruiseLocation { get; set; }
        public double CruisePrice { get; set; }
        public int CruiseTicketsRemaining { get; set; }
        public bool isActive { get; set; }
        public List<CruiseBooking> CruiseBookings { get; set; }
    }
}