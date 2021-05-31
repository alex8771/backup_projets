using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(responsable_partial))]
    public partial class responsable { }
    public partial class responsable_partial
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Le nom du responsable est requis")]
        [DisplayName("Nom du responsable")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom { get; set; }

        [Required(ErrorMessage = "Le prénom du responsable est requis")]
        [DisplayName("Prénom")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        [DisplayFormat(DataFormatString = "{0} ")]
        public string prenom { get; set; }

        [DisplayName("# de Téléphone")]
        [Phone]
        [RegularExpression(@"^\(?([418|581|819|514]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Le numéro de téléphone doit être du format \"(418) 123-4567\"")]
        [Required(ErrorMessage = "Le numéro de téléphone du responsable est requis")]
        public string telephone { get; set; }

        [DisplayName("Adresse de courriel")]
        [EmailAddress]
        [Required(ErrorMessage = "L'adresse de courriel du responsable est requis")]
        [MaxLength(75, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string courriel { get; set; }
    }
}