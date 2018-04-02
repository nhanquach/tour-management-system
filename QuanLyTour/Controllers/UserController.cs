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
            ViewBag.error = "";
            IQueryable<User> result = null;
            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                result = from u in db.Users
                         where u.Email.Equals(email)
                         where u.Password.Equals(password)
                         select u;

                if(result.FirstOrDefault() != null)
                {
                    //Sign In successfully
                    return View("UserHome", result.ToList());
                } else
                {
                    ViewBag.error = "Email or Password not match.";
                }
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection formCollection, User user)
        {
            string name = formCollection["Name"];
            string email = formCollection["Email"];
            string password = formCollection["Password"];
            string rePassword = formCollection["Repeat_password"];
            bool checkPassword = (password == rePassword);
            if (checkPassword)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return View();
            } else
            {
                ViewBag.error = "Password and Repeat Password do not match.";
            }
            return View();
        }
    }
}