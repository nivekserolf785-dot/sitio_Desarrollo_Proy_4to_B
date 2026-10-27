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

namespace PROYECTO_PA_PARCIAL.Formulario
{
    public partial class FrmVendedor : Form
    {
        public FrmVendedor()
        {
            InitializeComponent();
        }
       
        private string rolRecibido;
        private bool esModificacion = false;
        public  FrmVendedor (string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }
       
        public void CargarDatos()
        {
            // 1. Intentamos leer los datos directamente desde el archivo CSV en la carpeta Data
            TLista_Vendedor.Leer();

            // 2. Si el archivo está vacío, inyectamos registros de prueba
            if (TLista_Vendedor.listaVendedores.Count == 0)
            {
                TLista_Vendedor.listaVendedores.Add(new Vendedor("0701111111", "Carlos", "Mendoza", "0987654321", "Machala"));
                TLista_Vendedor.listaVendedores.Add(new Vendedor("0702222222", "Maria", "Guerrero", "0991122334", "Pasaje"));

                // 3. Guardamos automáticamente en el archivo vendedores.csv
                TLista_Vendedor.Guardar();
            }
        }

        // Mostrar lista en el DataGridView
        public void Listar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TLista_Vendedor.listaVendedores.ToList();
        }

        public bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Cédula (Cedulav)
                string.IsNullOrWhiteSpace(textBox2.Text) || // Nombre (Nombrev)
                string.IsNullOrWhiteSpace(textBox3.Text))   // Apellido (Apellidov)
            {
                MessageBox.Show("Por favor complete los campos obligatorios principales (Cédula, Nombre y Apellido).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public Vendedor CrearObjeto()
        {
            if (!ValidarDatos()) return null;

            string cedv = textBox1.Text.Trim();
            string nomv = textBox2.Text.Trim();
            string apev = textBox3.Text.Trim();
            string celv = textBox4.Text.Trim();
            string ciuv = textBox5.Text.Trim();

            return new Vendedor(cedv, nomv, apev, celv, ciuv);
        }

        public void setDatosVendedor(Vendedor v)
        {
            if (v != null)
            {
                esModificacion = true;
                textBox1.Text = v.Cedulav;
                textBox1.Enabled = false; // Bloquea la cédula al editar

                textBox2.Text = v.Nombrev;
                textBox3.Text = v.Apellidov;
                textBox4.Text = v.Celularv;
                textBox5.Text = v.Ciudadv;
            }
        }

        public void Limpiar()
        {
            esModificacion = false;
            textBox1.Enabled = true;

            textBox1.Clear(); // Cédula
            textBox2.Clear(); // Nombre
            textBox3.Clear(); // Apellido
            textBox4.Clear(); // Celular
            textBox5.Clear(); // Ciudad
            if (textBox6 != null) textBox6.Clear(); // Buscador (si usa textBox6 para buscar)
            textBox1.Focus();
        }

        public void Guardar()
        {
            try
            {
                Vendedor obj = CrearObjeto();
                if (obj != null)
                {
                    TLista_Vendedor controladorInstancia = new TLista_Vendedor();

                    if (!esModificacion)
                    {
                        controladorInstancia.Insertar(obj);
                        MessageBox.Show("Vendedor ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        controladorInstancia.Modificar(obj, obj.Cedulav);
                        MessageBox.Show("Vendedor modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Guardar en el archivo CSV de forma estática
                    TLista_Vendedor.Guardar();

                    Listar();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Modificar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!esModificacion)
                    {
                        int index = dataGridView1.CurrentRow.Index;
                        TLista_Vendedor controladorInstancia = new TLista_Vendedor();
                        Vendedor v = controladorInstancia.GetVendedor(index);
                        setDatosVendedor(v);
                        MessageBox.Show("Datos cargados. Edite los campos y vuelva a presionar Modificar para confirmar.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Vendedor objActualizado = CrearObjeto();
                        if (objActualizado != null)
                        {
                            TLista_Vendedor controladorInstancia = new TLista_Vendedor();
                            controladorInstancia.Modificar(objActualizado, objActualizado.Cedulav);
                            TLista_Vendedor.Guardar();
                            MessageBox.Show("Vendedor modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Listar();
                            Limpiar();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un registro de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    TLista_Vendedor controladorInstancia = new TLista_Vendedor();
                    Vendedor vendedorSeleccionado = controladorInstancia.GetVendedor(index);

                    DialogResult respuesta = MessageBox.Show(
                        $"¿Está seguro de eliminar al vendedor {vendedorSeleccionado.Nombrev} {vendedorSeleccionado.Apellidov}?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.Yes)
                    {
                        controladorInstancia.Eliminar(vendedorSeleccionado.Cedulav);
                        TLista_Vendedor.Guardar();
                        MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listar();
                        Limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un registro de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Buscar()
        {
            try
            {
                // Cambie textBox8 por el número de su caja de texto de búsqueda en el diseño (ej. textBox6)
                string textoBuscar = textBox6.Text.Trim();

                if (string.IsNullOrEmpty(textoBuscar))
                {
                    Listar();
                    return;
                }

                TLista_Vendedor controladorInstancia = new TLista_Vendedor();
                List<Vendedor> listaFiltrada = controladorInstancia.BuscarVendedores(textoBuscar);

                if (listaFiltrada.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listaFiltrada;
                }
                else
                {
                    MessageBox.Show("No se encontraron vendedores que coincidan con: " + textoBuscar, "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar vendedor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void cargarComboSexo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("M");
            comboBox1.Items.Add("F");
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }
        private void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
               
            {
                button2.Enabled = false;
                button3.Enabled = false; // Deshabilitar el botón de eliminar vendedor para el rol vendedor
            }
        }
        private void FrmVendedor_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Listar();
            cargarComboSexo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar();
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
    }
}
