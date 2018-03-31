using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class Tour
    {
        public int ID { get; set; }
        public int TourID { get; set; }
        public String TourName { get; set; }
        public String TourDescription { get; set; }
        public String TourPrice { get; set; }

        public virtual ICollection<TourDetail> TourDetails { get; set; }
        public virtual ICollection<TourGroup> TourGroups { get; set; }
    }
}