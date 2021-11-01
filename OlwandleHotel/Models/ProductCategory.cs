using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class ProductCategory
    {
        [Key]
        [DisplayName("Category ID")]
        public int CatergoryId { get; set; }

        [Display(Name = "Picture")]
        public byte[] CategoryPicture { get; set; }


        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}