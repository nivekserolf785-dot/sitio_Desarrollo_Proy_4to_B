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
        public void LimpiarCampos()
        {
            textBox1.Clear(); // Cédula
            textBox2.Clear(); // Nombre
            textBox3.Clear(); // Apellido
            comboBox1.SelectedIndex = 0; // Sexo
            textBox4.Clear(); // Celular
            textBox5.Clear(); // Dirección
            textBox6.Clear(); // Ciudad
            textBox7.Clear(); // Email
            textBox8.Clear(); // Buscador
            textBox1.Focus();
        }

        // Método Nuevo (Insertar)
        public void Nuevo()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Por favor, ingrese al menos Cédula y Nombre.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                char sexo = comboBox1.SelectedItem != null ? Convert.ToChar(comboBox1.SelectedItem.ToString()) : 'M';

                Cliente obj = new Cliente(
                    textBox1.Text.Trim(),
                    textBox2.Text.Trim(),
                    textBox3.Text.Trim(),
                    sexo,
                    textBox4.Text.Trim(),
                    textBox5.Text.Trim(),
                    textBox6.Text.Trim(),
                    textBox7.Text.Trim()
                );

                // Validar si ya existe por cédula
                int pos = -1;
                for (int i = 0; i < TLista_Cliente.listaClientes.Count; i++)
                {
                    if (TLista_Cliente.listaClientes[i].Cedula.Equals(obj.Cedula, StringComparison.OrdinalIgnoreCase))
                    {
                        pos = i;
                        break;
                    }
                }

                if (pos == -1)
                {
                    TLista_Cliente.listaClientes.Add(obj);
                    MessageBox.Show("Cliente ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ya existe un cliente registrado con esa cédula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Insertar Cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método Modificar
        public void Modificar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int posicion = dataGridView1.CurrentRow.Index;
                    char sexo = comboBox1.SelectedItem != null ? Convert.ToChar(comboBox1.SelectedItem.ToString()) : 'M';

                    Cliente objModificado = new Cliente(
                        textBox1.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox3.Text.Trim(),
                        sexo,
                        textBox4.Text.Trim(),
                        textBox5.Text.Trim(),
                        textBox6.Text.Trim(),
                        textBox7.Text.Trim()
                    );

                    TLista_Cliente.listaClientes[posicion] = objModificado;
                    MessageBox.Show("Cliente modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un registro de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Modificar Cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método Eliminar
        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int posicion = dataGridView1.CurrentRow.Index;
                    Cliente clienteSeleccionado = TLista_Cliente.listaClientes[posicion];

                    DialogResult respuesta = MessageBox.Show(
                        $"¿Está seguro de eliminar al cliente {clienteSeleccionado.Nombre} {clienteSeleccionado.Apellido}?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.Yes)
                    {
                        TLista_Cliente.listaClientes.RemoveAt(posicion);
                        MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listar();
                        LimpiarCampos();
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
        private void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                button3.Enabled = false; // Deshabilitar el boton de eliminar cliente para el vendedor
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
            Nuevo();
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
    }
}
