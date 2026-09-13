using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Taller_2.Datos;

namespace Proyecto_Taller_2.Negocio
{
    internal class UsuarioNegocio
    {
        // Instanciamos la clase de la capa de datos
        private UsuarioDatos datosUsuario = new UsuarioDatos();

        // Método que será llamado desde el botón de la interfaz
        public bool RegistrarUsuario(Usuario usuario, Direccion direccion)
        {
            // 1. REGLAS DE NEGOCIO (Validaciones extra)
            // Aquí te aseguras de que no llegue basura a la base de datos
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new Exception("El nombre del usuario no puede estar vacío.");
            }

            if (usuario.RolId <= 0)
            {
                throw new Exception("Debe seleccionar un rol válido.");
            }

            // Podrías agregar más lógica aquí (Ej: validar formato de correo, 
            // buscar si el correo ya existe en la BD antes de intentar guardarlo, etc.)

            // 2. COMUNICACIÓN CON DATOS
            // Si todo está correcto, le pasamos la pelota a la capa de Datos
            return datosUsuario.InsertarUsuarioYDireccion(usuario, direccion);
        }
    }
}
