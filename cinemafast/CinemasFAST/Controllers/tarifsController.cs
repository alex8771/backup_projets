using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemasFAST.Models;

namespace CinemasFAST.Controllers
{
    public class tarifsController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: tarifs
        public ActionResult Index()
        {
            var tarifs = db.tarifs.Include(t => t.cinema);
            return View(tarifs.ToList());
        }

        // GET: tarifs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarif tarif = db.tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }

        // GET: tarifs/Create
        public ActionResult Create()
        {
            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom");
            return View();
        }

        // POST: tarifs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,prix_adulte,prix_enfant,prix_aine,prix_3d,prix_ultra_avx,prix_dbox,prix_imax,id_cinema")] tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.tarifs.Add(tarif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", tarif.id_cinema);
            return View(tarif);
        }

        // GET: tarifs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarif tarif = db.tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", tarif.id_cinema);
            return View(tarif);
        }

        // POST: tarifs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,prix_adulte,prix_enfant,prix_aine,prix_3d,prix_ultra_avx,prix_dbox,prix_imax,id_cinema")] tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", tarif.id_cinema);
            return View(tarif);
        }

        // GET: tarifs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarif tarif = db.tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }

        // POST: tarifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tarif tarif = db.tarifs.Find(id);
            db.tarifs.Remove(tarif);
            db.SaveChanges();
            return RedirectToAction("Index");
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
