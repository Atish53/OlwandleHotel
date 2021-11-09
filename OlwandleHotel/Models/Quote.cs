using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Quote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteId { get; set; }
                
        public string FirstName { get; set; }
                
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
                
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Number of Adults")]
        [Display(Name = "Number of Adults")]
        public int NumAdults { get; set; }

        [Required(ErrorMessage = "Please enter Number of Kids")]
        [Display(Name = "Number of Kids")]
        public int NumKids { get; set; }

        [Required(ErrorMessage = "What is your Departure Date?")]
        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "What is your Return Date?")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Tour Destinations:")]
        public TourList TourL { get; set; }
        public enum TourList
        {
            None,
            Ushaka,
            Kruger_National_Park,
            DurbanNaturalScienceMuseum,
            Cape_of_Good_Hope,
            GoldReefCityThemePark,
            Apartheid_Museum
        }

        [Display(Name = "Cruise Destinations:")]
        public CruiseList CruiseL { get; set; }
        public enum CruiseList
        {
            None,
            MSC,
            Princess,
            Costa,
            Royal

        }       

        [Display(Name = "Estimated Price")]
        public double estimatedPrice { get; set; }
    }
}