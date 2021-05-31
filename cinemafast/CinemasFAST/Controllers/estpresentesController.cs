using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Windows.Forms;
using CinemasFAST.Models;

namespace CinemasFAST.Controllers
{
    public class estpresentesController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();
        // GET: estpresentes
        public ActionResult Index()
        {
            var estpresentes = db.estpresentes.Include(e => e.seance).Include(e => e.film);
            return View(estpresentes.ToList());
        }

        // GET: estpresentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstPresenter_ViewModel presenter = new EstPresenter_ViewModel();
            estpresente estpresente = db.estpresentes.Find(id);


            if (estpresente == null)
            {
                return HttpNotFound();
            }
            return View(estpresente);
        }

        // GET: estpresentes/Create
        public ActionResult Create()
        {
            ViewBag.id_seance = new SelectList(db.seances, "id", "nom_seance");
            ViewBag.id_film = new SelectList(db.films, "id", "titre");
            return View();
        }

        // POST: estpresentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_film,id_seance,isprincipal")] estpresente estpresente)
        {
            film film = db.films.Find(estpresente.id_film);
            List<estpresente> presentations = new List<estpresente>();
            foreach (estpresente estpresentes in db.estpresentes)
            {
                if (estpresente.id_seance == estpresentes.id_seance)
                {
                    presentations.Add(estpresentes);
                }
            }
            if (film.type == "Standard" || film.type == "Court métrage")
            {
                estpresente presentation = change(presentations, estpresente,film);
                if (presentation.id != estpresente.id)
                {
                    db.Entry(presentation).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (film.type == "Promotionnel")
            {
                estpresente.isprincipal = false;
            }
            seance s = AddTimeSeance(estpresente);


            bool b = VerifTemps(s);
            if (b == true)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                if (ModelState.IsValid)
                {
                    db.estpresentes.Add(estpresente);
                    db.SaveChanges();
                    return RedirectToAction("Edit", "seances", new { id = estpresente.id_seance });
                }
            }
            else
            {
                Session["ErreurAjouterFilmSeance"] = "Impossible d'ajouter ce film,car il va mettre la séance en conflit avec une autre séance.";
            }

            return RedirectToAction("Edit", "seances", new { id = estpresente.id_seance });
        }

        private bool VerifTemps(seance s)
        {
            foreach (seance seance in db.seances)
            {
                if (s.heure_debut > seance.heure_debut && s.heure_debut < seance.heure_fin && s.id_salle == seance.id_salle)
                {
                    return false;
                }
                if (s.heure_fin > seance.heure_debut && s.heure_fin < seance.heure_fin && s.id_salle == seance.id_salle)
                {
                    return false;
                }
                if (s.heure_debut < seance.heure_debut && s.heure_fin > seance.heure_fin && s.id_salle == seance.id_salle)
                {
                    return false;
                }
            }
            return true;
        }

        private estpresente change(List<estpresente> presentations, estpresente estpresente, film film)
        {
            estpresente presentation = new estpresente();

            if(film.type== "Standard" ||film.type== "Court métrage")
            {
                if (presentations.Count() > 0 && estpresente.isprincipal == true)
                {
                    foreach (estpresente e in presentations)
                    {
                        if (e.isprincipal == true)
                        {
                            e.isprincipal = false;
                            presentation = e;
                            return presentation;
                        }
                    }
                }
                else if (presentations.Count() == 0 && estpresente.isprincipal == false)
                {
                    estpresente.isprincipal = true;
                }
            }
            else
            {
                estpresente.isprincipal = false;
            }

            return estpresente;
        }

        // GET: estpresentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estpresente estpresente = db.estpresentes.Find(id);
            if (estpresente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_seance = new SelectList(db.seances, "id", "nom_seance", estpresente.id_seance);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", estpresente.id_film);
            return View(estpresente);
        }

        // POST: estpresentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_film,id_seance,isprincipal")] estpresente estpresente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estpresente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_seance = new SelectList(db.seances, "id", "nom_seance", estpresente.id_seance);
            ViewBag.id_film = new SelectList(db.films, "id", "titre", estpresente.id_film);
            return View(estpresente);
        }

        // GET: estpresentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estpresente estpresente = db.estpresentes.Find(id);
            if (estpresente == null)
            {
                return HttpNotFound();
            }
            return View(estpresente);
        }

        // POST: estpresentes/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            estpresente estpresente = db.estpresentes.Find(id);

            if (estpresente.isprincipal == true)
            {
                estpresente estpresentes = changeprincipale(estpresente);
                db.Entry(estpresentes).State = EntityState.Modified;
                db.SaveChanges();
            }

            seance seances = db.seances.Find(estpresente.id_seance);
            film films = db.films.Find(estpresente.id_film);
            int mins = films.duree;
            seances.heure_fin = seances.heure_fin.AddMinutes(-mins);

            db.estpresentes.Remove(estpresente);
            db.SaveChanges();
            return RedirectToAction("Edit", "seances", new { id = estpresente.id_seance });
        }

        private estpresente changeprincipale(estpresente estpresentes)
        {
            foreach (estpresente e in db.estpresentes)
            {
                if (e.id_seance == estpresentes.id_seance && e.id != estpresentes.id)
                {
                    e.isprincipal = true;
                    return e;
                }
            }
            return estpresentes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private seance AddTimeSeance(estpresente estpresente)
        {
            film f = new film();
            foreach (film film in db.films)
            {
                if (estpresente.id_film == film.id)
                {
                    f = film;
                }
            }
            int mins = f.duree;
            seance s = db.seances.Find(estpresente.id_seance);
            s.heure_fin = s.heure_fin.AddMinutes(mins);
            return s;
        }
    }
}
