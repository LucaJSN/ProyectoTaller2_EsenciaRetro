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
            if (string.IsNullOrWhiteSpace(producto.nombre))
            {
                throw new Exception("El nombre del producto no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(producto.descripcion))
            {
                producto.descripcion = "Sin Descripción";
            }

            if (string.IsNullOrWhiteSpace(producto.talle))
            {
                throw new Exception("El talle del producto no puede estar vacío.");
            }

            if (producto.precio_costo <= 0)
            {
                throw new Exception("El precio de costo no puede ser 0.");
            }

            if (producto.precio_venta <= 0)
            {
                throw new Exception("El precio de Venta no puede ser 0");
            }

            if (producto.precio_costo >= producto.precio_venta)
            {
                throw new Exception("El precio de Costo no puede ser igual o superior al precio de venta");
            }

                return datos.InsertarProducto(producto);
        }

        public bool ActualizarProducto(Producto producto)
        {
            return datos.ActualizarProducto(producto);
        }

        public bool BajaProducto(int idProducto)
        {
            return datos.BajaProducto(idProducto);
        }

        public void ActivarProducto(int idProducto)
        {
            datos.ActivarProducto(idProducto);
        }
    }
}
