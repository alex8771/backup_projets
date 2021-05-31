using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using CinemasFAST.Models;
using CinemasFAST.Models.ViewModels;

namespace CinemasFAST.Controllers
{
    public class filmsController : Controller
    {
        private cinemadbEntities db = new cinemadbEntities();

        // GET: films
        public async Task<ActionResult> Index(bool? erreur, bool? erreurDel, bool? successImport, string searchString)
        {
            if (erreur == true)
                ViewBag.erreur = "fail";
            else if (erreur == false && successImport == true)
                ViewBag.erreur = "succesImport";
            else if (erreur == false && successImport == null)
                ViewBag.erreur = "succes";
            else
                ViewBag.erreur = "normal";
            if (erreurDel == true)
                ViewBag.erreurDel = "fail";
            else if (erreurDel == false)
                ViewBag.erreurDel = "succes";            
            else
                ViewBag.erreurDel = "normal";

            IndexFilm_ViewModel viewModel = new IndexFilm_ViewModel();
            ViewBag.id_genre = new SelectList(db.genres, "id", "nom");
            ViewBag.type_film = new List<string> { "Standard", "Court métrage", "Promotionnel" };


            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Films = await db.films.Where(s => s.titre.Contains(searchString)
                                       || s.description.Contains(searchString)).ToListAsync();
                ViewBag.searchRequest = true;
            }
            else
            {
                viewModel.Films = await db.films.Include(f => f.genre).ToListAsync();
                ViewBag.searchRequest = false;
            }

            ModelState.Clear();
            return View(viewModel);
        }

        // GET: films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesDirections_ViewModel viewModel = getViewModelEdit(id);
            if (viewModel.Film == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: films/Create
        public ActionResult Create()
        {
            ViewBag.searchRequest = false;
            return View();
        }

        // POST: films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titre,description,annee,duree,note,nbr_votes,revenue,id_genre,image,type")] film film)
        {
            bool isConflicted = false;
            if (ModelState.IsValid)
            {
                isConflicted = false;
                db.films.Add(film);
                ViewBag.id_genre = new SelectList(db.genres, "id", "nom", film.id_genre);
                db.SaveChanges();
            }
            else
            {
                isConflicted = true;
            }
            return RedirectToAction("Index", new { erreur = isConflicted });
        }

        // GET: films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = db.films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_genre = new SelectList(db.genres, "id", "nom", film.id_genre);
            return View(film);
        }

