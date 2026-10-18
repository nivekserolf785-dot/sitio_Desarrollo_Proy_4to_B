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
    public partial class FrmFractura : Form
    {
        public FrmFractura()
        {
            InitializeComponent();
        }
        string rolRecibido;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        public FrmFractura (string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            ConfigurarControlesPorRol();
        }
        public void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                // Aquí desactiva o oculta lo que el vendedor no deba tocar en este formulario específico
                // btnEliminarCliente.Enabled = false;
                // txtDescuentoEspecial.Enabled = false;
            }
        }

        private void FrmFractura_Load(object sender, EventArgs e)
        {

        }
    }
}
