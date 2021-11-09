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
        [Display(Name = "Hotel")]
        public string RoomLocation { get; set; } //Room Location

        [Required]
        [Display(Name = "Address")]
        public string RoomAddress { get; set; } //Room Address


        [Required]
        [DisplayName("Room Number")]
        public int RoomNumber { get; set; }

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

        [Required]
        [DisplayName("Cost")]
        public double Cost { get; set; } //Number of living rooms in this type of room.
            
        public int RoomsAvailable { get; set; }

        public bool isActive { get; set; }

        public virtual List<Room> Rooms { get; set; }       
    }
}