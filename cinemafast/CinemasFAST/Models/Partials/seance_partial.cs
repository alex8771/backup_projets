using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(seance_partial))]
    public partial class seance 
    {
        [DisplayName("Film principal")]
        public string Filmisprincipal { get; set; }
    }

    public partial class seance_partial
    {
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DisplayName("Heure de début")]
        public DateTime heure_debut { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DisplayName("Heure de fin")]
        public DateTime heure_fin { get; set; }

        [DisplayName("Nom de la séance")]
        [Required(ErrorMessage = "Le nom de la séance est requis")]
        [MaxLength(45, ErrorMessage = "{0} doit avoir un maximum de {1} charactères")]
        public string nom_seance { get; set; }

        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_salle { get; set; }

        [DisplayName("Salle")]
        public virtual salle salle { get; set; }
    }
}