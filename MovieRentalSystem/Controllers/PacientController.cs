using MovieRentalSystem.Models;
using MovieRentalSystem.Models.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalSystem.Controllers
{
    public class PacientController : Controller
    {

        // GET: Pacient
        public ActionResult Index()
        {
            using (var context = new MedicalEntities())
            {

                var currentPacient = context.Pacients.Single(t => t.UserId == GlobalClass.UserId);
                if (currentPacient == null)
                {
                    currentPacient = new Pacient();
                }


                return View(currentPacient);
            }
        }

        public ActionResult GetSpecificPacient (Guid pacientId)
        {
            using (var context = new MedicalEntities())
            {

                var currentPacient = context.Pacients.Single(t => t.Id == pacientId);
                if (currentPacient == null)
                {
                    currentPacient = new Pacient();
                }


                return View("~/Views/Pacient/Index.cshtml", currentPacient);
            }
        }


        public JsonResult SavePacient(Pacient model)
        {
            using (var context = new MedicalEntities())
            {
                var pacient = context.Pacients.Single(t => t.Id == model.Id);
                if (pacient != null)
                {
                    pacient.CNP = model.CNP;
                    pacient.Nume = model.Nume;
                    pacient.Prenume = model.Prenume;
                    pacient.Varsta = model.Varsta;
                    pacient.Adresa = model.Adresa;
                    pacient.Telefon = model.Telefon;
                }

                context.SaveChanges();
            }

            return Json(true);
        }
    }
}