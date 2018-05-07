using QuanLyTour.DAL;
using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Controllers.Utils;

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
                var hashedPassword = MD5.Create(password);

                System.Diagnostics.Debug.WriteLine("Hashed Password: ",hashedPassword);

                var result = db.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(hashedPassword))
                    .FirstOrDefault();

                if(result != null)
                {
                    //Sign In successfully
                    //Save log in session
                    Session["UserEmail"] = result.Email;
                    Session["UserName"] = result.Name;
                    Session["UserID"] = result.ID;
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
            string hashedPassword = MD5.Create(formCollection["Password"]);
            string hashedRePassword = MD5.Create(formCollection["Repeat_password"]);
            string password = hashedPassword;
            string rePassword = hashedRePassword;
            System.Diagnostics.Debug.WriteLine("Sign up Password ", password);

            bool checkPassword = (password == rePassword);
            if (checkPassword)
            {
                user.Password = hashedPassword;
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