using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    public class Seances_ViewModel
    {
        public IEnumerable<seance> Seances { get; set; }
        public seance Seance { get; set; }
    }
}