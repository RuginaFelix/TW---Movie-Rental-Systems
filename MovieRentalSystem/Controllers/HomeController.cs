using MovieRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new MovieRentalSystemEntities())
            {
                var returnedList = new List<Movie>();
                var moviesList = context.Movies;

                foreach (var item in moviesList)
                {
                    returnedList.Add(item);
                }

                if (returnedList == null)
                {
                    return View(new List<Movie>());
                }

                return View(returnedList);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}