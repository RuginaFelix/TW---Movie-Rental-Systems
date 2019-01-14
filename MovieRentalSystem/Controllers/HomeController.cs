using MovieRentalSystem.Models;
using MovieRentalSystem.Models.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        public static List<Guid> MoviesList { get; set; }

        public ActionResult Index()
        {
            if(MoviesList != null)
            {
                MoviesList.Clear();
            }
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

        public void SaveItemsList(List<Guid> movieIds)
        {
            MoviesList = movieIds;
        }

        public ActionResult GoToCart()
        {
            List<MovieViewClass> movieList = new List<MovieViewClass>();
            using (var context = new MovieRentalSystemEntities())
            {
                if (MoviesList != null && MoviesList.Count > 0)
                {
                    foreach (var id in MoviesList)
                    {
                        var currentMovie = context.Movies.Single(movie => movie.Id == id);
                        var viewMovie = new MovieViewClass()
                        {
                            Id = currentMovie.Id,
                            Nume = currentMovie.Name,
                            //PozaURL = currentMovie.UrlPoza,
                            IsRented = currentMovie.IsRented ?? 0,
                        };
                        movieList.Add(viewMovie);
                    }
                }

                return View("~/Views/Home/Cart.cshtml", movieList);
            }
        }

    }
}