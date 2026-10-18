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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            cargarComboBox();
            inicioSesion();
        }
        public void cargarComboBox()
        {
            comboBox1.Items.Add("ROL USUARIO");
            comboBox1.Items.Add("ROL GERENTE");
            comboBox1.Items.Add("ROL VENDEDOR");
            comboBox1.SelectedIndex = 0; 
        }
        public void inicioSesion()
        {
            if (comboBox1.SelectedItem == "ROL USUARIO")
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
            }
           
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inicioSesion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("ROL GERENTE") && textBox1.Text == "Emperador1996#")
            {
                Frm_Inicio frm = new Frm_Inicio(comboBox1.SelectedItem.ToString());
                frm.Visible = true;
                this.Hide();
            }
            else if (comboBox1.SelectedItem.Equals("ROL VENDEDOR") && textBox1.Text == "Vendor555")
            {
                Frm_Inicio frm = new Frm_Inicio(comboBox1.SelectedItem.ToString());
                frm.Visible = true;
                this.Hide();
            }
            else if (comboBox1.SelectedItem.Equals("ROL USUARIO"))
            {
                MessageBox.Show("No se requiere clave para el rol de usuario.");
            }
            else
            {
                MessageBox.Show("Clave incorrecta o rol no válido. Intente nuevamente.");
            }

        }
    }
}
