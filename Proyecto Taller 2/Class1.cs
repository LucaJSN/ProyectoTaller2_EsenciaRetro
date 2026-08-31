using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Taller_2
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Edicion { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Talle { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public string RutaImagen { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaBaja { get; set; }

        // Constructor para darle valores iniciales seguros por defecto
        public Producto()
        {
            Nombre = string.Empty;
            Edicion = string.Empty;
            Descripcion = string.Empty;
            Talle = string.Empty;
            Categoria = string.Empty;
            RutaImagen = string.Empty;
            Activo = true;
            FechaBaja = null;
        }
    }
}