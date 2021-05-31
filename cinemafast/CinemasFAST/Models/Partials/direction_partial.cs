using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(direction_partial))]
    public partial class direction { }
    public partial class direction_partial
    {
        public int id { get; set; }

        public int id_film { get; set; }

        [DisplayName("Nom du directeur")]
        public int id_directeur { get; set; }

        //[MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")] <--- No Bueno
        [DisplayName("Nom du directeur")]
        public virtual directeur directeur { get; set; }

        //[MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")] <--- No Bueno
        [DisplayName("Titre du film")]
        public virtual film film { get; set; }
    }
}