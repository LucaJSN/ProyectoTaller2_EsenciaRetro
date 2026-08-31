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
        private void Form1_Load(object sender, EventArgs e)
        {
            // Código a ejecutar cuando la ventana termine de cargar
        }
        public Form1()
        {
            InitializeComponent();
            // Carga la pantalla de inicio al abrir el programa
            AbrirModulo(new UC_Dashboard());
        }

        // Método encargado de cambiar la vista dentro del panel contenedor
        private void AbrirModulo(UserControl modulo)
        {
            panelContenedor.Controls.Clear();
            modulo.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(modulo);
            panelContenedor.Tag = modulo;
            modulo.BringToFront();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Dashboard());
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Balance());
            LTitulo.Text = "Balance";
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Productos());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirModulo(new UC_Ventas());
        }
    }
}
