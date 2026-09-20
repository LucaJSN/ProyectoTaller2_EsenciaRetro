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
            Usuario usuarioActual = usuario;
            usuarioActivo = usuarioActual;

            this.Text = $"Sistema de Gestión - Usuario: {usuarioActual.Nombre} {usuarioActual.Apellido} ({usuarioActual.Rol.tipo})";

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
            // Mantiene los botones habilitados
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

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Dashboard());
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Balance());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Productos());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Ventas());
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Admin());
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Backup());
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}