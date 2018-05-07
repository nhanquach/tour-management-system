using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class User
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Address { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}