using CinemasFAST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemasFAST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Login(profil user)
        {
            using (cinemadbEntities db = new cinemadbEntities())
            {
                string hashedPassword = profilsController.ConvertToHash(user.password);
                var dbUSER = db.profils.Where(x => x.username == user.username && x.password == hashedPassword).FirstOrDefault();
                if (dbUSER == null)
                {
                    // Doit prévoir avertir si la tentative de connexion a échouée.
                    return View("Index");
                }
                else
                {
                    Session["login"] = dbUSER.nom_complet;
                    Session["id"] = dbUSER.id;
                    Session["type"] = dbUSER.type;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Disconnect()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}