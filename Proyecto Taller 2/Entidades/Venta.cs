using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Entidades
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal MontoTotal { get; set; }
        public int IdMetodoPago { get; set; }
        public string MetodoPago { get; set; }

        // Lista para los ítems del carrito asociados a esta venta
        public List<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    }
}
