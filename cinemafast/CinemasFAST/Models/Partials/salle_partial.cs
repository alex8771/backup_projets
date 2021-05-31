using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(salle_partial))]
    public partial class salle { }
    public partial class salle_partial
    {
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id { get; set; }

        [DisplayName("Numéro de la salle")]
        [Required(ErrorMessage = "Entrez un numéro de salle")]
        [Range(0, 30, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int numero { get; set; }

        [DisplayName("Nombre de places")]
        [Required(ErrorMessage = "Vous devez spécifier le nombre de places de la salle. [1,300]")]
        [Range(1, 300)]
        public int nbr_places { get; set; }

        [DisplayName("Taille de l'écran (en mètres)")]
        [Required(ErrorMessage = "Entrez la taille de l'écran")]
        [Range(0, 100, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public string taille_ecran { get; set; }

        [DisplayName("En service")]
        public bool exploitable { get; set; }

        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_cinema { get; set; }

        public virtual cinema cinema { get; set; }
    }
}