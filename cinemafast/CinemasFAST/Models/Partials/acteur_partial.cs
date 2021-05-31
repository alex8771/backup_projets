using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(acteur_partial))]
    public partial class acteur { }

    public partial class acteur_partial
    {

        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id { get; set; }

        [DisplayName("Nom de l'acteur")]
        [Required(ErrorMessage = "Entrez le nom de l'acteur")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom { get; set; }

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Entrez le prénom de l'acteur")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string prenom { get; set; }

        [DisplayName("Langue")]
        [Required(ErrorMessage = "Entrez la langue parlée de l'acteur")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string langue { get; set; }

        [DisplayName("Date de naissance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La date de naissance est requise")]
        public System.DateTime date_naissance { get; set; }

        [DisplayName("Lieu de naissance")]
        [Required(ErrorMessage = "Le lieu de naissance est requis")]
        [MaxLength(75, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string lieu_naissance { get; set; }

        [DisplayName("Photo")]
        [MaxLength(500, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string image { get; set; }
    }
}