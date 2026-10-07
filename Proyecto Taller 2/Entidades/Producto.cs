using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Entidades
{
    internal class Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string talle { get; set; } 
        public string edicion { get; set; }
        public string url_imagen { get; set; }
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public DateTime? fecha_baja { get; set; }
        public int id_categoria { get; set; }
        public int id_detalle_venta { get; set; }
        public int id_detalle_compra { get; set; }


        // Constructor
        public Producto()
        {
            nombre = string.Empty;
            descripcion = string.Empty;
            precio = 0.0m;
            stock = 0;
            talle = string.Empty;
            edicion = string.Empty;
            fecha_alta = DateTime.Now;
            fecha_modificacion = DateTime.Now;
            fecha_baja = null;
        }
    }
}
