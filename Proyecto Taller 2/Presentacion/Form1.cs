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

        public Form1(Usuario usuario)
        {
            InitializeComponent();
            usuarioActivo = usuario;

            string tipoRol = usuarioActivo?.Rol?.tipo ?? "Sin Rol";
            this.Text = $"Sistema de Gestión - Usuario: {usuarioActivo?.Nombre} {usuarioActivo?.Apellido} ({tipoRol})";

            // Carga la pantalla de inicio al iniciar
            AbrirModulo(new UC_Dashboard());
        }

        public Form1()
        {
            InitializeComponent();
            AbrirModulo(new UC_Dashboard());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (button1 != null)
            {
                button1.Enabled = true;
                button1.ForeColor = Color.White;
            }

            if (btnProductos != null)
            {
                btnProductos.Enabled = true;
                btnProductos.ForeColor = Color.White;
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
            if (usuarioActivo == null || usuarioActivo.Rol == null)
            {
                // Si no hay información de usuario, se asume acceso total para pruebas locales
                return true;
            }

            string rol = usuarioActivo.Rol.tipo.Trim().ToLower();

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
                AbrirModulo(new UC_Backup());
            }
        }

        #endregion

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}