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

        [Required]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date and Start Time")]
        [DataType(DataType.DateTime)]

        public DateTime Start { get; set; }
        [Required]
        [Display(Name = "Date and End Time")]


        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Event Venue")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Event Priority")]
        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }
    }
}