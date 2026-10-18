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
    public partial class Frm_Inicio : Form
    {
        public Frm_Inicio()
        {
            InitializeComponent();
        }
        private string rolUsuario;
        public Frm_Inicio(string rol)
        {
            InitializeComponent();
            this.rolUsuario = rol;// si no se pone el this no se puede acceder a la variable rolUsuario en otros métodos de la clase
        }
        private void FrmGerente_Load(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FrmClientes frmClientes = new FrmClientes(rolUsuario);
            frmClientes.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmVendedor frmVendedor = new FrmVendedor(rolUsuario);
            frmVendedor.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto(rolUsuario);
            frmProducto.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmFractura frmFactura = new FrmFractura(rolUsuario);
            frmFactura.Visible = true;
        }
    }
}
