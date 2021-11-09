using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }        

        [Display(Name = "Flight Types")]
        public FlightList FlightL { get; set; }
        public enum FlightList
        {
            Kulula,  //Cheapest
            BritishAirways,   //Most
            SAA      //Meh
        }

        [Display(Name = "Destinations")]
        public DestinationList DestinationL { get; set; }
        public enum DestinationList
        {
            Durban,
            Johannesburg,
            CapeTown
        }

        public string DateFlight { get; set; }
                
        public string DateReturn  { get; set; }

        public bool returnTicket { get; set; }

        public double TotalCost { get; set; }        

        public List<FlightBooking> FlightBookings { get; set; }

    }
}