using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; } //Room Number              

        [Display(Name = "Picture")]
        public byte[] RoomPicture { get; set; }

        [Required]        
        public string RoomLocation { get; set; } //Room Location

        [Required]
        [DisplayName("Room Status")]
        public bool RoomStatus { get; set; }

        [Required]
        [DisplayName("Sleeps")]
        public int MaxOccupants { get; set; } //Number of people able to stay in this type of room.

        [Required]
        [DisplayName("Beds")]
        public int NumBeds { get; set; } //Number of beds in this type of room.

        [Required]
        [DisplayName("Bathrooms")]
        public int NumBaths { get; set; } //Number of baths in this type of room.

        [Required]
        [DisplayName("Living Rooms")]
        public int NumLivingRooms { get; set; } //Number of living rooms in this type of room.


        public double Cost()
        {
            double FinalCost = NumBeds * 300 + NumBaths * 150 + NumLivingRooms * 400;
            return FinalCost;
        } 
            
        public int RoomsAvailable { get; set; }

        public bool isActive { get; set; }

        public virtual List<ReservedBooking> ReservedBookings { get; set; }       
    }
}