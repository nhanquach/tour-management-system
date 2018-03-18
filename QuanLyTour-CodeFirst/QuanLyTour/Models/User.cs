using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class User
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}