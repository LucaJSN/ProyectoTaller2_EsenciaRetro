using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Proyecto_Taller_2
{
    public partial class Form1 : Form
    {
        private Usuario usuarioActivo;
        public bool CerroSesionParaReingresar { get; private set; } = false;

        public Form1(Usuario usuario)
        {
            InitializeComponent();

            // 1. Cargamos y mostramos los datos del usuario inicial
            ActualizarDatosUsuario(usuario);

            // 2. Carga la pantalla de inicio al iniciar
            AbrirModulo(new UC_Dashboard());
        }

        /// <summary>
        /// Reasigna el usuario activo y refresca la barra superior y título del formulario.
        /// </summary>
        public void ActualizarDatosUsuario(Usuario usuario)
        {
            usuarioActivo = usuario;

            string tipoRol = usuarioActivo?.rol?.tipo ?? "Sin Rol";

            this.Text = $"Sistema de Gestión - Usuario: {usuarioActivo?.nombre} {usuarioActivo?.apellido} ({tipoRol})";
            AplicarPermisos();
        }

        public Form1()
        {
            InitializeComponent();
            AbrirModulo(new UC_Dashboard());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (button1 != null)
            //{
            //    button1.Enabled = true;
            //    button1.ForeColor = Color.White;
            //}

            //if (btnProductos != null)
            //{
            //    btnProductos.Enabled = true;
            //    btnProductos.ForeColor = Color.White;
            //}
            AplicarPermisos();
        }

        //Probando deshbabilitar boton segun el rol del usuario
        private void AplicarPermisos()
        {
            if (usuarioActivo == null || usuarioActivo.rol == null) return;

            string tipoRol = usuarioActivo.rol.tipo;

            // Opción A: Evaluando con condicionales directos
            if (tipoRol == "Vendedor")
            {
                button1.Enabled = false;
                btnProductos.Enabled = false;
                btnBackUp.Enabled = false;
            }
            if(tipoRol == "Supervisor")
            {
                button1.Enabled = false;
                btnBackUp.Enabled = false;
            }
        }

        // Método encargado de cambiar la vista dentro del contenedor principal
        private void AbrirModulo(UserControl modulo)
        {
            if (BtnAdmin != null)
            {
                BtnAdmin.Controls.Clear();
                modulo.Dock = DockStyle.Fill;
                BtnAdmin.Controls.Add(modulo);
                BtnAdmin.Tag = modulo;
                modulo.BringToFront();
            }
        }

        /// <summary>
        /// Evalúa si el usuario activo tiene permiso para acceder al módulo solicitado.
        /// Retorna true si tiene acceso, o false si no posee el rol adecuado.
        /// </summary>
        private bool ValidarAccesoModulo(string moduloSolicitado)
        {
            if (usuarioActivo == null || usuarioActivo.rol == null)
            {
                // Si no hay información de usuario, se asume acceso total para pruebas locales
                return true;
            }

            string rol = usuarioActivo.rol.tipo.Trim().ToLower();

            // Administrador tiene acceso irrestricto
            if (rol.Contains("admin"))
            {
                return true;
            }

            // Vendedor
            if (rol.Contains("vendedor"))
            {
                if (moduloSolicitado == "Inicio" || moduloSolicitado == "Productos" || moduloSolicitado == "Ventas")
                {
                    return true;
                }

                MostrarAlertaAccesoDenegado(moduloSolicitado, "Administrador o Supervisor");
                return false;
            }

            // Supervisor
            if (rol.Contains("supervisor") || rol.Contains("gerente"))
            {
                if (moduloSolicitado == "Inicio" || moduloSolicitado == "Productos" || moduloSolicitado == "Ventas" || moduloSolicitado == "Balance")
                {
                    return true;
                }

                MostrarAlertaAccesoDenegado(moduloSolicitado, "Administrador");
                return false;
            }

            // Para cualquier otro rol no contemplado
            MostrarAlertaAccesoDenegado(moduloSolicitado, "Administrador");
            return false;
        }

        private void MostrarAlertaAccesoDenegado(string modulo, string rolRequerido)
        {
            MessageBox.Show($"Acceso Denegado.\n\nNo posee los permisos necesarios para acceder al módulo de '{modulo}'.\nSe requiere nivel de acceso: {rolRequerido}.",
                            "Restricción de Seguridad",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
        }


        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            CerroSesionParaReingresar = true;
            this.Close(); // Cierra FormPrincipal y el ciclo 'while' de Program.cs vuelve a pedir Login
        }

        #region --- EVENTOS DE BOTONES DEL MENÚ ---

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("Inicio"))
            {
                AbrirModulo(new UC_Dashboard());
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("Productos"))
            {
                AbrirModulo(new UC_Productos());
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("Ventas"))
            {
                AbrirModulo(new UC_Ventas());
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("Balance"))
            {
                AbrirModulo(new UC_Balance());
            }
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("Administrador"))
            {
                AbrirModulo(new UC_Admin());
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            if (ValidarAccesoModulo("BackUp"))
            {
                AbrirModulo(new UC_BackUp()); // <-- Corregido con el guion bajo
            }
        }

        #endregion

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdmin_Click_1(object sender, EventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            // 1. Ocultamos la ventana principal
            this.Hide();

            // 2. Abrimos una instancia limpia del Login
            using (FormLogin login = new FormLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // 3. Actualizamos los datos del usuario activo
                    ActualizarDatosUsuario(login.UsuarioAutenticado);

                    // 4. Mostramos nuevamente la ventana principal
                    this.Show();

                    // 5. Cargamos el Dashboard
                    AbrirModulo(new UC_Dashboard());
                }
                else
                {
                    // Si cancela o cierra el Login, cerramos la aplicación
                    this.Close();
                }
            }
        }
    }
}