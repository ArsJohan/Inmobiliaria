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
    
    public partial class VENTA
    {
        public int Codigo_Transaccion { get; set; }
        public System.DateTime Fecha_Pago { get; set; }
        public decimal Monto_Pagado { get; set; }
    
        public virtual TRANSACCION TRANSACCION { get; set; }
    }
}
