using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CinemasFAST.Models;

namespace CinemasFAST.Controllers
{
    public class profilsController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: profils
        public ActionResult Index()
        {
            return View(db.profils.ToList());
        }

        // GET: profils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profil profil = db.profils.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // GET: profils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: profils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,courriel,type")] profil profil)
        {
            if (ModelState.IsValid)
            {
                profil.password = ConvertToHash(profil.password);

                db.profils.Add(profil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profil);
        }

        // GET: profils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profil profil = db.profils.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }

            profil.password = null;
            return View(profil);
        }

        // POST: profils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_complet,username,password,courriel,type")] profil profil)
        {
            if (ModelState.IsValid)
            {
                profil.password = ConvertToHash(profil.password);
                Session["type"] = profil.type;

                db.Entry(profil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profil);
        }

        // GET: profils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profil profil = db.profils.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // POST: profils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            profil profil = db.profils.Find(id);
            db.profils.Remove(profil);
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

        public static string ConvertToHash(string text)
        {
            using (System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                StringBuilder sb = null;
                try
                {
                    var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                    sb = new StringBuilder(hash.Length * 2);

                    foreach (byte item in hash)
                    {
                        sb.Append(item.ToString("x2"));
                    }
                    sha1.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                if (sb != null)
                    return sb.ToString();
                return null;

            }
        }
    }
}
