using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(profil_partial))]
    public partial class profil { }
    public partial class profil_partial
    {
        public int id { get; set; }

        [RegularExpression(@"^[a-zA-ZàéèäïëÀÉÈÄÏË -]+$", ErrorMessage = "Votre nom ne peut pas contenir de caractères spéciaux ou de chiffres")]
        [DisplayName("Nom complet")]
        [MaxLength(100, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom_complet { get; set; }

        [DisplayName("Nom d'utilisateur")]
        [RegularExpression("^[a-zA-Z._0-9]+$", ErrorMessage = "Le nom d'utilisateur ne peut contenir que des lettres, des chiffres et les charactères suivants: . _")]
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string username { get; set; }

        [DisplayName("Mot de passe")]
        [RegularExpression("^[a-zA-Z.-_0-9!/$%&*#]{6,45}$", ErrorMessage = "{0} doit comporter entre 6 et 45 charactères. Les charactères spéciaux acceptés sont: ! / $ # % & *")]
        [Required(ErrorMessage = "Un mot de passe est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string password { get; set; }

        [DisplayName("Adresse de courriel")]
        [EmailAddress]
        [Required(ErrorMessage = "Votre adresse de courriel est requis")]
        [MaxLength(75, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string courriel { get; set; }

        [DisplayName("Type du compte")]
        [Required(ErrorMessage = "Un type de compte est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string type { get; set; }
    }
}