//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InmobiliariaAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ESTADO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESTADO()
        {
            this.INMUEBLEs = new HashSet<INMUEBLE>();
        }
    
        public int Codigo_Estado { get; set; }
        public string Nombre_Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INMUEBLE> INMUEBLEs { get; set; }
    }
}
