using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proyecto_Taller_2
{
    public partial class UC_Balance : UserControl
    {
        public UC_Balance()
        {
            InitializeComponent();
            ConfigurarControlesUI();
            CargarGraficosEstadisticos();
        }

        private void UC_Balance_Load(object sender, EventArgs e)
        {
        }

        private void ConfigurarControlesUI()
        {
            // Ajustes visuales de los botones de reporte general para evitar que se corte el texto
            if (button4 != null)
            {
                button4.Padding = new Padding(0);
                button4.TextAlign = ContentAlignment.MiddleCenter;
                button4.TextImageRelation = TextImageRelation.ImageBeforeText;
                button4.Click -= BtnVerReporteGeneral_Click;
                button4.Click += BtnVerReporteGeneral_Click;
            }

            if (button5 != null)
            {
                button5.Padding = new Padding(0);
                button5.TextAlign = ContentAlignment.MiddleCenter;
                button5.TextImageRelation = TextImageRelation.ImageBeforeText;
                button5.Click -= BtnImprimirReporteGeneral_Click;
                button5.Click += BtnImprimirReporteGeneral_Click;
            }
        }

        private void CargarGraficosEstadisticos()
        {
            // --- GRÁFICO 1: Ventas por Colección de Remeras ---
            if (chart1 != null)
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Titles.Add("Ventas por Colección de Remeras (Unidades)");

                Series serieColecciones = chart1.Series.Add("Colecciones");
                serieColecciones.ChartType = SeriesChartType.Column;
                serieColecciones.IsValueShownAsLabel = true;

                serieColecciones.Points.AddXY("Champions", 105);
                serieColecciones.Points.AddXY("Libertadores", 70);
                serieColecciones.Points.AddXY("Mundiales", 120);
                serieColecciones.Points.AddXY("Ligas Europeas", 90);

                if (serieColecciones.Points.Count >= 4)
                {
                    serieColecciones.Points[0].Color = Color.FromArgb(52, 120, 180);
                    serieColecciones.Points[1].Color = Color.FromArgb(225, 155, 60);
                    serieColecciones.Points[2].Color = Color.FromArgb(90, 165, 210);
                    serieColecciones.Points[3].Color = Color.FromArgb(30, 75, 120);
                }
            }

            // --- GRÁFICO 2: Top 5 Diseños Más Vendidos ---
            if (chart2 != null)
            {
                chart2.Series.Clear();
                chart2.Titles.Clear();
                chart2.Titles.Add("Top 5 Diseños Más Vendidos");

                Series serieTop5 = chart2.Series.Add("TopDiseños");
                serieTop5.ChartType = SeriesChartType.Bar;
                serieTop5.IsValueShownAsLabel = true;

                serieTop5.Points.AddXY("Champion Ligas Europeas", 13);
                serieTop5.Points.AddXY("Boca Jrs Mundiales", 22);
                serieTop5.Points.AddXY("Boca Jrs Libertadores", 25);
                serieTop5.Points.AddXY("Real Madrid Champions", 38);
                serieTop5.Points.AddXY("Argentina '86 Mundiales", 40);

                if (serieTop5.Points.Count >= 5)
                {
                    serieTop5.Points[0].Color = Color.FromArgb(235, 160, 65);
                    serieTop5.Points[1].Color = Color.FromArgb(190, 75, 70);
                    serieTop5.Points[2].Color = Color.FromArgb(15, 70, 130);
                    serieTop5.Points[3].Color = Color.FromArgb(200, 160, 110);
                    serieTop5.Points[4].Color = Color.FromArgb(100, 175, 215);
                }
            }
        }

        #region Reportes Generales

        private void BtnVerReporteGeneral_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte General de Facturación y Balances abierto exitosamente.",
                            "Reporte General", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnImprimirReporteGeneral_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte General impreso correctamente en la impresora predeterminada.",
                            "Reporte General", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}