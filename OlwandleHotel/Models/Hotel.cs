using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Hotel
    {
        [Key]
        [Display(Name = "HotelId")]
        public int HotelId { get; set; }

        [Display(Name = "Picture")]
        public byte[] HotelPicture { get; set; }

        public string HotelLocation { get; set; }

        public string HotelName { get; set; }

        public string HotelDescription { get; set; }

        public virtual Room Rooms { get; set; }
        
    }
}