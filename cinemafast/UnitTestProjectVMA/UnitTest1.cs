using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemasFAST;
using CinemasFAST.Controllers;
using CinemasFAST.Models;
using System.Linq;

namespace UnitTestProjectVMA
{
    [TestClass]
    public class UnitTest1
    {
        private cinemadbEntities db = new cinemadbEntities();
        acteursController controllerActeur = new acteursController();
        rolesController controllerRole = new rolesController();
        seancesController controllerSeance = new seancesController();
        filmsController controllerFilm = new filmsController();
        directionsController controllerDirection = new directionsController();
        private string nom = "testTristanthisIsUnique";
        private int idActeur;
        private int idFilm;
        private int idSalle;
        private int idDirecteur;

        [TestMethod]
        public void TestCreateActeurEtRole()
        {
            acteur acteur = new acteur();
            acteur.nom = nom;
            acteur.prenom = nom;
            acteur.langue = "Anglais";
            acteur.date_naissance = DateTime.Now;
            acteur.lieu_naissance = "Beauport";
            acteur.image = "link";
            controllerActeur.Create(acteur);

            idFilm = db.films.First().id;
            idActeur = db.acteurs.Where(x => x.nom == nom && x.prenom == nom).ToList().First().id;

            role role = new role();
            role.id_film = idFilm;
            role.id_acteur = idActeur;
            role.nom_personnage = nom;
            controllerRole.Create(role);

            var roleFound = db.roles.Where(x => x.nom_personnage == nom).ToList().First();
            Assert.IsNotNull(roleFound);
        }

        [TestMethod]
        public void TestDelActeurCheckIfRoleDelCascade()
        {
            idActeur = db.acteurs.Where(x => x.nom == nom && x.prenom == nom).ToList().First().id;
            controllerActeur.DeleteConfirmed(idActeur);
            var roleFound = db.roles.Any(x => x.nom_personnage == nom);
            Assert.IsFalse(roleFound);
        }

        [TestMethod]
        public void TestCreateFilm()
        {
            film film = new film();
            film.titre = nom;
            film.description = nom + ".........................................................";
            film.annee = 2020;
            film.duree = 115;
            film.note = 5;
            film.nbr_votes = 5000;
            film.revenue = 0;
            film.id_genre = 2;
            film.image = "Link";
            film.type = "Standard";
            controllerFilm.Create(film);

            idDirecteur = db.directeurs.First().id;
            idFilm = db.films.Where(x => x.titre == nom).ToList().First().id;

            direction direction = new direction();
            direction.id_directeur = idDirecteur;
            direction.id_film = idFilm;
            controllerDirection.Create(direction);

            var directionFound = db.directions.Where(x => x.id_film == idFilm).ToList().First().id;
            Assert.IsNotNull(directionFound);

        }

        [TestMethod]
        public void TestDelFilmCheckIfDirectionDelCascade()
        {
            idFilm = db.films.Where(x => x.titre == nom).ToList().First().id;
            controllerFilm.DeleteConfirmed(idFilm);
            var directionFound = db.directions.Any(x => x.id_film == idFilm);
            Assert.IsFalse(directionFound);
        }

        [TestMethod]
        public void CreateSeanceCheckIfOverlap()
        {
            bool multiple = false;
            idSalle = db.salles.Where(x => x.exploitable == true).First().id;
            seance seance = new seance();
            seance.heure_debut = new DateTime(1958, 5, 1, 8, 30, 52);
            seance.nom_seance = nom;
            controllerSeance.Create(seance, idSalle);

            seance = new seance();
            seance.heure_debut = new DateTime(1958, 5, 1, 8, 45, 52);
            seance.nom_seance = nom;
            controllerSeance.Create(seance, idSalle);

            if (db.seances.Where(x => x.nom_seance == nom).Count() > 1)
            {
                multiple = true;
            }

            var seanceFound = db.seances.Where(x => x.nom_seance == nom).ToList().First().id;
            controllerSeance.DeleteSeanceEstpresente(seanceFound);

            Assert.IsFalse(multiple);
        }
    }
}
