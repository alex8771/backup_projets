using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    public class EstPresenter_ViewModel
    {
        public seance Seance { get; set; }

        public IEnumerable<seance> seances { get; set; }

        //public film Film { get; set; }

        //public IEnumerable<film> films { get; set; }

        public estpresente estpresente { get; set; }

        public IEnumerable<estpresente> estpresentes { get; set; }
    }
}