        // POST: films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titre,description,annee,duree,note,nbr_votes,revenue,id_genre,type,image")] film film)
        {

            if (ModelState.IsValid)
            {
                Session["editSuccess"] = "Film modifié avec succès!";
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = film.id });
            }
            ViewBag.id_genre = new SelectList(db.genres, "id", "nom", film.id_genre);
            RolesDirections_ViewModel viewModel = getViewModelEdit(film.id);
            return View("Details", viewModel);
        }

        //Remplis un viewmodel qui servira aux vue partiels qui sont a l'interieurs du detils du film
        public RolesDirections_ViewModel getViewModelEdit(int? id)
        {
            RolesDirections_ViewModel viewModel = new RolesDirections_ViewModel();
            viewModel.Role = new role();
            viewModel.Direction = new direction();
            viewModel.Film = db.films.Find(id);
            viewModel.Roles = db.roles.Where(r => r.id_film == id).ToList();
            viewModel.Directions = db.directions.Where(d => d.id_film == id).ToList();

            ViewBag.id_film = new SelectList(db.films.Where(x => x.id == id), "id", "titre", viewModel.Film.id);
            ViewBag.id_acteur = new SelectList(db.acteurs, "id", "nom", viewModel.Role.id_acteur);
            ViewBag.id_directeur = new SelectList(db.directeurs, "id", "nom", viewModel.Direction.id_directeur);
            ViewBag.id_genre = new SelectList(db.genres, "id", "nom", viewModel.Film.id_genre);
            ViewBag.titre = db.films.Find(id).titre;
            ViewBag.film_actuel = viewModel.Film.titre;

            return viewModel;
        }

        // GET: films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = db.films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }


        public ActionResult DeleteConfirmed(int id)
        {

            film film = db.films.Find(id);
            //Supprime les directions et les roles associer au film
            List<direction> directions = db.directions.Where(d => d.id_film == id).ToList();
            List<role> roles = db.roles.Where(r => r.id_film == id).ToList();
            List<seance> seances = db.seances.Where(r => r.id == id).ToList();
            List<estpresente> estpresente = db.estpresentes.Where(x => x.id_film == id).ToList();
            int nbSeance = estpresente.Count();
            bool isConflicted = false;

            if (seances.Count() != 0)
            {
                isConflicted = true;
            }
            else
            {
                isConflicted = false;

                foreach (role r in roles)
                {
                    db.roles.Remove(r);
                }
                foreach (direction d in directions)
                {
                    db.directions.Remove(d);
                }
                foreach (estpresente e in estpresente)
                {
                    db.estpresentes.Remove(e);
                }
                db.films.Remove(film);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { erreurDel = isConflicted });
        }

        public ActionResult deleteRole(int id)
        {
            return RedirectToAction("DeleteConfirmedToFilm", "roles", new { id = id });
        }

        public ActionResult deleteDirection(int id)
        {
            return RedirectToAction("DeleteConfirmedToFilm", "directions", new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Importation d'un fichier des films       

        [HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = filePath.Substring(Math.Max(0, filePath.Length - 4));
                if (extension != ".csv")
                {
                    Session["ErrorUponCreateFilm"] = "Le fichier importé doit être de type .csv";
                    return RedirectToAction("Index");
                }
                AjoutFilms(filePath);
                AjoutDirecteurs(filePath);
                AjoutActeurs(filePath);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                AjoutDirection(filePath);
                AjoutRole(filePath);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { erreur = false, successImport = true });
        }
        //Méthode qui gère l'importation des directeurs.
        public void AjoutDirecteurs(string path)
        {
            List<string> listeDirecteurImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsvPersonnel(v, 4)).ToList();
            //S'assure que le nom d'un réalisateur n'apparait pas plusieurs fois dans la liste.
            listeDirecteurImport = listeDirecteurImport.Distinct().ToList();
            List<directeur> listeDirecteurBD = db.directeurs.ToList();
            bool existDansLaBD = false;
            foreach (string s in listeDirecteurImport)
            {
                foreach (directeur directeur in listeDirecteurBD)
                {
                    string nom;
                    //Si le nom et prénom sont identique cela veut dire qu'il y a seulement le prénom du directeur dans le fichier csv.
                    if (directeur.prenom == directeur.nom)
                    {
                        nom = directeur.prenom;
                    }
                    else
                    {
                        nom = (directeur.prenom + " " + directeur.nom);
                        string lastChar = nom.Substring(nom.Length - 1);
                        //Si le dernier caractère de la string est un espace on l'enlève.
                        if (lastChar == " ")
                        {
                            nom = nom.Remove(nom.Length - 1);
                        }
                    }
                    if (s == nom)
                    {
                        existDansLaBD = true;
                        break;
                    }
                    else
                    {
                        existDansLaBD = false;
                    }
                }
                //Si le directeur n'existe pas dans la BD.
                if (existDansLaBD == false)
                {
                    directeur directeurAdd = new directeur();
                    string[] prenomNom = s.Split(' ');
                    directeurAdd.prenom = prenomNom[0];
                    if (prenomNom.Length > 1)
                    {
                        //Le nombre de noms de famille dans la string
                        int nbrNom = prenomNom.Length - 1;
                        //Ajoutera tous les noms de famille dans la string
                        for (int i = 1; i <= nbrNom; i++)
                        {
                            directeurAdd.nom += prenomNom[i] + " ";
                        }
                    }
                    else
                    {
                        directeurAdd.nom = prenomNom[0];
                    }                    
                    directeurAdd.langue = "À ajouter";
                    directeurAdd.lieu_naissance = "À ajouter";
                    directeurAdd.date_naissance = DateTime.Now;
                    db.directeurs.Add(directeurAdd);
                }
            }
        }
        //Méthode qui ajoute les Directions de chaque réalisateur importé.
        public void AjoutDirection(string path)
        {
            bool existDansLaBD = false;
            List<direction> listeDirectionImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsvDirection(v)).ToList();
            List<direction> listeDirectionBD = db.directions.ToList();
            foreach (direction d in listeDirectionImport)
            {
                foreach (direction direction in listeDirectionBD)
                {
                    if (d.id_film == direction.id_film && d.id_directeur == direction.id_directeur)
                    {
                        existDansLaBD = true;
                        break;
                    }
                    else
                    {
                        existDansLaBD = false;
                    }
                }
                if (existDansLaBD == false)
                {
                    db.directions.Add(d);
                }
            }           
        }
        //Méthode qui gère l'importation des acteurs.
        public void AjoutActeurs(string path)
        {
            List<string> listeActeursImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsvPersonnel(v, 5)).ToList();
            listeActeursImport = FormatListActeur(listeActeursImport);
            listeActeursImport = listeActeursImport.Distinct().ToList();
            List<acteur> listeActeurBD = db.acteurs.ToList();
            bool existDansLaBD = false;
            foreach (string s in listeActeursImport)
            {
                foreach (acteur acteur in listeActeurBD)
                {
                    string nom;
                    //Si le nom et prénom sont identique cela veut dire qu'il y a seulement le prénom de l'acteur dans le fichier csv.
                    if (acteur.prenom == acteur.nom)
                    {
                        nom = acteur.prenom;
                    }
                    else
                    {
                        nom = (acteur.prenom + " " + acteur.nom);                       
                        string lastChar = nom.Substring(nom.Length - 1);
                        //Si le dernier caractère de la string est un espace on l'enlève.
                        if (lastChar == " ")
                        {
                            nom = nom.Remove(nom.Length - 1);
                        }                        
                    }
                    if (s == nom)
                    {
                        existDansLaBD = true;
                        break;
                    }
                    else
                    {
                        existDansLaBD = false;
                    }
                }
                //Si l'acteur n'existe pas dans la BD.
                if (existDansLaBD == false)
                {
                    acteur acteurAdd = new acteur();

                    string[] prenomNom = s.Split(' ');
                    acteurAdd.prenom = prenomNom[0];
                    if (prenomNom.Length > 1)
                    {
                        //Le nombre de noms de famille dans la string
                        int nbrNom = prenomNom.Length - 1;
                        //Ajoutera tous les noms de famille dans la string
                        for (int i = 1; i <= nbrNom; i++)
                        {
                            acteurAdd.nom += prenomNom[i] + " ";
                        }
                    }
                    else
                    {
                        acteurAdd.nom = prenomNom[0];
                    }
                    acteurAdd.langue = "À ajouter";
                    acteurAdd.lieu_naissance = "À ajouter";
                    acteurAdd.date_naissance = DateTime.Now;
                    db.acteurs.Add(acteurAdd);
                }
            }
        }
        //Méthode qui ajoute les rôles d'un fichier importé
        public void AjoutRole(string path)
        {
            List<string> listeActeursImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsvPersonnel(v, 5)).ToList();
            List<film> listeFilmsImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsv(v)).ToList();
            listeActeursImport = FormatListActeur(listeActeursImport);
            List<role> listeRoleImport = new List<role>();
            List<role> listeRoleBD = db.roles.ToList();
            int index = 0;
            bool existDansLaBD = false;
            foreach (film f in listeFilmsImport)
            {
                film film = db.films.Where(x => x.titre == f.titre).FirstOrDefault();
                for (int i = 0; i <= 3; i++)
                {
                    string acteurString = listeActeursImport[index];
                    acteur acteur = db.acteurs.Where(x => x.prenom + " " + x.nom == acteurString).FirstOrDefault();
                    role role = new role();
                    role.id_film = film.id;
                    role.id_acteur = acteur.id;
                    role.nom_personnage = "À ajouter";
                    listeRoleImport.Add(role);
                    index++;
                }
            }
            foreach (role r in listeRoleImport)
            {
                foreach (role rBd in listeRoleBD)
                {
                    if (r.id_film == rBd.id_film && r.id_acteur == rBd.id_acteur)
                    {
                        existDansLaBD = true;
                        break;
                    }
                    else
                    {
                        existDansLaBD = false;
                    }
                }
                if (existDansLaBD == false)
                {
                    db.roles.Add(r);
                }
            }            
        }
        //Méthode qui format la string de la colonne acteur pour resortir tous les noms avec le bon format dans une liste.
        public List<string> FormatListActeur(List<string> listeNonFormatee)
        {
            List<string> listeFormatee = new List<string>();
            string substringActeur = "";
            foreach (string s in listeNonFormatee)
            {
                //On enlève les guillemets
                substringActeur = s.Substring(1, s.Length - 2);
                string[] acteurs = substringActeur.Split(',');
                for (int i = 0; i <= 3; i++)
                {
                    string firstChar = acteurs[i].Substring(0, 1);
                    //Si le premier caractère de la string est un espace on l'enlève.
                    if (firstChar == " ")
                    {
                        substringActeur = acteurs[i].Substring(1, acteurs[i].Length - 1);
                        acteurs[i] = substringActeur;
                    }
                    listeFormatee.Add(acteurs[i]);
                }
            }
            return listeFormatee;
        }
        //Méthode qui gère l'importation des films.
        public void AjoutFilms(string path)
        {
            List<film> listeFilmsBD = db.films.ToList();
            List<film> listeFilmsImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsv(v)).ToList();
            List<string> listeGenreImport = System.IO.File.ReadAllLines(path).Skip(1).Select(v => film_partial.FromCsvGenre(v)).ToList();
            List<genre> listGenreBD = db.genres.ToList();
            bool exist = false;
            int indexGenre = 0;
            foreach (film fImport in listeFilmsImport)
            {
                fImport.type = "Standard";
                foreach (film fData in listeFilmsBD)
                {
                    if (fImport.titre == fData.titre)
                    {
                        exist = true;
                        break;
                    }
                    else
                    {
                        exist = false;
                    }
                }
                if (exist == false)
                {
                    //Associer le film au bon genre dans la table genre.
                    foreach (genre g in listGenreBD)
                    {
                        if (listeGenreImport.ElementAt(indexGenre) == g.nom)
                        {
                            fImport.id_genre = g.id;
                        }
                    }
                    db.films.Add(fImport);
                }
                else
                {
                    //Si le film existe déja on vérifie si ses informations sont identiques que dans le fichier importé.
                    film filmModif = new film();
                    foreach (film fData in listeFilmsBD)
                    {
                        if (fImport.titre == fData.titre)
                        {
                            //Si une des colonnes est différentes entre la version importé et la version déja présente dans la BD on met alors les infos du fichier.
                            if (fImport.description != fData.description || fImport.annee != fData.annee || fImport.duree != fData.duree || fImport.note != fData.note || fImport.nbr_votes != fData.nbr_votes || fImport.revenue != fData.revenue)
                            {
                                filmModif = fImport;
                                fData.description = filmModif.description;
                                fData.annee = filmModif.annee;
                                fData.duree = filmModif.duree;
                                fData.note = filmModif.note;
                                fData.nbr_votes = filmModif.nbr_votes;
                                fData.revenue = filmModif.revenue;
                                db.Entry(fData).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                indexGenre++;
            }
        }
        #endregion
    }
}
