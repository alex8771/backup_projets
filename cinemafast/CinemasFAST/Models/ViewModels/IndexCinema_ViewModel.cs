using CinemasFAST.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models.ViewModels
{
    public class IndexCinema_ViewModel
    {
        public cinema CinemaCreate { get; set; }

        public IEnumerable<cinema> Cinemas { get; set; }
    }
}