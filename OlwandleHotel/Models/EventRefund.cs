using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class EventRefund
    {
        [Key]
        public int RefundId { get; set; }
        public int EventBookingId { get; set; }
        public string RefundReason { get; set; }
        public string RefundStatus { get; set; }
        public bool isRefundActive { get; set; }

        public virtual EventBooking EventBooking { get; set; }
    }
}