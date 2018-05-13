using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public enum BillStatus
    {
        Pending, Complete, Cancel
    }

    public class Bill
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TourGroupID { get; set; }
        public int TourID { get; set; }
        public String TourPrice { get; set; }
        public int NumberOfTicket { get; set; }

        public BillStatus Status { get; set; }

        public virtual User User { get; set; }
    }
}