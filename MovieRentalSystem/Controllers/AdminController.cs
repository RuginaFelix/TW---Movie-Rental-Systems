using MovieRentalSystem.Models;
using MovieRentalSystem.Models.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new MedicalEntities())
            {
                List<User> returnedList = GetAllUsers(context);

                if (returnedList == null)
                {
                    return View(new List<User>());
                }

                var userTypes = new List<DropdownClass>
                {
                    new DropdownClass()
                    {
                        name = "ADMIN",
                        value = 0
                    },
                         new DropdownClass()
                    {
                        name = "USER",
                        value = 1
                    },
                              new DropdownClass()
                    {
                        name = "MEDIC",
                        value = 2
                    },
                   new DropdownClass()
                    {
                        name = "SUPRAVEGHETOR",
                        value = 3
                    },
                    new DropdownClass()
                    {
                        name = "PACIENT",
                        value = 4
                    },

                };

                ViewBag.UserTypes = userTypes;

                return View(returnedList);
            }
        }

        private static List<User> GetAllUsers(MedicalEntities context)
        {
            var returnedList = new List<User>();
            var userList = context.Users.Where(t => t.Id != GlobalClass.UserId);

            foreach (var item in userList)
            {
                returnedList.Add(item);
            }

            return returnedList;
        }

        public ActionResult GetUsers()
        {
            using (var context = new MedicalEntities())
            {
                var returnedModel = new List<User>();
                var users = context.Users.Where(t => t.Id != GlobalClass.UserId).ToList();
                if (users == null || users.Count() <= 0)
                {

                    return PartialView("~/Views/Admin/_Table.cshtml");
                }

                foreach (var item in users)
                {
                    var newMessage = new User()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        UserType = item.UserType.Value
                    };

                    returnedModel.Add(newMessage);
                }

                return PartialView("~/Views/Admin/_Table.cshtml", returnedModel);
            }
        }

        public JsonResult SaveUser(UserViewModel model)
        {
            if (model.Parola != model.ConfirmaParola)
            {
                return Json(null);
            }

            using (var context = new MedicalEntities())
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    Parola = model.Parola,
                    UserType = model.UserType
                };

                switch ((USER_TYPE_ENUM)model.UserType)
                {
                    case USER_TYPE_ENUM.ADMIN:
                        break;
                    case USER_TYPE_ENUM.USER:
                        break;
                    case USER_TYPE_ENUM.MEDIC:
                        var medic = new Medic()
                        {
                            Id = Guid.NewGuid(),
                            UserId = newUser.Id,
                            Nume = "Ioan" + new Random(12).Next(1,100),
                            Prenume = "Alexandru Medic" + new Random(12).Next(1,100),
                        };
                        context.Medics.Add(medic);
                        break;
                    case USER_TYPE_ENUM.SUPRAVEGHETOR:
                        break;
                    case USER_TYPE_ENUM.PACIENT:
                        var pacient = new Pacient()
                        {
                            Id = Guid.NewGuid(),
                            UserId = newUser.Id,
                            Nume = "Ioan" + new Random(12).Next(1, 100),
                            Prenume = "Alexan" + new Random(12).Next(1, 100)
                        };
                        context.Pacients.Add(pacient);
                        break;
                    default:
                        break;
                }

                context.Users.Add(newUser);
                context.SaveChanges();

                return Json(true);
            }
        }

        public JsonResult DeleteUser(Guid userId)
        {
            using (var context = new MedicalEntities())
            {
                var userToDelete = context.Users.Single(t => t.Id == userId);
                if (userToDelete != null)
                {
                    context.Users.Remove(userToDelete);
                }

                switch ((USER_TYPE_ENUM)userToDelete.UserType)
                {
                    case USER_TYPE_ENUM.ADMIN:
                        break;
                    case USER_TYPE_ENUM.MEDIC:
                        var medicToRemove = context.Medics.Single(t => t.UserId == userId);
                        context.Medics.Remove(medicToRemove);
                        break;
                    case USER_TYPE_ENUM.SUPRAVEGHETOR:
                        break;
                    case USER_TYPE_ENUM.PACIENT:
                        var pacientToRemove = context.Pacients.Single(t => t.UserId == userId);
                        context.Pacients.Remove(pacientToRemove);
                        break;
                    default:
                        break;
                }


                context.SaveChanges();

                return Json(true);
            }
        }


    }
}