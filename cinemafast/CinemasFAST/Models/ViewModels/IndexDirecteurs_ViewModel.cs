using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models.ViewModels
{
    public class IndexDirecteurs_ViewModel
    {
        public directeur DirecteurCreate { get; set; }

        public IEnumerable<directeur> Directeurs { get; set; }
    }
}