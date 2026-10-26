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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROYECTO_PA_PARCIAL.Formulario
{
    public partial class FrmProducto : Form
    {
        private string rolRecibido;
        private bool esModificacion = false;
        private TLista_Productos controlador = new TLista_Productos();

        public FrmProducto()
        {
            InitializeComponent();
            cargarComboCategoria();
        }

        public FrmProducto(string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }

        // Carga de Categorías en el ComboBox
        public void cargarComboCategoria()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Aseo personal");
            comboBox1.Items.Add("Limpieza hogar");
            comboBox1.Items.Add("Alimentos");
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        // Datos iniciales de prueba (22 productos)
        public static void CargarDatos()
        {
            if (TLista_Productos.listaProductos.Count == 0)
            {
                // Alimentos (Bolos)
                TLista_Productos.listaProductos.Add(new Producto("P001", "Bolos de sabores 40 gr surtidos", "Alimentos", "Paradise Express", 0.05, 500, new DateTime(2027, 12, 31)));
                TLista_Productos.listaProductos.Add(new Producto("P002", "Bolos de sabores 80 gr surtidos", "Alimentos", "Paradise Express", 0.10, 300, new DateTime(2027, 12, 31)));

                // Limpieza hogar (10 productos)
                TLista_Productos.listaProductos.Add(new Producto("P003", "Detergente en Polvo 1kg", "Limpieza hogar", "Deja", 2.90, 55, new DateTime(2028, 12, 1)));
                TLista_Productos.listaProductos.Add(new Producto("P004", "Cloro Desinfectante 1L", "Limpieza hogar", "Clorox", 1.20, 70, new DateTime(2027, 6, 15)));
                TLista_Productos.listaProductos.Add(new Producto("P005", "Lavaplatos Crema 400g", "Limpieza hogar", "Axion", 1.50, 60, new DateTime(2028, 8, 20)));
                TLista_Productos.listaProductos.Add(new Producto("P006", "Limpiador Pisos 900ml", "Limpieza hogar", "Mr. Músculo", 2.10, 45, new DateTime(2027, 9, 10)));
                TLista_Productos.listaProductos.Add(new Producto("P007", "Suavizante de Ropa 1L", "Limpieza hogar", "Downy", 3.50, 40, new DateTime(2027, 11, 5)));
                TLista_Productos.listaProductos.Add(new Producto("P008", "Esponja Multiuso x3", "Limpieza hogar", "Scotch-Brite", 1.00, 90, new DateTime(2030, 12, 31)));
                TLista_Productos.listaProductos.Add(new Producto("P009", "Desinfectante Aerosol 400ml", "Limpieza hogar", "Lysol", 4.80, 25, new DateTime(2028, 2, 14)));
                TLista_Productos.listaProductos.Add(new Producto("P010", "Limpiador de Vidrios 500ml", "Limpieza hogar", "Cif", 2.30, 35, new DateTime(2028, 3, 22)));
                TLista_Productos.listaProductos.Add(new Producto("P011", "Bolsas de Basura Medianas x30", "Limpieza hogar", "Glad", 2.75, 50, new DateTime(2030, 12, 31)));
                TLista_Productos.listaProductos.Add(new Producto("P012", "Jabón Multiuso en Barra", "Limpieza hogar", "Tarragó", 0.80, 80, new DateTime(2029, 1, 10)));

                // Aseo personal (10 productos)
                TLista_Productos.listaProductos.Add(new Producto("P013", "Jabón Líquido 500ml", "Aseo personal", "Protex", 2.40, 30, new DateTime(2028, 7, 14)));
                TLista_Productos.listaProductos.Add(new Producto("P014", "Shampoo Control Caspa 400ml", "Aseo personal", "H&S", 4.50, 25, new DateTime(2027, 10, 19)));
                TLista_Productos.listaProductos.Add(new Producto("P015", "Pasta Dental 100ml", "Aseo personal", "Colgate", 1.80, 65, new DateTime(2028, 5, 12)));
                TLista_Productos.listaProductos.Add(new Producto("P016", "Desodorante Roll-On 50ml", "Aseo personal", "Rexona", 2.60, 50, new DateTime(2027, 9, 30)));
                TLista_Productos.listaProductos.Add(new Producto("P017", "Papel Higiénico x4 Rollos", "Aseo personal", "Elite", 2.20, 85, new DateTime(2030, 12, 31)));
                TLista_Productos.listaProductos.Add(new Producto("P018", "Crema Corporal Hidratante 400ml", "Aseo personal", "Nivea", 5.20, 20, new DateTime(2028, 4, 18)));
                TLista_Productos.listaProductos.Add(new Producto("P019", "Afeitadoras Descartables x2", "Aseo personal", "Gillette", 1.50, 70, new DateTime(2030, 12, 31)));
                TLista_Productos.listaProductos.Add(new Producto("P020", "Enjuague Bucal 250ml", "Aseo personal", "Listerine", 3.80, 30, new DateTime(2027, 11, 25)));
                TLista_Productos.listaProductos.Add(new Producto("P021", "Jabón de Tocador en Barra", "Aseo personal", "Heno de Pravia", 0.90, 90, new DateTime(2028, 8, 15)));
                TLista_Productos.listaProductos.Add(new Producto("P022", "Bloqueador Solar FPS 50", "Aseo personal", "Banana Boat", 8.50, 15, new DateTime(2028, 3, 1)));
            }
        }

        // Mostrar lista en el DataGridView
        public void Listar()
        {
            dataGridView1.Columns.Clear(); // Limpia columnas fijas del diseñador
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TLista_Productos.listaProductos.ToList();
        }

        // Validación de datos
        public bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Código
                string.IsNullOrWhiteSpace(textBox2.Text) || // Nombre
                string.IsNullOrWhiteSpace(textBox3.Text) || // Marca
                string.IsNullOrWhiteSpace(textBox4.Text) || // Precio
                string.IsNullOrWhiteSpace(textBox5.Text))   // Stock
            {
                MessageBox.Show("Por favor complete los campos obligatorios principales.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!double.TryParse(textBox4.Text.Trim(), out _) || !double.TryParse(textBox5.Text.Trim(), out _))
            {
                MessageBox.Show("El Precio y el Stock deben ser valores numéricos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Crear Objeto
        public Producto CrearObjeto()
        {
            if (!ValidarDatos()) return null;

            string codigo = textBox1.Text.Trim();
            string nombre = textBox2.Text.Trim();
            string categoria = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
            string marca = textBox3.Text.Trim();
            double precio = double.TryParse(textBox4.Text.Trim(), out double p) ? p : 0.0;
            double stock = double.TryParse(textBox5.Text.Trim(), out double s) ? s : 0.0;
            DateTime fechaCaducidad = dateTimePicker1.Value;

            return new Producto(codigo, nombre, categoria, marca, precio, stock, fechaCaducidad);
        }
        public void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                button2.Enabled = false; // Modificar deshabilitado
                button3.Enabled = false; // Eliminar deshabilitado
            }
            else
            {
                // Si es Gerente (o cualquier otro rol administrativo), habilitados de entrada
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        // Al seleccionar un producto para editar
        public void setDatosProducto(Producto prod)
        {
            if (prod != null)
            {
                esModificacion = true;
                textBox1.Text = prod.Codigo;
                textBox1.Enabled = false;

                textBox2.Text = prod.Nombre;
                comboBox1.SelectedItem = prod.Categoria;
                textBox3.Text = prod.Marca;
                textBox4.Text = prod.Precio.ToString();
                textBox5.Text = prod.Stock.ToString();
                dateTimePicker1.Value = prod.FechaCaducidad;

                // Validamos permisos según el rol
                if (rolRecibido == "ROL VENDEDOR")
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;  // El Gerente SÍ puede modificar al seleccionar un registro
                    button3.Enabled = true;  // El Gerente SÍ puede eliminar al seleccionar un registro
                }
            }
        }
        // Limpiar controles
        public void Limpiar()
        {
            esModificacion = false;

            // Genera el código automático desde el controlador y bloquea la caja de texto
            textBox1.Text = controlador.GenerarCodigoAutomatico();
            textBox1.Enabled = false;

            textBox2.Clear(); // Nombre
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0; // Categoria
            textBox3.Clear(); // Marca
            textBox4.Clear(); // Precio
            textBox5.Clear(); // Stock
            dateTimePicker1.Value = DateTime.Now; // Fecha Caducidad
            if (textBox6 != null) textBox6.Clear(); // Buscador

           
            textBox2.Focus(); // Pone el foco directamente en el Nombre ya que el código es automático
        }

        // Guardar (Insertar o Modificar)
        public void Guardar()
        {
            try
            {
                Producto obj = CrearObjeto();
                if (obj != null)
                {
                    if (!esModificacion)
                    {
                        controlador.Insertar(obj); // Utiliza el método Insertar de TLista_Productos
                        MessageBox.Show("Producto ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        controlador.Modificar(obj, obj.Codigo);
                        MessageBox.Show("Producto modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Listar();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Modificar
        public void Modificar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!esModificacion)
                    {
                        int index = dataGridView1.CurrentRow.Index;
                        Producto prod = controlador.GetProducto(index);
                        setDatosProducto(prod);
                        MessageBox.Show("Datos cargados. Edite los campos y presione Guardar (o Modificar) para confirmar.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Guardar();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un producto de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar
        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    Producto prodSeleccionado = controlador.GetProducto(index);

                    if (prodSeleccionado != null)
                    {
                        DialogResult respuesta = MessageBox.Show(
                            $"¿Está seguro de eliminar el producto {prodSeleccionado.Nombre} ({prodSeleccionado.Codigo})?",
                            "Confirmar Eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (respuesta == DialogResult.Yes)
                        {
                            controlador.Eliminar(prodSeleccionado.Codigo);
                            MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Listar();
                            Limpiar();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un producto de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Buscar
        public void Buscar()
        {
            try
            {
                string textoBuscar = textBox6.Text.Trim();

                if (string.IsNullOrEmpty(textoBuscar))
                {
                    Listar();
                    return;
                }

                List<Producto> listaFiltrada = controlador.BuscarProductos(textoBuscar);

                if (listaFiltrada.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listaFiltrada;
                }
                else
                {
                    MessageBox.Show("No se encontraron productos que coincidan con: " + textoBuscar, "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            cargarComboCategoria();
            CargarDatos();
            Listar();
            Limpiar();
            ConfigurarControlesPorRol();
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Producto prod = controlador.GetProducto(e.RowIndex);
                setDatosProducto(prod);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}