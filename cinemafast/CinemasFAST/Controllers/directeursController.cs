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
using CinemasFAST.Models;
using CinemasFAST.Models.ViewModels;

namespace CinemasFAST.Controllers
{
    public class directeursController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: directeurs
        public async Task<ActionResult> Index(bool? error, bool? isDelete, string searchString)
        {
            if (error == true && isDelete == null)
                ViewBag.erreur = "fail";
            else if (error == false && isDelete == null)
                ViewBag.erreur = "succes";
            else if (error == false && isDelete == true)
                ViewBag.erreur = "succesDelete";
            else
                ViewBag.erreur = "normal";
            
            IndexDirecteurs_ViewModel viewModel = new IndexDirecteurs_ViewModel();

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Directeurs = await db.directeurs.Where(s => s.nom.Contains(searchString)
                                       || s.prenom.Contains(searchString)).ToListAsync();
                ViewBag.searchRequest = true;
            }
            else
            {
                viewModel.Directeurs = await db.directeurs.ToListAsync();
                ViewBag.searchRequest = false;
            }

            ModelState.Clear();
            return View(viewModel);
        }

        // GET: directeurs/Details/5
        public ActionResult Details(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (error == false)
                ViewBag.erreur = "Succes";
            else
                ViewBag.erreur = "normal";
            //remplis un viewmodel qui permetteras d'afficher les directions du directeur dans le details
            RolesDirections_ViewModel viewModel = new RolesDirections_ViewModel();
            viewModel.Directeur = db.directeurs.Find(id);
            viewModel.Directions = db.directions.Where(d => d.id_directeur == id).ToList();
            if (viewModel.Directeur == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: directeurs/Create
        public ActionResult Create()
        {
            ViewBag.searchRequest = false;
            return View();
        }

        // POST: directeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,prenom,langue,date_naissance,lieu_naissance,image")] directeur directeur)
        {
            bool erreur = false;
            if (ModelState.IsValid)
            {
                db.directeurs.Add(directeur);
                db.SaveChanges();
                erreur = false;                
            }
            else
            {               
                erreur = true;                
            }
            return RedirectToAction("Index", new { error = erreur });           
        }

        // GET: directeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            directeur directeur = db.directeurs.Find(id);
            if (directeur == null)
            {
                return HttpNotFound();
            }
            return View(directeur);
        }

        // POST: directeurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,langue,date_naissance,lieu_naissance,image")] directeur directeur)
        {
            if (ModelState.IsValid)
            {
                //Session["editSuccess"] = "Directeur modifié avec succès!";
                db.Entry(directeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = directeur.id, error = false });                
            }
            //remplis un viewmodel qui permetteras d'afficher les directions du directeur dans le details
            RolesDirections_ViewModel viewModel = new RolesDirections_ViewModel();
            viewModel.Directeur = db.directeurs.Find(directeur.id);
            viewModel.Directions = db.directions.Where(d => d.id_directeur == directeur.id).ToList();
            //return View(" ", viewModel);
            return RedirectToAction("Details", new { id = directeur.id});
        }

        // GET: directeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            directeur directeur = db.directeurs.Find(id);
            if (directeur == null)
            {
                return HttpNotFound();
            }
            return View(directeur);
        }

        // POST: directeurs/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            List<direction> directions = db.directions.Where(d => d.id_directeur == id).ToList();
            foreach (direction d in directions)
            {
                db.directions.Remove(d);
            }
            directeur directeur = db.directeurs.Find(id);           
            db.directeurs.Remove(directeur);
            db.SaveChanges();
            return RedirectToAction("Index", new { error = false, isDelete = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult deleteDirection(int id)
        {
            return RedirectToAction("DeleteConfirmedToDirecteur", "directions", new { id = id });
        }
    }
}
