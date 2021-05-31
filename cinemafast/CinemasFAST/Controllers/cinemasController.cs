using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CinemasFAST.Models;
using CinemasFAST.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CinemasFAST.Controllers
{
    public class cinemasController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: cinemas
        public async Task<ActionResult> Index(string searchString)
        {
            IndexCinema_ViewModel viewModel = new IndexCinema_ViewModel();

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Cinemas = await db.cinemas.Where(s => s.nom.Contains(searchString)
                || s.responsable.nom.Contains(searchString)
                || s.responsable.prenom.Contains(searchString)).ToListAsync();
                ViewBag.searchRequest = true;
            }
            else
            {
                viewModel.Cinemas = await db.cinemas.Include(c => c.responsable).ToListAsync();
                ViewBag.searchRequest = false;
            }

            ModelState.Clear();
            return View(viewModel);
        }

        public ActionResult IndexMessage(string error)
        {
            if (error == "Existe")
                ViewBag.erreur = "Existe";
            else if (error == "Aucune")
                ViewBag.erreur = "Aucune";
            else if (error == "Invalide")
                ViewBag.erreur = "Invalide";
            else if (error == "Utilise")
                ViewBag.erreur = "Utilise";
            else if (error == "succesDelete")
                ViewBag.erreur = "succesDelete";
            else
                ViewBag.erreur = "Normal";

            IndexCinema_ViewModel viewModel = new IndexCinema_ViewModel();
            viewModel.Cinemas = db.cinemas.Include(c => c.responsable).ToList();
            ModelState.Clear();
            return View("Index", viewModel);
        }


        // GET: cinemas/Create
        public ActionResult Create()
        {
            ViewBag.id_responsable = new SelectList(db.responsables, "id", "nom");
            ViewBag.searchRequest = false;
            return View();
        }
        // POST: cinemas/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,adresse,telephone")] cinema cinema)
        {
            bool existe = CinemaExiste(cinema);
            string erreur = "";
            if (existe)
            {
                erreur = "Existe";
            }
            else
            {
                erreur = "Aucune";
                //On crée automatiquement un responsable lors de la création d'un cinéma
                responsable johnDoe = new responsable();
                johnDoe.id = cinema.id;
                johnDoe.nom = "Doe";
                johnDoe.prenom = "John";
                johnDoe.telephone = "418-000-0000";
                johnDoe.courriel = "myemail@hotmail.com";
                ViewBag.id_responsable = new SelectList(db.responsables, "id", "nom", cinema.id_responsable);

                if (ModelState.IsValid)
                {
                    db.cinemas.Add(cinema);
                    db.responsables.Add(johnDoe);
                    db.SaveChanges();
                }
                else
                {
                    erreur = "Invalide";
                }
            }
            return RedirectToAction("IndexMessage", new { error = erreur });
        }

        // GET: cinemas/Edit/5
        public ActionResult Edit(int? id, bool? error, bool? isResponsable, bool? isDelete)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (error == true && isResponsable == null && isDelete == null)
                ViewBag.erreur = "Existe";
            else if (error == false && isResponsable == null && isDelete == null)
                ViewBag.erreur = "Succes";
            else if (isResponsable == true)
                ViewBag.erreur = "SuccesResponsable";
            else if (isResponsable == false && error == false && isDelete == null)
                ViewBag.erreur = "SuccesSalle";
            else if (isResponsable == false && error == true && isDelete == null)
                ViewBag.erreur = "FailSalle";
            else if (isResponsable == false && error == false && isDelete == true)
                ViewBag.erreur = "SuccesDelete";
            else if (isResponsable == false && error == true && isDelete == true)
                ViewBag.erreur = "FailDelete";
            else
                ViewBag.erreur = "normal";

            EditCinema_ViewModel viewModel = new EditCinema_ViewModel();
            //On va chercher la liste des salle selon l'id du cinema
            viewModel.Salles = db.salles.Where(c => c.id_cinema == id).ToList();

            //On va chercher le cinéma qui possède cet id.
            viewModel.CinemaDetails = db.cinemas.Find(id);

            //On va chercher le responsable du cinéma qui possède cet id.
            viewModel.ResponsableDetails = db.responsables.Find(viewModel.CinemaDetails.id_responsable);
            Session["idCinemaActuel"] = id;
            Session["nomCinemaActuel"] = viewModel.CinemaDetails.nom;
            if (viewModel == null)
                return HttpNotFound();
            ViewBag.id_responsable = new SelectList(db.responsables, "id", "nom", viewModel.CinemaDetails.id_responsable);
            //Ce clear permet de faire 2 post à l'intérieur de la même view sans mélanger les id lors du binding
            ModelState.Clear();
            return View(viewModel);
        }

        // POST: cinemas/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,adresse,telephone,id_responsable")] cinema cinema)
        {
            bool existe = CinemaExiste(cinema);
            bool erreur = false;
            if (existe)
            {
                erreur = true;
                return RedirectToAction("Edit", new { error = erreur });
            }
            else
            {
                erreur = false;
                if (ModelState.IsValid)
                {
                    // J'update les champs un par un lors de la modification parce que puisque je fais un select 
                    // sur les données dans la méthode CinemaExiste() ca crée un conflit avec | db.Entry(cinema).State = EntityState.Modified;
                    var currentCinema = db.cinemas.FirstOrDefault(p => p.id == cinema.id);
                    currentCinema.id = cinema.id;
                    currentCinema.nom = cinema.nom;
                    currentCinema.adresse = cinema.adresse;
                    currentCinema.telephone = cinema.telephone;
                    currentCinema.id_responsable = cinema.id_responsable;
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = cinema.id, error = erreur });
                }
                ViewBag.id_responsable = new SelectList(db.responsables, "id", "nom", cinema.id_responsable);
                return View(cinema);
            }
        }
        public ActionResult Delete(int id)
        {
            cinema cinema = db.cinemas.Find(id);
            //La liste des salles de ce cinéma.
            List<salle> salles = db.salles.Where(c => c.id_cinema == id).ToList();
            //Le responsable associé à ce cinéma.
            responsable responsable = db.responsables.Find(cinema.id_responsable);
            //On supprimer toutes les salles et leurs suppléments avant de pouvoir supprimer le cinéma.
            string erreur = "";
            foreach (salle s in salles)
            {
                if (s.exploitable)
                {
                    erreur = "Utilise";
                    return RedirectToAction("IndexMessage", new { error = erreur });
                }
                else
                {
                    supplement sup = db.supplements.Where(c => c.id_salle == s.id).FirstOrDefault();
                    db.supplements.Remove(sup);
                    db.salles.Remove(s);
                }

            }
            erreur = "succesDelete";
            db.responsables.Remove(responsable);
            db.cinemas.Remove(cinema);
            db.SaveChanges();
            return RedirectToAction("IndexMessage", new { error = erreur });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Vérifie si le nom du cinéma existe déja.
        private bool CinemaExiste(cinema cine)
        {
            bool existe = false;

            List<cinema> allCinema = db.cinemas.ToList();
            foreach (cinema c in allCinema)
            {
                if (c.nom == cine.nom)
                {
                    if (c.id != cine.id)
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }
    }
}
