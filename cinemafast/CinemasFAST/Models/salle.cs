//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemasFAST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class salle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public salle()
        {
            this.seances = new HashSet<seance>();
            this.supplements = new HashSet<supplement>();
        }
    
        public int id { get; set; }
        public int numero { get; set; }
        public int nbr_places { get; set; }
        public string taille_ecran { get; set; }
        public bool exploitable { get; set; }
        public int id_cinema { get; set; }
    
        public virtual cinema cinema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seance> seances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplement> supplements { get; set; }
    }
}
