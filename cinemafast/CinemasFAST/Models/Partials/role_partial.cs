using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(role_partial))]
    public partial class role { }
    public partial class role_partial
    {
        public int id { get; set; }

        [DisplayName("id film")]
        [Required(ErrorMessage = "ID du film requis.")]
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_film { get; set; }

        [DisplayName("Nom de l'acteur")]
        [Required(ErrorMessage = "ID de l'acteur requis.")]
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_acteur { get; set; }

        [DisplayName("Nom du personnage")]
        [Required(ErrorMessage = "Veuillez fournir le nom du personnage de l'acteur")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom_personnage { get; set; }

        [DisplayName("Acteur")]
        public virtual acteur acteur { get; set; }

        [DisplayName("Film")]
        public virtual film film { get; set; }
    }
}