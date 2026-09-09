using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proyecto_Taller_2
{
    public partial class UC_Balance : UserControl
    {
        private List<FacturaMock> _todasLasFacturas;
        public UC_Balance()
        {
            InitializeComponent();
            CargarDatosFicticios();
            ConfigurarControles();
            //AplicarFiltros();
        }

        private void CargarDatosFicticios()
        {
            // Simulamos datos emitidos a lo largo del año 2026
            _todasLasFacturas = new List<FacturaMock>
            {
                new FacturaMock { Numero = "FC-001-01", Fecha = new DateTime(2026, 1, 10), Cliente = "Distribuidora Sur", MetodoPago = "Efectivo", Monto = 250000m },
                new FacturaMock { Numero = "FC-001-02", Fecha = new DateTime(2026, 2, 14), Cliente = "Librería Central", MetodoPago = "Transferencia", Monto = 380000m },
                new FacturaMock { Numero = "FC-001-03", Fecha = new DateTime(2026, 3, 05), Cliente = "Comercial Alfa", MetodoPago = "Tarjeta", Monto = 420000m },
                new FacturaMock { Numero = "FC-001-04", Fecha = new DateTime(2026, 3, 20), Cliente = "Imprenta Omega", MetodoPago = "Efectivo", Monto = 230000m },
                new FacturaMock { Numero = "FC-001-05", Fecha = new DateTime(2026, 4, 12), Cliente = "Estudio Jurídico", MetodoPago = "Transferencia", Monto = 590000m },
                new FacturaMock { Numero = "FC-001-06", Fecha = new DateTime(2026, 5, 18), Cliente = "Distribuidora Sur", MetodoPago = "Tarjeta", Monto = 720000m },
                new FacturaMock { Numero = "FC-001-07", Fecha = new DateTime(2026, 6, 22), Cliente = "Librería Central", MetodoPago = "Transferencia", Monto = 810000m },
                new FacturaMock { Numero = "FC-001-08", Fecha = new DateTime(2026, 7, 11), Cliente = "Comercial Alfa", MetodoPago = "Efectivo", Monto = 950000m }
            };
        }

        private void ConfigurarControles()
        {
            // Llenar combos de meses (1 a 12)
            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            CBMesDesde.DataSource = meses.Clone();
            CBMesHasta.DataSource = meses.Clone();

            CBMesDesde.SelectedIndex = 2; // Marzo (Índice 2)
            CBMesHasta.SelectedIndex = 6; // Julio (Índice 6)

            CBTipoFiltro.Items.AddRange(new string[] { "Por Mes", "Por Rango de Meses" });
            CBTipoFiltro.SelectedIndex = 1;

            // Configuración estética del Chart
            chartEvolucion.Series.Clear();
            Series serie = chartEvolucion.Series.Add("Facturación ($)");
            serie.ChartType = SeriesChartType.Column; // Cambiar a SeriesChartType.Line si prefieres líneas
            serie.Color = Color.FromArgb(13, 110, 253);
            serie.IsValueShownAsLabel = true;
        }

        private void CBMesHasta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAplicarFiltros_Click(object sender, EventArgs e)
        {

        }
    }
}
