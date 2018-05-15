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
                    Session["UserEmail"] = result.Email;
                    Session["UserName"] = result.Name;
                    Session["UserID"] = result.ID;
                    return RedirectToAction("Home");
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
                // Get all booked tours
                int uId = int.Parse(Session["UserID"].ToString());
                
                var bookedTours = (
                    from bills in db.Bills
                    join tours in db.Tours
                    on bills.TourID equals tours.ID
                    join grs in db.TourGroups
                    on bills.TourGroupID equals grs.ID
                    where uId == bills.UserID
                    orderby grs.LeaveDate
                    select new BookedTour
                    {
                        ID = tours.ID,
                        Name = tours.TourName,
                        Price = bills.TourPrice,
                        Status = bills.Status,
                        LeaveDate = grs.LeaveDate,
                        ReturnDate = grs.ReturnDate,
                        NumberOfTickets = bills.NumberOfTicket,
                    }).Distinct().ToList();

                ViewBag.bookedTours = bookedTours;
                return View();
            }
            return RedirectToAction("SignIn");
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