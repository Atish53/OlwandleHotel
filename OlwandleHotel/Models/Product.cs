using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Picture")]
        public byte[] ProductPicture { get; set; }

        public string ProductName { get; set; }
        public int ProductStock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between R0.01 and R99999.99")]
        public decimal Price { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }


    }
}