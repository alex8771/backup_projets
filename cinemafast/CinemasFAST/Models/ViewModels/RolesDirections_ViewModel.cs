using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    public class RolesDirections_ViewModel
    {
        public IEnumerable<role> Roles { get; set; }
        public role Role { get; set; }

        public IEnumerable<acteur> Acteurs { get; set; }
        public acteur Acteur { get; set; }

        public IEnumerable<direction> Directions { get; set; }
        public direction Direction { get; set; }

        public directeur Directeur { get; set; }

        public IEnumerable<film> Films { get; set; }
        public film Film { get; set; }
    }
}