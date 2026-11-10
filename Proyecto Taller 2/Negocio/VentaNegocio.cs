using System.Data;
using System.Collections.Generic;
using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Negocio
{
    public class VentaNegocio
    {
        private VentaDatos objDatos = new VentaDatos();

        public bool RegistrarVenta(int idUsuario, int idCliente, int idMetodoPago, string tipoFactura, decimal totalVenta, List<DetalleVenta> listaDetalle, out string mensaje)
        {
            // Restricciones de Negocio
            if (listaDetalle == null || listaDetalle.Count == 0)
            {
                mensaje = "El carrito de compras está vacío.";
                return false;
            }

            if (totalVenta <= 0)
            {
                mensaje = "El monto total de la venta debe ser mayor a cero.";
                return false;
            }

            if (idUsuario <= 0)
            {
                mensaje = "Debe registrar un usuario vendedor válido.";
                return false;
            }

            return objDatos.RegistrarVentaSP(idUsuario, idCliente, idMetodoPago, tipoFactura, totalVenta, listaDetalle, out mensaje);
        }

        public DataTable ObtenerClientes()
        {
            // Llama al método ListarClientes de la capa de datos
            return objDatos.ListarClientes();
        }

        public DataTable ObtenerProductos()
        {
            // Llama al método ListarProductos de la capa de datos
            return objDatos.ListarProductos();
        }

        public DataTable ObtenerDetalleVentaPorId(int idVenta)
        {
            return objDatos.ObtenerDetalleVentaPorId(idVenta);
        }

        public DataTable ObtenerHistorialVentas()
        {
            return objDatos.ObtenerHistorialVentas();
        }

        public DataTable ObtenerClientePorId(int idCliente)
        {
            VentaDatos datos = new VentaDatos();
            return datos.ObtenerClientePorId(idCliente);
        }

        public DataTable ObtenerProductoPorId(int idProducto)
        {
            VentaDatos datos = new VentaDatos();
            return datos.ObtenerProductoPorId(idProducto);
        }
    }
}