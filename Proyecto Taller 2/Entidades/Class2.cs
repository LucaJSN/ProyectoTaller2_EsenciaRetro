using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public bool Activo { get; set; }
        public string Talle {  get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public DateTime? FechaBaja { get; set; }

        
        public Producto()
        {
            Nombre = string.Empty;
            Descripcion = string.Empty;
            RutaImagen = string.Empty;
            FechaBaja = null;
        }

        
        public Producto(int idProducto, string nombre, string descripción, decimal precioCosto, decimal precioVenta, int cantidad, string categoria, bool activo, string talle, int stock, string rutaImagen, DateTime? fechaBaja = null)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripción;
            PrecioCosto = precioCosto;
            PrecioVenta = precioVenta;
            Cantidad = cantidad;
            Categoria = categoria;
            Activo = activo;
            Talle = talle;
            Stock = stock;
            RutaImagen = rutaImagen;
            FechaBaja = fechaBaja;
        }
    }
}
