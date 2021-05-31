using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(tarifs_partial))]
    public partial class tarifs { }
    public partial class tarifs_partial
    {
        public int id { get; set; }

        [DisplayName("Prix adulte")]
        public double prix_adulte { get; set; }

        [DisplayName("Prix enfant")]
        public double prix_enfant { get; set; }

        [DisplayName("Prix aîné")]
        public double prix_aine { get; set; }

        [DisplayName("Prix 3D")]
        public double prix_3d { get; set; }

        [DisplayName("Prix AVX")]
        public double prix_ultra_avx { get; set; }

        [DisplayName("Prix Dbox")]
        public double prix_dbox { get; set; }

        [DisplayName("Prix Imax")]
        public double prix_imax { get; set; }

        [DisplayName("id_cinema")]
        public int id_cinema { get; set; }

        [DisplayName("Cinéma")]
        public virtual cinema cinema { get; set; }
    }
}