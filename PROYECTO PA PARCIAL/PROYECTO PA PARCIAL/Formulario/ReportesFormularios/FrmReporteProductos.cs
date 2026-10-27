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
    public partial class FrmReporteProductos : Form
    {
        public FrmReporteProductos()
        {
            InitializeComponent();
        }
        private void CargarDashboardProductos()
        {
            try
            {
                // 1. Leemos los datos actualizados del CSV de productos
                TLista_Productos.Leer();
                var lista = TLista_Productos.listaProductos; // Asumiendo que su lista estática se llama así

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay productos registrados para mostrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Cargamos la tabla por defecto
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lista;
                EstilizarTabla();

                // 3. Configuramos el gráfico estadístico por defecto (chart1) para productos
                ConfigurarGraficoProductos(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte de productos: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGraficoProductos(System.Collections.Generic.List<Producto> lista)
        {
            // Usamos chart1 que es el nombre por defecto del componente
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Área del gráfico
            ChartArea areaGrafico = new ChartArea("AreaPrincipal");
            areaGrafico.BackColor = Color.White;
            areaGrafico.AxisX.MajorGrid.LineColor = Color.LightGray;
            areaGrafico.AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas.Add(areaGrafico);

            // Serie de datos (Gráfico de barras)
            Series serie = new Series("Productos");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;

            // Agrupamos los productos por categoría (o ajusté la propiedad según su entidad Producto, ej: Categoria o Stock)
            var productosPorCategoria = lista.GroupBy(p => p.Categoria) // Cambie 'Categoria' si su atributo se llama distinto en la Entidad Producto
                                             .Select(g => new { Categoria = g.Key, Cantidad = g.Count() });

            foreach (var item in productosPorCategoria)
            {
                serie.Points.AddXY(string.IsNullOrEmpty(item.Categoria) ? "General" : item.Categoria, item.Cantidad);
            }

            serie.Color = Color.FromArgb(39, 174, 96); // Verde profesional distintivo para productos/inventario
            chart1.Series.Add(serie);

            // Título del gráfico
            chart1.Titles.Clear();
            chart1.Titles.Add("Inventario de Productos por Categoría - Paradise Express");
            chart1.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chart1.Titles[0].ForeColor = Color.FromArgb(31, 58, 82);
        }

        private void EstilizarTabla()
        {
            // Estilo visual limpio para dataGridView1
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(39, 174, 96); // Selección verde a juego
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
        private void FrmReporteProductos_Load(object sender, EventArgs e)
        {
            CargarDashboardProductos();
        }
    }
}
