using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(film_partial))]
    public partial class film { }
    public partial class film_partial
    {
        private static cinemadbEntities db = new cinemadbEntities();

        public int id { get; set; }

        [DisplayName("Titre du film")]
        [Required(ErrorMessage = "Le titre du film est requis")]
        [MaxLength(75, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string titre { get; set; }

        [DisplayName("Type de film")]
        [Required(ErrorMessage = "Le type du film est requis")]
        [MaxLength(25, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string type { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Synopsis")]
        [MaxLength(500, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        [Required(ErrorMessage = "Un court synopsis est requis")]
        public string description { get; set; }

        [DisplayName("Année de sortie")]
        [Required(ErrorMessage = "La date de sortie du film est requise")]
        [Range(1920, 2500, ErrorMessage = "{0} doit être entre {1} et {2}")]
        public int annee { get; set; }

        [DisplayName("Durée (en minutes)")]
        [Required(ErrorMessage = "La durée du film est requise")]
        [Range(1, 300, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int duree { get; set; }

        [DisplayName("Note")]
        [Range(0, 10, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        [Required(ErrorMessage = "Une note de 1 à 10 est exigée")]
        public Nullable<double> note { get; set; }

        [DisplayName("Nombre de votes")]
        [Required(ErrorMessage = "Le nombre des votes totaux est requis")]
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int nbr_votes { get; set; }

        [DisplayName("Revenu au Box Office (en millions)")]
        [Range(0, 300, ErrorMessage = "{0} doit être entre {1} et {2} millions")]
        public Nullable<double> revenue { get; set; }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "Vous devez sélectionner au moins un genre")]
        public int id_genre { get; set; }

        [DisplayName("Affiche du film")]
        [MaxLength(500, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string image { get; set; }

        /* Split les données du fichier.csv entre chaque virgules afin de remplir les valeurs de l'objet film */
        public static film FromCsv(string csvLine)
        {
            film film = new film();
            string[] data = Regex.Split(csvLine, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            film.titre = data[1].ToString();
            film.description = data[3].ToString();
            film.annee = TryParseInt(data[6]);
            film.duree = TryParseInt(data[7]);
            film.note = TryParseDouble(data[8]);
            film.nbr_votes = TryParseInt(data[9]);
            film.revenue = TryParseDouble(data[10]);
            return film;
        }

        public static double TryParseDouble(string nombre)
        {
            if (Double.TryParse(nombre, out double nbrConvert))
                return nbrConvert;

            return 0;
        }

        public static int TryParseInt(string nombre)
        {
            if (Int32.TryParse(nombre, out int nbrConvert))
                return nbrConvert;

            return 0;
        }

        /* Retourne le genre du film */
        public static string FromCsvGenre(string csvLine)
        {
            string[] valeurs = csvLine.Split(',');
            string genre;
            char premierChar;
            /* Lis les trois premiers caractères du string afin de déterminer le premier genre */
            genre = valeurs[2].Substring(0, 3);
            premierChar = Convert.ToChar(valeurs[2].Substring(0, 1));
            // Si le premier charactère est un guillemet, on l'ignore.
            if (premierChar == '"')
                genre = valeurs[2].Substring(1, 3);
            switch (genre)
            {
                case "Act":
                    genre = "action";
                    break;
                case "Com":
                    genre = "comédie";
                    break;
                case "Adv":
                    genre = "aventure";
                    break;
                case "Hor":
                    genre = "horreur";
                    break;
                case "Ani":
                    genre = "animation";
                    break;
                case "Bio":
                    genre = "biographie";
                    break;
                case "Dra":
                    genre = "drame";
                    break;
                case "Cri":
                    genre = "policier";
                    break;
                case "Mys":
                    genre = "mystère";
                    break;
                case "Sci":
                    genre = "science-fiction";
                    break;
                case "Rom":
                    genre = "romance amoureuse";
                    break;
                case "His":
                    genre = "historique";
                    break;
                case "Fan":
                    genre = "fantasy";
                    break;
                case "Thr":
                    genre = "thriller";
                    break;
                default:
                    genre = "autre";
                    break;
            }
            return genre;
        }
        //Lire un fichier Csv pour les directeurs ou les acteurs.
        public static string FromCsvPersonnel(string csvLine, int colonne)
        {
            string[] data = Regex.Split(csvLine, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            string valeursPersonne = data[colonne];            
            return valeursPersonne;
        }
        //Lire un fichier Csv pour les directions
        public static direction FromCsvDirection(string csvLine)
        {

            string[] data = Regex.Split(csvLine, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            string valeurTitreFilm = data[1];
            string valeurDirecteur = data[4];

            string[] prenomNom = valeurDirecteur.Split(' ');
            string prenom = prenomNom[0];
            string nom = "";
            if (prenomNom.Length > 1)
            {
                //Le nombre de noms de famille dans la string
                int nbrNom = prenomNom.Length - 1;
                //Ajoutera tous les noms de famille dans la string
                for (int i = 1; i <= nbrNom; i++)
                {
                    nom += prenomNom[i] + " ";
                }
            }
            else
            {
                nom = prenomNom[0];
            }
            direction direct = new direction();
            film filmDirige = db.films.Where(f => f.titre == valeurTitreFilm).FirstOrDefault();           
            directeur directeur = db.directeurs.Where(f => f.prenom == prenom && f.nom == nom).FirstOrDefault();
            direct.id_film = filmDirige.id;
            direct.id_directeur = directeur.id;
            return direct;
        }        
    }
}