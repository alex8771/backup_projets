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
    public class sallesController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        public ActionResult Index(int id)
        {
            var salles = db.salles.Where(s => s.id_cinema == id);
            return View(salles.ToList());
        }

        // GET: salles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salle salle = db.salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        // GET: salles/Create
        public ActionResult Create(int id, string nom)
        {
            var salle = new salle();
            //Permet d'avoir l'id du cinéma dans lequel on ajoute une salle.           
            salle.id_cinema = id;
            ViewBag.nomCinema = nom;
            ViewBag.idCinema = id;
            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom");
            return View(salle);
        }

        // POST: salles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,numero,nbr_places,taille_ecran,exploitable")] salle salle)
        {
            salle.id_cinema = (int)Session["idCinemaActuel"];
            bool existe = SalleExiste(salle, true);
            bool erreur = false;
            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", salle.id_cinema);
            if (existe)
            {
                erreur = true;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    erreur = false;
                    salle.id_cinema = (int)Session["idCinemaActuel"];
                    db.salles.Add(salle);
                    db.SaveChanges();
                    return RedirectToAction("CreateSupplement", "supplements", new { id = salle.id, error = erreur, isResponsable = false });
                }
            }
            return RedirectToAction("Edit", "cinemas", new { id = salle.id_cinema, error = erreur, isResponsable = false });
        }
        // GET: salles/Edit/5
        public ActionResult Edit(int? id, bool? error, bool? isSupplement)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            if (error == true && isSupplement == null)
                ViewBag.erreur = "Existe";
            else if (error == false && isSupplement == null)
                ViewBag.erreur = "Succes";
            else if (error == false && isSupplement == true)
                ViewBag.erreur = "SuccesSupplement";            
            else
                ViewBag.erreur = "normal";

            EditSalle_ViewModel viewModel = new EditSalle_ViewModel();

            //On va chercher la liste des salle selon l'id du cinema
            viewModel.SalleDetails = db.salles.Find(id);

            //On va chercher le supplément qui possède l'id de cette salle.
            viewModel.SupplementDetails = db.supplements.Where(c => c.id_salle == id).FirstOrDefault();
            if (viewModel == null)
                return HttpNotFound();

            ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", viewModel.SalleDetails.id_cinema);
            Session["numeroSalleActuelle"] = viewModel.SalleDetails.numero;

            //Ce clear permet de faire 2 post à l'intérieur de la même view sans mélanger les id lors du binding
            ModelState.Clear();
            return View(viewModel);
        }
        // POST: salles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,numero,nbr_places,taille_ecran,exploitable,id_cinema")] salle salle)
        {
            bool existe = SalleExiste(salle, false);
            bool erreur = false;
            if (existe)
            {
                erreur = true;
                return RedirectToAction("Edit", "salles", new { id = salle.id, error = erreur });
            }
            else
            {                
                if (ModelState.IsValid)
                {
                    erreur = false;                    
                    var currentSalle = db.salles.FirstOrDefault(p => p.id == salle.id);
                    currentSalle.id = salle.id;
                    currentSalle.numero = salle.numero;
                    currentSalle.nbr_places = salle.nbr_places;
                    currentSalle.taille_ecran = salle.taille_ecran;
                    currentSalle.exploitable = salle.exploitable;
                    currentSalle.id_cinema = salle.id_cinema;                    
                    db.SaveChanges();
                    return RedirectToAction("Edit", "salles", new { id = salle.id , error = erreur });
                }
                ViewBag.id_cinema = new SelectList(db.cinemas, "id", "nom", salle.id_cinema);
                return View(salle);
            }

        }
        public ActionResult Delete(int id, int cinema)
        {
            salle salle = db.salles.Find(id);
            bool erreur = false;
            if (salle.exploitable)
            {
                erreur = true;                
            }
            else
            {
                erreur = false;                
                //Suppléments associés à cette salle
                supplement sup = db.supplements.Where(c => c.id_salle == salle.id).FirstOrDefault();
                //On supprimer les suppléments avant la salle
                db.supplements.Remove(sup);
                db.salles.Remove(salle);
                db.SaveChanges();                
            }
            return RedirectToAction("Edit", "cinemas", new { id = cinema, error = erreur, isResponsable = false, isDelete = true });

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Vérifie si le numéro de la salle existe déja.
        private bool SalleExiste(salle salle, bool isCreate)
        {
            bool existe = false;
            List<salle> allSalle = db.salles.Where(s => s.id_cinema == salle.id_cinema).ToList();
            foreach (salle s in allSalle)
            {
                if (s.numero == salle.numero)
                {
                    existe = true;
                    if (!isCreate)
                    {
                        if (s.id == salle.id)
                        {
                            existe = false;
                        }
                    }
                }
            }
            return existe;
        }
    }
}
