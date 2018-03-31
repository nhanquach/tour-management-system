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
            if(!String.IsNullOrEmpty(email) || !String.IsNullOrEmpty(password))
            {
                result = from user in db.Users
                         where user.Email.Equals(email)
                         where user.Password.Equals(password)
                         select user;

                if(result.FirstOrDefault() != null)
                {
                    //Sign In successfully
                    return View(result);
                } else
                {
                    ViewBag.error = "Email or Password not match.";
                }
            }

            return View();
        }
    }
}