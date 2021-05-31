using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemasFAST.Models
{
    //Cette classe permet d'accèder à plusieurs portions du modèle et de les inclure dans la même View (Edit des salles)
    public class EditSalle_ViewModel
    {       
        public salle SalleDetails { get; set; }
        public supplement SupplementDetails { get; set; }       
    }
}