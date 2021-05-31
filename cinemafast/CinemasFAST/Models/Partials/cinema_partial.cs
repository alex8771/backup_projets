using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(cinema_partial))]
    public partial class cinema { }
    public partial class cinema_partial
    {
        public int id { get; set; }

        [DisplayName("Nom du cinéma")]
        [Required(ErrorMessage = "Le nom du cinéma est requis")]
        [MaxLength(100, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom { get; set; }

        [DisplayName("Adresse")]
        [Required(ErrorMessage = "L'adresse du cinéma est requise")]
        [MaxLength(150, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string adresse { get; set; }

        [DisplayName("Numéro de téléphone")]
        [Phone]
        [MaxLength(30, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        [RegularExpression(@"^\(?([418|581|819|514]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "{0} doit avoir un code régional valide (418,581,819,514)")]
        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        public string telephone { get; set; }

        [DisplayName("Responsable")]
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_responsable { get; set; }

        [DisplayName("Responsable")]
        public virtual responsable responsable { get; set; }
    }
}