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
    public class rolesController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: roles
        public ActionResult Index()
        {
            var roles = db.roles.Include(r => r.acteur).Include(r => r.film);
            return View(roles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role role = db.roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            ViewBag.id_acteur = new SelectList(db.acteurs, "id", "nom");
            ViewBag.id_film = new SelectList(db.films, "id", "titre");
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_film,id_acteur,nom_personnage")] role role)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Details", "films", new { id = role.id_film });
            }

            ViewBag.id_acteur = new SelectList(db.acteurs, "id", "nom", role.id_acteur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", role.id_film);
            Session["roleErreur"] = "Le nom du personnage doit être entré.";
            return RedirectToAction("Details", "films", new { id = role.id_film });
        }

        // GET: roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role role = db.roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_acteur = new SelectList(db.acteurs, "id", "nom", role.id_acteur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", role.id_film);
            return View(role);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_film,id_acteur,nom_personnage")] role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_acteur = new SelectList(db.acteurs, "id", "nom", role.id_acteur);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", role.id_film);
            return View(role);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role role = db.roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        public ActionResult DeleteConfirmedToFilm(int id)
        {
            role role = db.roles.Find(id);
            film film = db.films.Find(role.id_film);
            db.roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Details", "films", new { id = film.id });
        }

        public ActionResult DeleteConfirmedToActeur(int id)
        {
            role role = db.roles.Find(id);
            acteur acteur = db.acteurs.Find(role.id_acteur);
            db.roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Details", "acteurs", new { id = acteur.id });
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
