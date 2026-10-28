using System.Collections.Generic;
using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Negocio
{
    public class MetodoPagoNegocio
    {
        private MetodoPagoDatos objDatos = new MetodoPagoDatos();

        public List<MetodoPago> CargarMetodosPago()
        {
            return objDatos.ObtenerMetodosPago();
        }
    }
}