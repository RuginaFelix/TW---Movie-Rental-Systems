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
        public ActionResult Index()
        {
            using (var context = new MedicalEntities())
            {
                List<Pacient> returnedList = GetAllPacients(context);

                if (returnedList == null)
                {
                    return View(new List<Pacient>());
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

        public ActionResult GetNotes(Guid userId)
        {
            using (var context = new MedicalEntities())
            {
                var returnedModel = new List<NotesViewModel>();
                var messages = context.Mesajes.Where(t => t.ReciverId == userId).ToList();
                if (messages == null || messages.Count() <= 0)
                {

                    return PartialView("~/Views/Home/Modals/_ModalMesajeBody.cshtml", returnedModel);
                }

                foreach (var item in messages)
                {
                    var newMessage = new NotesViewModel()
                    {
                        Id = item.Id,
                        AuthorId = item.AuthorId.Value,
                        ReciverId = item.ReciverId.Value,
                        Mesaj = item.Mesaj
                    };

                    if (item.AuthorType == (int)AuthorType.MEDIC)
                    {
                        var autor = context.Medics.Single(t => t.UserId == item.AuthorId);
                        newMessage.AuthorName = autor.Nume + " " + autor.Prenume;

                        var reciver = context.Pacients.Single(t => t.UserId == item.ReciverId);
                        newMessage.ReciverName = reciver.Nume + " " + reciver.Prenume;

                    }
                    else if (item.AuthorType == (int)AuthorType.PACIENT)
                    {
                        var autor = context.Pacients.Single(t => t.UserId == item.AuthorId);
                        newMessage.AuthorName = autor.Nume + " " + autor.Prenume;

                        var reciver = context.Medics.Single(t => t.UserId == item.ReciverId);
                        newMessage.ReciverName = reciver.Nume + " " + reciver.Prenume;
                    }

                    returnedModel.Add(newMessage);
                }

                return PartialView("~/Views/Home/Modals/_ModalMesajeBody.cshtml", returnedModel);
            }
        }

        public JsonResult SaveMessage(string message, Guid senderId, Guid reciverId)
        {
            using (var context = new MedicalEntities())
            {
                var newMessage = new Mesaje()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = senderId,
                    Mesaj = message,
                    ReciverId = reciverId
                };

                if(GlobalClass.UserType == (int)USER_TYPE_ENUM.MEDIC)
                {
                    newMessage.AuthorType = (int)AuthorType.MEDIC;

                } else if (GlobalClass.UserType == (int)USER_TYPE_ENUM.SUPRAVEGHETOR)
                {
                    newMessage.AuthorType = (int)AuthorType.SUPRAVEGHETOR;

                } else if (GlobalClass.UserType == (int)USER_TYPE_ENUM.PACIENT)
                {
                    newMessage.AuthorType = (int)AuthorType.PACIENT;
                }

                context.Mesajes.Add(newMessage);
                context.SaveChanges();

                return Json(true);
            }
        }

        public JsonResult DeleteNote(Guid noteId)
        {
            using (var context = new MedicalEntities())
            {
                var messageToDelete = context.Mesajes.Single(t => t.Id == noteId);
                if(messageToDelete != null)
                {
                    context.Mesajes.Remove(messageToDelete);
                }

                context.SaveChanges();

                return Json(true);
            }
        }
        private static List<Pacient> GetAllPacients(MedicalEntities context)
        {
            var returnedList = new List<Pacient>();
            var pacientList = context.Pacients;

            foreach (var item in pacientList)
            {
                returnedList.Add(item);
            }

            return returnedList;
        }
    }
}