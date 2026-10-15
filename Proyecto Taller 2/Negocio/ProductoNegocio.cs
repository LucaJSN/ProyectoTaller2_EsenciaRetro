using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class ProductoNegocio
    {
        private ProductoDatos datos = new ProductoDatos();
        public List<Producto> ListarTodosLosProductos()
        {
            return datos.ObtenerTodosLosProductos();
        }
        public bool InsertarProducto(Producto producto)
        {
            return datos.InsertarProducto(producto);
        }

        public bool ActualizarProducto(Producto producto)
        {
            return datos.ActualizarProducto(producto);
        }

        public void BajaProducto(int idProducto)
        {
            datos.BajaProducto(idProducto);
        }

        public void ActivarProducto(int idProducto)
        {
            datos.ActivarProducto(idProducto);
        }
    }
}
