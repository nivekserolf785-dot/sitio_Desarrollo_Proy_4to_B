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
    public partial class FrmClientes : Form
    {
        private string rolRecibido;
        private bool esModificacion = false;

        public FrmClientes()
        {
            InitializeComponent();
            cargarComboSexo();
           
        }

        public FrmClientes(string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
            cargarComboSexo();
        }

       
        public void cargarComboSexo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("M");
            comboBox1.Items.Add("F");
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }
        public void cargarDatos()
        {
            TLista_Cliente.Leer();
        }
        
       
        public void Listar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TLista_Cliente.listaClientes.ToList();
        }

        public bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Cédula
                string.IsNullOrWhiteSpace(textBox2.Text) || // Nombre
                string.IsNullOrWhiteSpace(textBox3.Text))   // Apellido
            {
                MessageBox.Show("Por favor complete los campos obligatorios principales (Cédula, Nombre y Apellido).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public Cliente CrearObjeto()
        {
            if (!ValidarDatos()) return null;

            string cedula = textBox1.Text.Trim();
            string nombre = textBox2.Text.Trim();
            string apellido = textBox3.Text.Trim();
            char sexo = comboBox1.SelectedItem != null ? Convert.ToChar(comboBox1.SelectedItem.ToString()) : 'M';
            string celular = textBox4.Text.Trim();
            string direccion = textBox5.Text.Trim();
            string ciudad = textBox6.Text.Trim();
            string correo = textBox7.Text.Trim();

            return new Cliente(cedula, nombre, apellido, sexo, celular, direccion, ciudad, correo);
        }

        public void setDatosCliente(Cliente cli)
        {
            if (cli != null)
            {
                esModificacion = true;
                textBox1.Text = cli.Cedula;
                textBox1.Enabled = false; 

                textBox2.Text = cli.Nombre;
                textBox3.Text = cli.Apellido;
                comboBox1.SelectedItem = cli.Sexo.ToString();
                textBox4.Text = cli.Celular;
                textBox5.Text = cli.Direccion;
                textBox6.Text = cli.Ciudad;
                textBox7.Text = cli.Correo;
            }
        }

        public void Limpiar()
        {
            esModificacion = false;
            textBox1.Enabled = true;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            if (textBox8 != null) textBox8.Clear();
            textBox1.Focus();
        }

        public void Guardar()
        {
            try
            {
                Cliente obj = CrearObjeto();
                if (obj != null)
                {
                    TLista_Cliente controladorInstancia = new TLista_Cliente();

                    if (!esModificacion)
                    {
                        controladorInstancia.Insertar(obj);
                        MessageBox.Show("Cliente ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        controladorInstancia.Modificar(obj, obj.Cedula);
                        MessageBox.Show("Cliente modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                   
                    TLista_Cliente.Guardar();

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
                        TLista_Cliente controladorInstancia = new TLista_Cliente();
                        Cliente cli = controladorInstancia.GetCliente(index);
                        setDatosCliente(cli);
                        MessageBox.Show("Datos cargados. Edite los campos y vuelva a presionar Modificar para confirmar.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cliente objActualizado = CrearObjeto();
                        if (objActualizado != null)
                        {
                            TLista_Cliente controladorInstancia = new TLista_Cliente();
                            controladorInstancia.Modificar(objActualizado, objActualizado.Cedula);
                            TLista_Cliente.Guardar();
                            MessageBox.Show("Cliente modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    TLista_Cliente controladorInstancia = new TLista_Cliente();
                    Cliente clienteSeleccionado = controladorInstancia.GetCliente(index);

                    DialogResult respuesta = MessageBox.Show(
                        $"¿Está seguro de eliminar al cliente {clienteSeleccionado.Nombre} {clienteSeleccionado.Apellido}?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.Yes)
                    {
                        controladorInstancia.Eliminar(clienteSeleccionado.Cedula);
                        TLista_Cliente.Guardar();
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
                string textoBuscar = textBox8.Text.Trim();

                if (string.IsNullOrEmpty(textoBuscar))
                {
                    Listar();
                    return;
                }

                TLista_Cliente controladorInstancia = new TLista_Cliente();
                List<Cliente> listaFiltrada = controladorInstancia.BuscarClientes(textoBuscar);

                if (listaFiltrada.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listaFiltrada;
                }
                else
                {
                    MessageBox.Show("No se encontraron clientes que coincidan con: " + textoBuscar, "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                button3.Enabled = false; 
            }
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            cargarDatos();
            Listar();
            cargarComboSexo();
        }

        private void button4_Click(object sender, EventArgs e) => this.Close();
        private void button1_Click(object sender, EventArgs e) => Guardar();
        private void button2_Click(object sender, EventArgs e) => Modificar();
        private void button3_Click(object sender, EventArgs e) => Eliminar();
        private void button5_Click(object sender, EventArgs e) => Buscar();
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}