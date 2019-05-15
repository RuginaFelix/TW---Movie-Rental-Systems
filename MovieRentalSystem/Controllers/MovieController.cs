using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MovieRentalSystem.Models;
using MovieRentalSystem.Models.ViewClasses;

namespace MovieRentalSystem.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            Expression<Func<Movie, bool>> exp = t => t.Id != Guid.Empty;
            return View();
        }

        [HttpGet]
        public PartialViewResult GetAllRentedMovies()
        {
            var returnedModel = new List<MovieViewClass>();

            using (var context = new MovieRentalSystemEntities())
            {
                var movies = context.Movies.Where(movie => movie.IsRented == 0 || movie.IsRented == null);
                if(movies == null || movies.Count() == 0)
                {
                    return PartialView("~/Views/Home/Modals/_ModalBodyInchireaza.cshtml", returnedModel);
                }

                foreach (var movie in movies)
                {
                    returnedModel.Add(new MovieViewClass() {
                        IsRented = movie.IsRented.HasValue ? movie.IsRented.Value : 0,
                        Nume = movie.Name,
                        RenterId = movie.RenterId.HasValue ? movie.RenterId.Value : Guid.Empty
                    });
                }
                return PartialView("~/Views/Home/Modals/_ModalBodyInchireaza.cshtml", returnedModel);
            }
        }

        [HttpGet]
        public PartialViewResult Movie(string name)
        {
            var returnedModel = new List<MovieViewClass>();

            using (var context = new MovieRentalSystemEntities())
            {
                var movies = context.Movies.Where(t => t.Name.Contains(name));
                if (movies == null || movies.Count() == 0)
                {
                    return PartialView("~/Views/Home/Modals/_ModalBodyInchireaza.cshtml", returnedModel);
                }

                foreach (var movie in movies)
                {
                    returnedModel.Add(new MovieViewClass()
                    {
                        IsRented = movie.IsRented.HasValue ? movie.IsRented.Value : 0,
                        Nume = movie.Name,
                        RenterId = movie.RenterId.HasValue ? movie.RenterId.Value : Guid.Empty
                    });
                }
                return PartialView("~/Views/Home/Modals/_ModalBodyInchireaza.cshtml", returnedModel);
            }
        }

    }
}