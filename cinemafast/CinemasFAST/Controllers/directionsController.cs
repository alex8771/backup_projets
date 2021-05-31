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
    public class directionsController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: directions
        public ActionResult Index()
        {
            var directions = db.directions.Include(d => d.directeur).Include(d => d.film);
            return View(directions.ToList());
        }

        // GET: directions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direction direction = db.directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // GET: directions/Create
        public ActionResult Create()
        {
            ViewBag.id_directeur = new SelectList(db.directeurs, "id", "nom");
            ViewBag.id_film = new SelectList(db.films, "id", "titre");
            return View();
        }

        // POST: directions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_directeur,id_film")] direction direction)
        {
            if (ModelState.IsValid)
            {
                db.directions.Add(direction);
                db.SaveChanges();
                return RedirectToAction("Details", "films", new { id = direction.id_film });
            }

            ViewBag.id_directeur = new SelectList(db.directeurs, "id", "nom", direction.id_directeur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", direction.id_film);
            return View(direction);
        }

        // GET: directions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direction direction = db.directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_directeur = new SelectList(db.directeurs, "id", "nom", direction.id_directeur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", direction.id_film);
            return View(direction);
        }

        // POST: directions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_directeur,id_film")] direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_directeur = new SelectList(db.directeurs, "id", "nom", direction.id_directeur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", direction.id_film);
            return View(direction);
        }

        // GET: directions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direction direction = db.directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }


        public ActionResult DeleteConfirmedToFilm(int id)
        {
            direction direction = db.directions.Find(id);
            film film = db.films.Find(direction.id_film);
            db.directions.Remove(direction);
            db.SaveChanges();
            return RedirectToAction("Details", "films", new { id = film.id });
        }

        public ActionResult DeleteConfirmedToDirecteur(int id)
        {
            direction direction = db.directions.Find(id);
            directeur directeur = db.directeurs.Find(direction.id_directeur);
            db.directions.Remove(direction);
            db.SaveChanges();
            return RedirectToAction("Details", "directeurs", new { id = directeur.id });
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
