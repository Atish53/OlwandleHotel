using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class FinalizedBookingDetail
    { 
    [Key]
    public int FinalizedBookingDetailId { get; set; }    

    [Required]
    [DisplayName("Reservation Cost")]
    public int FinalCost { get; set; } //Amount to pay.

    public virtual FinalizedBooking FinalizedBooking { get; set; }
    public virtual ReservedBooking ReservedBooking { get; set; }


    }

    
}