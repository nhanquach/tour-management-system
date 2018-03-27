using QuanLyTour.DAL;
using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class UserController : Controller
    {
        private TourContext db = new TourContext();
        public ActionResult SignIn(String email, String password)
        {
            IQueryable<User> result = null;
            if(!String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                result = from user in db.Users
                         where user.Email.Equals(email)
                         where user.Password.Equals(password)
                         select user;
            }

            if(result.ToList().Count > 0)
            {
                //Sign In successfully
            }
            return View();
        }
    }
}