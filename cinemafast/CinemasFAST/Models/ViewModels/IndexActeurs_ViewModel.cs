using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models.ViewModels
{
    public class IndexActeurs_ViewModel
    {
        public acteur ActeurCreate { get; set; }

        public IEnumerable<acteur> Acteurs { get; set; }
    }
}