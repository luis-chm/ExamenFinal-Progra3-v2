using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenFinal.CapaDatos
{
    public class ClsVentas
    {
        public int IDVenta { get; set; }
        public int IDAgente { get; set; }
        public int IDCliente { get; set; }
        public int IDCasa { get; set; }
        public DateTime Fecha { get; set; }
    }
}