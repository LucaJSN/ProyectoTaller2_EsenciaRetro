using MySqlConnector;
using Proyecto_Taller_2.Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //Para validar correo
using System.Threading.Tasks;
using System.Windows.Forms;

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

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                throw new Exception("El Apellido del usuario no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                throw new Exception("Debe ingresar un correo para el usuario");
            }

            if (datosUsuario.ExisteCorreo(usuario.Correo.ToString()))
            {
                throw new Exception("El correo ingresado ya tiene una cuenta registrada");
            }

            if (string.IsNullOrWhiteSpace(usuario.Password))
            {
                throw new Exception("Debe ingresar una contreseña para el usuario");
            }

            if (usuario.RolId <= 0)
            {
                throw new Exception("Debe seleccionar un rol válido.");
            }

            if (string.IsNullOrWhiteSpace(direccion.Provincia))
            {
                throw new Exception("Debe ingresar una Provincia");
            }

            if (string.IsNullOrWhiteSpace(direccion.Ciudad))
            {
                throw new Exception("Debe ingresar una Ciudad");
            }

            if (string.IsNullOrWhiteSpace(direccion.Calle))
            {
                throw new Exception("Debe ingresar una Calle");
            }

            if (direccion.Altura == 0)
            {
                throw new Exception("Debe ingresar una Altura");
            }

            // Podrías agregar más lógica aquí (Ej: validar formato de correo, 
            // buscar si el correo ya existe en la BD antes de intentar guardarlo, etc.)


            // 2. COMUNICACIÓN CON DATOS
            // Si todo está correcto, le pasamos la pelota a la capa de Datos
            return datosUsuario.InsertarUsuarioYDireccion(usuario, direccion);
        }

        public void ActivarUsuario(int idUsuario)
        {
            UsuarioDatos datos = new UsuarioDatos();
            datos.ActivarUsuario(idUsuario);
        }

        public bool ActualizarUsuario(Usuario usuario, Direccion direccion)
        {
            try
            {
                // Instancias tu clase de la capa de datos
                UsuarioDatos datos = new UsuarioDatos();

                // 1. REGLAS DE NEGOCIO (Validaciones extra)
                // Aquí te aseguras de que no llegue basura a la base de datos
                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    throw new Exception("El nombre del usuario no puede estar vacío.");
                }

                if (string.IsNullOrWhiteSpace(usuario.Apellido))
                {
                    throw new Exception("El Apellido del usuario no puede estar vacío.");
                }

                if (string.IsNullOrWhiteSpace(usuario.Correo))
                {
                    throw new Exception("Debe ingresar un correo para el usuario");
                }

                if (datosUsuario.ExisteCorreo(usuario.Correo.ToString(), usuario.IdUsuario))
                {
                    throw new Exception("El correo ingresado ya pertenece a otra cuenta registrada.");
                }

                if (string.IsNullOrWhiteSpace(usuario.Password))
                {
                    throw new Exception("Debe ingresar una contreseña para el usuario");
                }

                if (usuario.RolId <= 0)
                {
                    throw new Exception("Debe seleccionar un rol válido.");
                }

                if (string.IsNullOrWhiteSpace(direccion.Provincia))
                {
                    throw new Exception("Debe ingresar una Provincia");
                }

                // Llamas al método que actualiza en la base de datos y retornas el resultado
                return datos.ActualizarUsuarioYDireccion(usuario, direccion);
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o relanzarlo para que lo capture el formulario
                throw new Exception("Error en la capa de negocio al actualizar el usuario: " + ex.Message);
            }
        }


        public bool DarDeBajaUsuario(int idUsuario)
        {
            // Regla de negocio opcional: puedes validar que el ID sea mayor a 0
            if (idUsuario <= 0)
            {
                throw new Exception("ID de usuario inválido para dar de baja.");
            }

            // Instanciamos la capa de datos si no la tienes como atributo global
            UsuarioDatos datosUsuario = new UsuarioDatos();

            // Llamamos al método de la capa de datos que actualiza la fecha_baja
            return datosUsuario.DarDeBajaUsuario(idUsuario);
        }
        public List<Usuario> ListarTodosLosUsuarios()
        {
            UsuarioDatos datos = new UsuarioDatos();
            return datos.ObtenerTodosLosUsuarios();
        }
    }
}
