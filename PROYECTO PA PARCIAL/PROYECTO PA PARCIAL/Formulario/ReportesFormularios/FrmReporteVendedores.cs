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
    public partial class FrmReporteVendedores : Form
    {
        public FrmReporteVendedores()
        {
            InitializeComponent();
        }
        private void CargarDashboardVendedores()
        {
            try
            {
                // 1. Leemos los datos actualizados del CSV de vendedores
                TLista_Vendedor.Leer();
                var lista = TLista_Vendedor.listaVendedores; // Asegúrese de que su lista estática se llame así en su controlador

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay vendedores registrados para mostrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Cargamos la tabla por defecto
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lista;
                EstilizarTabla();

                // 3. Configuramos el gráfico estadístico por defecto (chart1) para vendedores
                ConfigurarGraficoVendedores(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte de vendedores: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGraficoVendedores(System.Collections.Generic.List<Vendedor> lista)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Área del gráfico
            ChartArea areaGrafico = new ChartArea("AreaPrincipal");
            areaGrafico.BackColor = Color.White;
            areaGrafico.AxisX.MajorGrid.LineColor = Color.LightGray;
            areaGrafico.AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas.Add(areaGrafico);

            // Serie de datos (Gráfico de barras)
            Series serie = new Series("Vendedores");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;

            // Usamos 'ciudadv' tal como está definido en su entidad Vendedor
            var vendedoresPorCiudad = lista.GroupBy(v => v.Ciudadv)
                                .Select(g => new { Ciudad = g.Key, Cantidad = g.Count() });
            foreach (var item in vendedoresPorCiudad)
            {
                serie.Points.AddXY(string.IsNullOrEmpty(item.Ciudad) ? "General" : item.Ciudad, item.Cantidad);
            }

            serie.Color = Color.FromArgb(230, 126, 34); // Naranja corporativo profesional
            chart1.Series.Add(serie);

            // Título del gráfico
            chart1.Titles.Clear();
            chart1.Titles.Add("Distribución del Personal de Ventas por Ciudad - Paradise Express");
            chart1.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chart1.Titles[0].ForeColor = Color.FromArgb(31, 58, 82);
        }

        private void EstilizarTabla()
        {
            // Estilo visual limpio para dataGridView1
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 126, 34); // Selección naranja a juego
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
        private void FrmReporteVendedores_Load(object sender, EventArgs e)
        {
            CargarDashboardVendedores();
        }
    }
}
