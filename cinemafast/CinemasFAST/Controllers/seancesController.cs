using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using CinemasFAST.Models;

namespace CinemasFAST.Controllers
{
    public class seancesController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();
        private List<salle> salleDb = new List<salle>();
        private List<seance> seanceDb = new List<seance>();
        private List<seance> seancesRecherche = new List<seance>();

        // GET: seances
        public async Task<ActionResult> Index(int? id, bool? erreur)
        {
            List<estpresente> presentation = new List<estpresente>();
            List<film> films = new List<film>();
            if (erreur == true)
                ViewBag.erreur = "fail";
            else if (erreur == false)
                ViewBag.erreur = "succes";
            else
                ViewBag.erreur = "normal";
            int jours = DateTime.Now.Day + 6;
            DateTime jourSept = DateTime.Now.AddDays(6);
            salle salle = db.salles.Where(x => x.id == id).FirstOrDefault();
            Session["numeroSalleActuelle"] = salle.numero;
            Session["sallecourante"] = id;
            seanceDb.Clear();

            foreach (var c in db.seances)
            {
                if (c.id_salle.Equals(id) && c.heure_debut.Day >= DateTime.Now.Day && c.heure_debut.Day < jourSept.Day)
                {
                    seanceDb.Add(c);
                }
            }

            IEnumerable <seance> seanceTrier = seanceDb.OrderBy(seance => seance.heure_debut);
            Seances_ViewModel viewModel = new Seances_ViewModel();
            viewModel.Seances = await db.seances.ToListAsync();
            viewModel.Seance = new seance();
            presentation = await db.estpresentes.ToListAsync();
            films = await db.films.ToListAsync();

            string dateDebutSemaine = DateTime.Now.ToString("dd MMMM yyyy");
            string dateFinSemaine = DateTime.Now.AddDays(6).ToString("dd MMMM yyyy");

            ViewBag.Date = string.Format("Les séances du {0} au {1}.", dateDebutSemaine, dateFinSemaine);
            ViewBag.Films = films;
            ViewBag.Seance = seanceTrier;
            ViewBag.Presentation = presentation;
            ViewBag.id_film = new SelectList(db.films, "id", "titre", viewModel.Seance.id);
            return View(viewModel);
        }

        public ActionResult DateRecherche()
        {
            return View("Index");
        }

        public ActionResult Liste(int? id)
        {
            salleDb.Clear();
            using (cinemadbEntities db = new cinemadbEntities())
            {
                foreach (var c in db.salles)
                {
                    if (c.id_cinema.Equals(id))
                    {
                        salleDb.Add(c);
                    }
                }
            }
            ViewBag.Salle = salleDb;
            return View("SalleCinema");
        }

        // GET: seances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seances.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // GET: seances/Create
        public ActionResult Create()
        {
            ViewBag.id_film = new SelectList(db.films, "id", "titre");
            ViewBag.id_salle = new SelectList(db.salles, "id", "id");
            return View();
        }

