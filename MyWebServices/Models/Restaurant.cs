using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebServices.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string NamaRestaurant { get; set; }
        public string Alamat { get; set; }
        public DateTime Tanggal { get; set; }
    }
}