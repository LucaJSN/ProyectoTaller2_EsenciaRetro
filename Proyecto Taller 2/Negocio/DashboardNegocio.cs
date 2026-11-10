using System;
using System.Data;
using Proyecto_Taller_2.Datos;

namespace Proyecto_Taller_2.Negocio
{
    public class DashboardNegocio
    {
        private DashboardDatos objDatos = new DashboardDatos();

        public decimal ObtenerRecaudacionDelDia()
        {
            return objDatos.ObtenerRecaudacionDelDia();
        }

        public int ObtenerCamisetasVendidasDelDia()
        {
            return objDatos.ObtenerCamisetasVendidasDelDia();
        }

        public DataTable ObtenerVentasDelDiaParaGrilla()
        {
            return objDatos.ObtenerVentasDelDiaParaGrilla();
        }
    }
}