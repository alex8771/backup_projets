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
    
    public partial class estpresente
    {
        public int id { get; set; }
        public int id_film { get; set; }
        public int id_seance { get; set; }
        public bool isprincipal { get; set; }
    
        public virtual seance seance { get; set; }
        public virtual film film { get; set; }
    }
}
