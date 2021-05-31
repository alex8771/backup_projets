using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models.ViewModels
{
    public class IndexFilm_ViewModel
    {
        public film FilmCreate { get; set; }

        public IEnumerable<film> Films { get; set; }
    }
}