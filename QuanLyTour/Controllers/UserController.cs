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
            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                var result = db.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password))
                    .FirstOrDefault();

                if(result != null)
                {
                    //Sign In successfully
                    //Save log in session
                    Session["UserEmail"] = result.Email;
                    Session["UserName"] = result.Name;
                    return View("UserHome", result);
                } else
                {
                    ViewBag.error = "Email or Password not match.";
                }
            }
            return View();
        }

        public ActionResult Home()
        {
            if(Session["UserEmail"] != null && Session["UserName"] != null)
            {
                return View("UserHome");
            }
            return View("SignIn");
        }

        public ActionResult LogOut()
        {
            Session["UserEmail"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
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
                return View("SignIn");
            } else
            {
                ViewBag.error = "Password and Repeat Password do not match.";
            }
            return View();
        }
    }
}