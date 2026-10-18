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
        public  FrmVendedor (string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }
        private void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                // Aquí desactiva o oculta lo que el vendedor no deba tocar en este formulario específico
                // btnEliminarCliente.Enabled = false;
                // txtDescuentoEspecial.Enabled = false;
            }
        }
        private void FrmVendedor_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
