using MovieRentalSystem.Models;
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
    }
}