        // POST: seances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,heure_debut,heure_fin,nom_seance,id_salle")] seance seance, int salleId)
        {
            seance.id_salle = salleId;
            seance.heure_fin = seance.heure_debut.AddMinutes(30);
            string verif = Verifier(seance);
            bool isConflicted = false;
            if (verif == "")
            {
                if (ModelState.IsValid)
                {
                    isConflicted = false;
                    salle salleUtilise = db.salles.Find(seance.id_salle);
                    salleUtilise.exploitable = true;
                    db.seances.Add(seance);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = seance.id_salle, erreur = isConflicted });
                }
                return View(seance);
            }
            else
            {
                isConflicted = true;
                return RedirectToAction("Index", new { id = seance.id_salle, erreur = isConflicted });
            }
        }

        // GET: seances/Edit/5
        public ActionResult Edit(int? id, bool? erreur)
        {
            if (erreur == null)
                erreur = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstPresenter_ViewModel viewModel = new EstPresenter_ViewModel();
            viewModel.Seance = db.seances.Find(id);
            viewModel.estpresentes = db.estpresentes.Where(s => s.id_seance == id).ToList();
            estpresente presenter = new estpresente();
            presenter.id_seance = (int)id;
            viewModel.estpresente = presenter;
            if (viewModel.Seance == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_film = new SelectList(db.films, "id", "titre", viewModel.Seance.id);
            ViewBag.id_salle = new SelectList(db.salles, "id", "id", viewModel.Seance.id_salle);
            ViewBag.erreur = erreur;

            return View(viewModel);
        }

        // POST: seances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,heure_debut,heure_fin,nom_seance,id_salle")] seance seance)
        {
            List<estpresente> presentation = new List<estpresente>();
            seance seances = db.seances.Find(seance.id);
            int temp = 0;
            if (seances.heure_debut != seance.heure_debut || seances.nom_seance != seance.nom_seance)
            {
                if (ModelState.IsValid)
                {
                    foreach (estpresente e in db.estpresentes)
                    {
                        if (e.id_seance == seance.id)
                        {
                            presentation.Add(e);
                        }
                    }

                    foreach (film f in db.films)
                    {
                        foreach (estpresente e in presentation)
                        {
                            if (f.id == e.id_film)
                            {
                                temp = f.duree;
                            }
                        }
                    }

                    seances.heure_debut = seance.heure_debut;
                    seances.heure_fin = seance.heure_debut.AddMinutes(temp + 30);
                    seances.nom_seance = seance.nom_seance;
                    bool verif = VerifierEdit(seances);
                    if (verif == true)
                    {
                        db.Entry(seances).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        Session["ErreurAjouterFilmSeance"] = "Impossible de modifier l'heure de cette séance, car une autre séance est déjà planifiée à l'horaire.";
                    }
                }
            }
            return RedirectToAction("Edit", new { id = seance.id });
        }

        public ActionResult Delete(int id)
        {
            seance seance = db.seances.Find(id);
            //Test s'il reste des séances dans cette salle.
            List<seance> nbrSeances = db.seances.Where(s => s.id_salle == seance.id_salle).ToList();
            //S'il reste une seule séance dans cette salle cela veut dire qu'il n'y en aura plus après sa suppression on met la variable exploitable à false (pour pouvoir supprimer cette salle éventuellement).
            if (nbrSeances.Count == 1)
            {
                salle salleUtilise = db.salles.Find(seance.id_salle);
                salleUtilise.exploitable = false;
            }

            db.seances.Remove(seance);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = seance.id_salle });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Recherche les séances entre la date de début et la date de fin que l'utilisateur a entré
        [HttpPost]
        public ActionResult RechercheDate(seance d)
        {
            d.id_salle = (int)Session["sallecourante"];

            foreach (var v in db.seances)
            {
                if (v.heure_debut > d.heure_debut && v.heure_fin <= d.heure_fin && v.id_salle == d.id_salle)
                {
                    seancesRecherche.Add(v);
                }
            }

            string date = d.heure_debut.ToString("dd MMMM yyyy");
            string date7 = d.heure_fin.ToString("dd MMMM yyyy");
            string formatDate = string.Format("entre le {0} au {1}.", date, date7);
            ViewBag.Date = formatDate;
            IEnumerable<seance> seanceTrier = seancesRecherche.OrderBy(seance => seance.heure_debut);
            ViewBag.SeanceRechercher = seanceTrier;
            return View("SeanceRechercher");
        }

        public ActionResult DeleteSeanceEstpresente(int id)
        {
            seance seance = db.seances.Find(id);
            List<estpresente> listepresentes = new List<estpresente>();
            foreach (estpresente est in db.estpresentes)
            {
                if (est.id_seance == seance.id)
                {
                    listepresentes.Add(est);
                }
            }
            foreach (estpresente e in listepresentes)
            {
                db.estpresentes.Remove(e);
                db.SaveChanges();
            }

            //Test s'il reste des séances dans cette salle.
            List<seance> nbrSeances = db.seances.Where(s => s.id_salle == seance.id_salle).ToList();
            //S'il reste une seule séance dans cette salle cela veut dire qu'il n'y en aura plus après sa suppression on met la variable exploitable à false (pour pouvoir supprimer cette salle éventuellement).
            if (nbrSeances.Count == 1)
            {
                salle salleUtilise = db.salles.Find(seance.id_salle);
                salleUtilise.exploitable = false;
            }

            db.seances.Remove(seance);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = seance.id_salle });
        }

        public string Verifier(seance seance)
        {
            string message = "";
            string impossible = "Impossible, il y a déjà une représentation dans cette période.";
            foreach (var v in db.seances)
            {
                if (v.heure_debut < seance.heure_debut && seance.heure_debut < v.heure_fin && v.heure_fin > seance.heure_fin && v.id_salle == seance.id_salle)
                {
                    return impossible;
                }
                else if (v.heure_debut == seance.heure_debut && v.heure_fin == seance.heure_fin && v.id_salle == seance.id_salle)
                {
                    return impossible;
                }
                else if (seance.heure_debut > v.heure_debut && seance.heure_debut < v.heure_fin && v.id_salle == seance.id_salle)
                {
                    return impossible;
                }
                else if (seance.heure_fin > v.heure_debut && seance.heure_fin < v.heure_fin && v.id_salle == seance.id_salle)
                {
                    return impossible;
                }
                else if (v.heure_debut > seance.heure_debut && seance.heure_fin > v.heure_fin)
                {
                    return impossible;
                }
            }
            return message;
        }

        //Set l'heure de fin de la séance en fonction de l'heure de début et la durée du film plus 30min
        private seance SetheureFin(seance seance)
        {
            decimal tmp = 0;
            foreach (var c in db.films)
            {
                if (c.id == seance.id)
                {
                    tmp = c.duree + 30;
                }
            }
            decimal heurefin = (seance.heure_debut.Hour * 60) + seance.heure_debut.Minute + seance.heure_debut.Second;
            heurefin = heurefin + tmp;

            decimal h = (Math.Floor(heurefin / 60));
            int heure = decimal.ToInt32(h);

            decimal m = heurefin - (heure * 60);
            int minute = decimal.ToInt32(m);

            int s = seance.heure_debut.Second;

            seance.heure_fin = seance.heure_fin.AddYears(seance.heure_debut.Year - 1);
            seance.heure_fin = seance.heure_fin.AddMonths(seance.heure_debut.Month - 1);
            seance.heure_fin = seance.heure_fin.AddDays(seance.heure_debut.Day - 1);

            seance.heure_fin = seance.heure_fin.AddHours(heure);
            seance.heure_fin = seance.heure_fin.AddMinutes(minute);
            seance.heure_fin = seance.heure_fin.AddSeconds(s);

            return seance;
        }

        public string VerifTemp(seance s)
        {
            string temps = "";
            foreach (var v in db.seances)
            {
                if (s.id != v.id && v.heure_debut == s.heure_debut)
                {
                    temps = "Impossible de mettre cette heure de début à cette séance, elle entre en conflit avec une autre séance.";
                    return temps;
                }
                if (s.id != v.id)
                {
                    if (s.heure_debut > v.heure_debut && s.heure_debut < v.heure_fin && s.id_salle == v.id_salle)
                    {
                        temps = "Impossible de mettre cette heure de début à cette séance, elle entre en conflit avec une autre séance.";
                        return temps;
                    }
                    else if (s.heure_fin > v.heure_debut && s.heure_fin < v.heure_fin && s.id_salle == v.id_salle)
                    {
                        temps = "Impossible de mettre cette heure de début à cette séance, elle entre en conflit avec une autre séance.";
                        return temps;
                    }
                }
            }
            return temps;
        }


        public ActionResult CreateSemaine(int? id)
        {
            List<estpresente> presentation = new List<estpresente>();
            List<seance> seancesSemaine = new List<seance>();
            seance seance = db.seances.Find(id);
            seance ss;
            int j = 1;
            bool isConflicted = false;
            for (int i = 1; i < 7; i++)
            {
                ss = new seance();
                ss.heure_debut = seance.heure_debut.AddDays(j);
                ss.heure_fin = seance.heure_fin.AddDays(j);
                ss.nom_seance = seance.nom_seance;
                ss.id_salle = seance.id_salle;
                seancesSemaine.Add(ss);
                j++;
            }

            foreach (estpresente e in db.estpresentes)
            {
                if (e.id_seance == seance.id)
                {
                    presentation.Add(e);
                }
            }

            bool verifSemaine = VerifierSemaineSeance(seancesSemaine);
            if (verifSemaine == true)
            {
                foreach (seance s in seancesSemaine)
                {
                    db.seances.Add(s);
                    db.SaveChanges();
                    CreatePresentation(s, presentation);
                }
            }
            else
            {
                isConflicted = true;
            }
            return RedirectToAction("Edit", new { id = seance.id, erreur = isConflicted });
        }


        private void CreatePresentation(seance s, List<estpresente> presentation)
        {
            estpresente es;
            foreach (estpresente e in presentation)
            {
                es = new estpresente();
                es.id_film = e.id_film;
                es.id_seance = s.id;
                es.isprincipal = e.isprincipal;
                db.estpresentes.Add(es);
                db.SaveChanges();
            }
        }

        private bool VerifierSemaineSeance(List<seance> s)
        {
            foreach (seance seances in s)
            {
                string verif = VerifTemp(seances);
                if (verif != "")
                {
                    return false;
                }
            }
            return true;
        }

        private bool VerifierEdit(seance s)
        {
            foreach (seance seance in db.seances)
            {
                if (s.nom_seance != seance.nom_seance && s.heure_debut == seance.heure_debut && s.id_salle == seance.id_salle && s.id == seance.id)
                {
                    return true;
                }
                else if (s.heure_debut > seance.heure_debut && s.heure_debut < seance.heure_fin && s.id_salle == seance.id_salle && s.id != seance.id)
                {
                    return false;
                }
                else if (s.heure_fin > seance.heure_debut && s.heure_fin < seance.heure_fin && s.id_salle == seance.id_salle)
                {
                    return false;
                }
                else if (s.nom_seance != seance.nom_seance && s.heure_debut != seance.heure_debut && s.heure_fin < seance.heure_debut && s.id == seance.id)
                {
                    return true;
                }
                else if (s.nom_seance != seance.nom_seance && s.heure_debut != seance.heure_debut && s.heure_debut > seance.heure_fin && s.id == seance.id)
                {
                    return true;
                }
                else if (s.nom_seance == seance.nom_seance && s.id != seance.id && s.heure_debut == seance.heure_debut && s.heure_fin == seance.heure_fin)
                {
                    return false;
                }
                else if(s.id_salle==seance.id_salle && s.heure_fin >seance.heure_debut && s.heure_fin<seance.heure_fin)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
