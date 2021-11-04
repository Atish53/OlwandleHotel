using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Event Title")]
        public string Title { get; set; }

        [Display(Name = "Picture")]
        public byte[] EventPicture { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Event Venue")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Tickets Left: ")]
        public int TicketsRemaining { get; set; }

        public bool isActive { get; set; }

        public List<EventBooking> EventBookings { get; set; }

    }
}