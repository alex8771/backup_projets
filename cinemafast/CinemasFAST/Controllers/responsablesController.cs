using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemasFAST.Models;

namespace CinemasFAST.Controllers
{
    public class responsablesController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: responsables
        public ActionResult Index()
        {
            return View(db.responsables.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,telephone,courriel")] responsable responsable)
        {
            
            if (ModelState.IsValid)
            {               
                db.Entry(responsable).State = EntityState.Modified;
                db.SaveChanges();
                cinema cinema = db.cinemas.Where(c => c.id_responsable == responsable.id).FirstOrDefault();
                return RedirectToAction("Edit", "cinemas", new { id = cinema.id, error = false, isResponsable = true });
            }
            return View(responsable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
