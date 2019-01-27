using MovieRentalSystem.Models;
using MovieRentalSystem.Models.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autentification(MovieRentalSystem.Models.User model)
        {
            using (var context = new MovieRentalSystemEntities())
            {
                var s = context.Users.Where(usr => usr.Email == model.Email && usr.Parola == model.Parola).FirstOrDefault();
                if(s != null)
                {
                    GlobalClass.UserId = s.Id;
                    GlobalClass.UserType = s.UserType != null? s.UserType.Value : 0;
                    GlobalClass.UserEmail = s.Email;
                }
                if (s == null)
                {
                    ViewBag.NotValidUser = "Email sau parola gresita";
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}