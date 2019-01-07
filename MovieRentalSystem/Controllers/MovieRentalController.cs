using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class MovieRentalController : Controller
    {
        // GET: MovieRental
        public ActionResult Index()
        {
            return View();
        }
    }
}