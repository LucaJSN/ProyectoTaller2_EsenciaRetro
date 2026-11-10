using System;
using System.Data;
using Proyecto_Taller_2.Datos;

namespace Proyecto_Taller_2.Negocio
{
    public class BalanceNegocio
    {
        private BalanceDatos objDatos = new BalanceDatos();

        public DataTable ObtenerVentasPorColeccion(DateTime desde, DateTime hasta)
        {
            return objDatos.ObtenerVentasPorColeccion(desde, hasta);
        }

        public DataTable ObtenerVentasPorMetodoPago(DateTime desde, DateTime hasta)
        {
            return objDatos.ObtenerVentasPorMetodoPago(desde, hasta);
        }
        public DataTable ObtenerVentasPorMes(DateTime desde, DateTime hasta)
        {
            return objDatos.ObtenerVentasPorMes(desde, hasta);
        }
        public DataTable ObtenerTop5Productos(DateTime desde, DateTime hasta)
        {
            return objDatos.ObtenerTop5Productos(desde, hasta);
        }
    }
}