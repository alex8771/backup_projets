using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(presentation_partial))]
    public partial class estpresente { }

    public partial class presentation_partial
    {
        public int id { get; set; }

        public int id_film { get; set; }

        public int id_seance { get; set; }

        [DisplayName("Film principal")]
        public bool isprincipal { get; set; }

        public virtual seance seance { get; set; }
        public virtual film film { get; set; }
    }
}