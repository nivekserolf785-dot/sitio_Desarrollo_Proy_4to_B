using PROYECTO_PA_PARCIAL.Controlador;
using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PROYECTO_PA_PARCIAL.Formulario.ReportesFormularios
{
    public partial class FrmReporteFacturas : Form
    {
        public FrmReporteFacturas()
        {
            InitializeComponent();
        }
        private void CargarDashboardFacturas()
        {
            try
            {
               
                TLista_Facturas.Leer(); 
                var lista = TLista_Facturas.listaFacturas; 

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay facturas registradas para mostrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

              
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lista;
                EstilizarTabla();

                ConfigurarGraficoFacturas(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte de facturas: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGraficoFacturas(System.Collections.Generic.List<Factura> lista)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea areaGrafico = new ChartArea("AreaPrincipal");
            areaGrafico.BackColor = Color.White;
            areaGrafico.AxisX.MajorGrid.LineColor = Color.LightGray;
            areaGrafico.AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas.Add(areaGrafico);

            Series serie = new Series("Facturas");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;

           
            var facturasPorFecha = lista.GroupBy(f => f.FechaEmision)
                               .Select(g => new { Fecha = g.Key, Cantidad = g.Count() });

            foreach (var item in facturasPorFecha)
            {
                serie.Points.AddXY(string.IsNullOrEmpty(item.Fecha) ? "General" : item.Fecha, item.Cantidad);
            }

            serie.Color = Color.FromArgb(142, 68, 173); 
            chart1.Series.Add(serie);

            chart1.Titles.Clear();
            chart1.Titles.Add("Emisión de Facturas por Fecha - Paradise Express");
            chart1.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chart1.Titles[0].ForeColor = Color.FromArgb(31, 58, 82);
        }

        private void EstilizarTabla()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(142, 68, 173);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowTemplate.Height = 28;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 58, 82);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 32;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void FrmReporteFacturas_Load(object sender, EventArgs e)
        {
            CargarDashboardFacturas();
        }
    }
}
