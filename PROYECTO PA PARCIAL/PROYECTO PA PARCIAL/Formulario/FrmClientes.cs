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
        public FrmClientes()
        {
            InitializeComponent();
            cargarComboSexo();
        }
        private string rolRecibido;
        private bool esModificacion = false;
        private TLista_Cliente controlador = new TLista_Cliente();
        public FrmClientes(string rol)// recibe el rol del usuario que inició sesión
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }
        // Carga del ComboBox de Sexo ("M" y "F")
        public void cargarComboSexo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("M");
            comboBox1.Items.Add("F");
            comboBox1.SelectedIndex = 0;
        }

        // Datos iniciales de prueba
        public void CargarDatos()
        {
            if (TLista_Cliente.listaClientes.Count == 0)
            {
                TLista_Cliente.listaClientes.Add(new Cliente("0701234567", "Carlos", "Mendoza", 'M', "0987654321", "Av. Central 123", "Machala", "carlos@email.com"));
                TLista_Cliente.listaClientes.Add(new Cliente("0709876543", "Maria", "Guerrero", 'F', "0991122334", "Calle Bolivar 456", "Pasaje", "maria@email.com"));
            }
        }

        // Mostrar lista en el DataGridView
        public void Listar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TLista_Cliente.listaClientes.ToList();
        }

        // Limpiar cajas de texto
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

        // 2. Método CrearObjeto()
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

        // 3. Método setDatosCliente(Cliente cli)
        public void setDatosCliente(Cliente cli)
        {
            if (cli != null)
            {
                esModificacion = true;
                textBox1.Text = cli.Cedula;
                textBox1.Enabled = false; // Bloquea la cédula al editar

                textBox2.Text = cli.Nombre;
                textBox3.Text = cli.Apellido;
                comboBox1.SelectedItem = cli.Sexo.ToString();
                textBox4.Text = cli.Celular;
                textBox5.Text = cli.Direccion;
                textBox6.Text = cli.Ciudad;
                textBox7.Text = cli.Correo;
            }
        }

        // 4. Método Limpiar()
        public void Limpiar()
        {
            esModificacion = false;
            textBox1.Enabled = true; // Rehabilita el campo cédula para nuevos registros

            textBox1.Clear(); // Cédula
            textBox2.Clear(); // Nombre
            textBox3.Clear(); // Apellido
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0; // Sexo
            textBox4.Clear(); // Celular
            textBox5.Clear(); // Dirección
            textBox6.Clear(); // Ciudad
            textBox7.Clear(); // Email
            textBox8.Clear(); // Buscador
            textBox1.Focus();
        }

        // 5. Método Guardar()
        public void Guardar()
        {
            try
            {
                Cliente obj = CrearObjeto();
                if (obj != null)
                {
                    if (!esModificacion)
                    {
                        controlador.Insertar(obj);
                        MessageBox.Show("Cliente ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        controlador.Modificar(obj, obj.Cedula);
                        MessageBox.Show("Cliente modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // 6. Método Modificar() adaptado para trabajar en el mismo formulario
        public void Modificar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    // Si aún no se han cargado los datos en los TextBox, el primer clic los carga para editar
                    if (!esModificacion)
                    {
                        int index = dataGridView1.CurrentRow.Index;
                        Cliente cli = controlador.GetCliente(index);
                        setDatosCliente(cli);
                        MessageBox.Show("Datos cargados. Edite los campos y vuelva a presionar Modificar (o Guardar) para confirmar.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Si ya estaba en modo edición, crea el objeto actualizado y guarda los cambios
                        Cliente objActualizado = CrearObjeto();
                        if (objActualizado != null)
                        {
                            controlador.Modificar(objActualizado, objActualizado.Cedula);
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

        // Método Eliminar()
        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    Cliente clienteSeleccionado = controlador.GetCliente(index);

                    DialogResult respuesta = MessageBox.Show(
                        $"¿Está seguro de eliminar al cliente {clienteSeleccionado.Nombre} {clienteSeleccionado.Apellido}?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.Yes)
                    {
                        controlador.Eliminar(clienteSeleccionado.Cedula);
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

        // Método Buscar()
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

                List<Cliente> listaFiltrada = controlador.BuscarClientes(textoBuscar);

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
                button3.Enabled = false; // Deshabilitar el botón de eliminar cliente para el vendedor
            }
        }


        private void FrmClientes_Load(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
