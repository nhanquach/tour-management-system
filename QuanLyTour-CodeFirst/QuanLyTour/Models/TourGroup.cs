using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class TourGroup
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime LeaveDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TourID { get; set; }
        public int Status { get; set; }
    }
}