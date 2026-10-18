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
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();
        }
        string rolRecibido;// variable que recibe el rol del usuario que inició sesión
        public FrmProducto(string rol)// constructor que recibe el rol del usuario que inició sesión
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }
      public void  ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                // Aquí desactiva o oculta lo que el vendedor no deba tocar en este formulario específico
                // btnEliminarCliente.Enabled = false;
                // txtDescuentoEspecial.Enabled = false;
            }
        }
        private void FrmProducto_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
