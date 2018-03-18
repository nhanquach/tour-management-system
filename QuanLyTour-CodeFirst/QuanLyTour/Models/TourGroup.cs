using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public enum TStatus
    {
        Delayed, Scheduled, Closed, Cancel
    }

    public class TourGroup
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime LeaveDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TourID { get; set; }
        public virtual TStatus Status { get; set; }
        public int NumberOfPeople { get; set; }

        public virtual Tour Tour { get; set; }

        public virtual ICollection<TourGroupDetail> TourGroupDetails { get; set; }
    }
}