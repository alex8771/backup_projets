using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CinemasFAST.Models;
using CinemasFAST.Models.ViewModels;

namespace CinemasFAST.Controllers
{
    public class acteursController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: acteurs
        public async Task<ActionResult> Index(bool? erreur, bool? erreurDel, string searchString)
        {
            if (erreur == true)
                ViewBag.erreur = "fail";
            else if (erreur == false)
                ViewBag.erreur = "succes";
            else
                ViewBag.erreur = "normal";
            if (erreurDel == null)
                erreurDel = true;
            ViewBag.erreurDel = erreurDel;

            IndexActeurs_ViewModel viewModel = new IndexActeurs_ViewModel();

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Acteurs = await db.acteurs.Where(s => s.nom.Contains(searchString)
                                       || s.prenom.Contains(searchString)).ToListAsync();
                ViewBag.searchRequest = true;
            }
            else
            {
                viewModel.Acteurs = await db.acteurs.ToListAsync();
                ViewBag.searchRequest = false;
            }

            ModelState.Clear();
            return View(viewModel);
        }

        // GET: acteurs/Details/5
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
            //remplis un viewmodel qui permetteras d'afficher les roles de l'acteur dans le details
            RolesDirections_ViewModel viewModel = new RolesDirections_ViewModel();
            acteur acteur = db.acteurs.Find(id);
            viewModel.Acteur = acteur;
            viewModel.Roles = db.roles.Where(r => r.id_acteur == id).ToList();
            if (viewModel.Acteur == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: acteurs/Create
        public ActionResult Create()
        {
            ViewBag.searchRequest = false;
            return View();
        }

        // POST: acteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,prenom,langue,date_naissance,lieu_naissance,image")] acteur acteur)
        {
            bool isConflicted = false;
            if (ModelState.IsValid)
            {
                isConflicted = false;
                db.acteurs.Add(acteur);
                db.SaveChanges();
            }
            else
            {
                isConflicted = true;
            }
            return RedirectToAction("Index", new { erreur = isConflicted });
        }

        // GET: acteurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acteur acteur = db.acteurs.Find(id);
            if (acteur == null)
            {
                return HttpNotFound();
            }
            return View(acteur);
        }

        // POST: acteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,langue,date_naissance,lieu_naissance,image")] acteur acteur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acteur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = acteur.id, error = false });
            }
            //remplis un viewmodel qui permetteras d'afficher les roles de l'acteur dans le details
            RolesDirections_ViewModel viewModel = new RolesDirections_ViewModel();
            viewModel.Acteur = db.acteurs.Find(acteur.id);
            viewModel.Roles = db.roles.Where(r => r.id_acteur == acteur.id).ToList();
            return View(" ", viewModel);
        }

        // GET: acteurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acteur acteur = db.acteurs.Find(id);
            if (acteur == null)
            {
                return HttpNotFound();
            }
            return View(acteur);
        }

        // POST: acteurs/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            List<role> roles = db.roles.Where(r => r.id_acteur == id).ToList();
            foreach (role r in roles)
            {
                db.roles.Remove(r);
            }
            acteur acteur = db.acteurs.Find(id);
            bool isConflicted = false;
            db.acteurs.Remove(acteur);
            db.SaveChanges();
            return RedirectToAction("Index", new { erreurDel = isConflicted });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult deleteRole(int id)
        {
            return RedirectToAction("DeleteConfirmedToActeur", "roles", new { id = id });
        }
    }
}
