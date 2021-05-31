using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(supplement_partial))]
    public partial class supplement { }
    public partial class supplement_partial
    {
        public int id { get; set; }

        [DisplayName("3D")]
        public bool C3d { get; set; }

        [DisplayName("AVX")]
        public bool ultra_avx { get; set; }

        [DisplayName("Dbox")]
        public bool dbox { get; set; }

        [DisplayName("Imax")]
        public bool imax { get; set; }

        [DisplayName("id salle")]
        [Range(0, 10000000000, ErrorMessage = "{0} doit être entre {1} et {2} charactères")]
        public int id_salle { get; set; }

        public virtual salle salle { get; set; }
    }
}