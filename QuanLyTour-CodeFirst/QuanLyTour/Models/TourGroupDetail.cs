using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class TourGroupDetail
    {
        public int ID { get; set; }
        public int TourGroup { get; set; }
        //public int CustomerID { get; set; }
        //Because 1 TourGroup can have many Customers
        public virtual ICollection<Customer> Customers { get; set; }
    }
}