using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class TourDetail
    {
        public int ID { get; set; }
        public int TourID { get; set; }
        public int LocationID { get; set; }
        
        public virtual Tour Tour { get; set; }
        public virtual Location Location { get; set; }
    }
}