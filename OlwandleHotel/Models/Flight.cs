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

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        public string DateFlight { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        public string DateReturn  { get; set; }

        public bool returnTicket { get; set; }

        public double TotalCost { get; set; }

        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //
        public string BoardDateAndTime { get; set; } //
        public string TicketNumber { get; set; } //     

    }
}