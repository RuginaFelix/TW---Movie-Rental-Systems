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
            GlobalClass.CartCounter = MoviesList.Count();
        }

        public ActionResult GoToCart()
        {
            ViewBag.IsForMyMovies = false;
            List<MovieViewClass> movieList = new List<MovieViewClass>();
            return RefreshMovies(movieList);
        }

        public ActionResult RemoveItemFromCart(List<Guid> filmIds)
        {
            foreach (var id in filmIds)
            {
                MoviesList.Remove(id);
                GlobalClass.CartCounter = MoviesList.Count();
            }
            return RefreshMovies();
        }

        public ActionResult RemoveMovieFromUser(List<Guid> filmIds)
        {
            using (var context = new MovieRentalSystemEntities())
            {
                var myId = GlobalClass.UserId;
                var myself = context.Users.Single(t => t.Id == myId);
                if(myself == null)
                {
                    return null;
                }

                foreach (var item in filmIds)
                {
                    var movieToRemove = myself.Movies.Single(t => t.Id == item);
                    if(movieToRemove == null)
                    {
                        continue;
                    }

                    movieToRemove.RenterId = null;
                    movieToRemove.IsRented = 0;
                    myself.Movies.Remove(movieToRemove);
                }
                context.SaveChanges();

                return RefreshMovies();
            }
        }

        public ActionResult GoToMyMovies()
        {
            ViewBag.IsForMyMovies = true;
            List<MovieViewClass> movieList = new List<MovieViewClass>();
            var myId = GlobalClass.UserId;

            using (var context = new MovieRentalSystemEntities())
            {
                var my = context.Users.Single(usr => usr.Id == myId);
                if (my == null)
                {
                    return View("~/Views/Home/Cart.cshtml", movieList);
                }

                if (my.Movies != null && my.Movies.Count > 0)
                {
                    foreach (var movie in my.Movies)
                    {
                        var viewMovie = new MovieViewClass()
                        {
                            Id = movie.Id,
                            Nume = movie.Name,
                            PozaURL = movie.UrlPicture,
                            IsRented = movie.IsRented ?? 0,
                        };
                        movieList.Add(viewMovie);
                    }
                }

                return View("~/Views/Home/Cart.cshtml", movieList);
            }
        }

        public string SaveMovies(string MovieIds)
        {
            using (var context = new MovieRentalSystemEntities())
            {
                var my = context.Users.Single(usr => usr.Id == GlobalClass.UserId);
                if (my == null)
                {
                    return "not ok";
                }

                var movieSmtg = MovieIds.Split(',');
                var MovieListGood = new List<Guid>();

                for (int i = 0; i < movieSmtg.Length; i++)
                {
                    movieSmtg[i] = movieSmtg[i].Trim(new Char[] { '"', ']', '[', '\"' });

                    MovieListGood.Add(Guid.Parse(movieSmtg[i]));
                }

                foreach (var item in MovieListGood)
                {
                    var movieToAdd = context.Movies.Single(t => t.Id == item);
                    if (movieToAdd == null)
                    {
                        continue;
                    }
                    movieToAdd.IsRented = 1;
                    movieToAdd.RenterId = my.Id;
                    my.Movies.Add(movieToAdd);
                }
                context.SaveChanges();
                MoviesList.Clear();
                GlobalClass.CartCounter = MoviesList.Count();
                return "ok";
            }
        }

        private ActionResult RefreshMovies(List<MovieViewClass> movieList = null)
        {
            if (movieList == null)
            {
                movieList = new List<MovieViewClass>();
            }

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
                            PozaURL = currentMovie.UrlPicture,
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