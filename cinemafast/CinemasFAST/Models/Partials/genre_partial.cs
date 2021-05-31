using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    [MetadataType(typeof(genre_partial))]
    public partial class genre { }
    public partial class genre_partial
    {

        public int id { get; set; }

        [DisplayName("Nom du genre ?????????????????????????????")]
        [MaxLength(45, ErrorMessage = "{0} doit être de {1} charactères maximum")]
        public string nom { get; set; }
    }
}