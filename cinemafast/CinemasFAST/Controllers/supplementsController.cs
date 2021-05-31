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
    public class supplementsController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,C3d,ultra_avx,dbox,imax,id_salle")] supplement supplement)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(supplement).State = EntityState.Modified;
                db.SaveChanges();                
                return RedirectToAction("Edit", "salles", new { id = supplement.id_salle, error = false, isSupplement = true });
            }
            ViewBag.id_salle = new SelectList(db.salles, "id", "taille_ecran", supplement.id_salle);
            return View(supplement);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Une entrée dans la table supplément est automatiquement créée lors de la création d'une salle.
        public ActionResult CreateSupplement(int id, bool? error, bool? isResponsable)
        {            
            supplement supplementAjout = new supplement();
            if (!db.supplements.Any())
            {
                supplementAjout.id = 0;
            }
            else
            {
                supplementAjout.id = db.supplements.Max(u => u.id) + 1;
            }
            supplementAjout.imax = false;
            supplementAjout.ultra_avx = false;
            supplementAjout.C3d = false;
            supplementAjout.dbox = false;
            supplementAjout.id_salle = id;
            salle salle = db.salles.Find(id);
            cinema cine = db.cinemas.Where(c => c.id == salle.id_cinema).FirstOrDefault();

            db.supplements.Add(supplementAjout);
            db.SaveChanges();

            return RedirectToAction("Edit", "cinemas", new { id = cine.id, error = error, isResponsable = isResponsable }); ;
        }
    }
}
