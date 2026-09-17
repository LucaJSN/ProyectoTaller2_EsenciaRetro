using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class LoginNegocio
    {
        public Usuario VerificarLogin(string correo, string password)
        {
            UsuarioDAO conexion = new UsuarioDAO();

            return conexion.ValidarUsuario(correo, password);
        }
    }
}
