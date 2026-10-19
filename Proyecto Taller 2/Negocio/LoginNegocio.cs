using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Taller_2.Datos; // Asegúrate de incluir el using de Datos

namespace Proyecto_Taller_2.Negocio
{
    internal class LoginNegocio
    {
        public Usuario VerificarLogin(string correo, string password)
        {
            // Cambiamos UsuarioDAO por UsuarioDatos
            UsuarioDatos conexion = new UsuarioDatos();

            return conexion.ValidarUsuario(correo, password);
        }
    }
}