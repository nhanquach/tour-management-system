using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String IdNumber { get; set; }
        public String Address { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }
    }
}