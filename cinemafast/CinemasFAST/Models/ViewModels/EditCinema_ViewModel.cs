using System.Collections.Generic;
using System.ComponentModel;


namespace CinemasFAST.Models
{
    //Cette classe permet d'accèder à plusieurs portions du modèle et de les inclure dans la même View (Edit des cinémas)
    public class EditCinema_ViewModel
    {
        public IEnumerable<salle> Salles { get; set; }

        public salle SalleCreate { get; set; }

        public salle SalleDetails { get; set; }

        public cinema CinemaDetails { get; set; }

        public responsable ResponsableDetails { get; set; }
    }
}