using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class Location
    {
        public int ID { get; set; }
        public String LocationName { get; set; }
        public String LocationDescription { get; set; }
        public String Discrict { get; set; }
        public String Country { get; set; }
    }
}