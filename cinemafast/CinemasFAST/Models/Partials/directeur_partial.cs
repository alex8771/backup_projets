using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(directeur_partial))]
    public partial class directeur { }
    public partial class directeur_partial
    {
        public int id { get; set; }

        [DisplayName("Nom du directeur")]
        [Required(ErrorMessage = "Le nom du directeur est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom { get; set; }

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Le prénom du directeur est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string prenom { get; set; }

        [DisplayName("Langue")]
        [Required(ErrorMessage = "La langue parlée du directeur est requise")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string langue { get; set; }

        [DisplayName("Date de naissance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La date de naissance est requise")]
        public System.DateTime date_naissance { get; set; }

        [DisplayName("Lieu de naissance")]
        [Required(ErrorMessage = "Le lieu de naissance requis")]
        [MaxLength(75, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string lieu_naissance { get; set; }

        [DisplayName("Photo")]
        [MaxLength(500, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string image { get; set; }
    }
}