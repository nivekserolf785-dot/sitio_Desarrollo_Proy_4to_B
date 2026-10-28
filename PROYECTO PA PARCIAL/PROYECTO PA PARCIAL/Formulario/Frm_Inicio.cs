using PROYECTO_PA_PARCIAL.Formulario.ReportesFormularios;
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
        private void AbrirFormularioEnPanel(Form formHijo)
        {
            // Si ya hay un formulario abierto dentro del panel, lo removemos para liberar memoria
            if (panelContenedor.Controls.Count > 0)
            {
                panelContenedor.Controls[0].Dispose();
                panelContenedor.Controls.Clear();
            }

            // Configuramos el formulario hijo para que se comporte como un control interno
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill; // Hace que se adapte perfectamente al tamaño del panel

            // Lo agregamos al panel contenedor del formulario principal y lo mostramos
            panelContenedor.Controls.Add(formHijo);
            panelContenedor.Tag = formHijo;
            formHijo.Show();
        }
        private void FrmGerente_Load(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
          

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

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmProducto());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmFractura());
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FrmClientes frmCli = new FrmClientes();
            AbrirFormularioEnPanel(frmCli);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmVendedor());
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea bloquear el sistema y volver al inicio de sesión?",
                                             "Bloquear Sistema",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                FrmPrincipal frmPrincipal = new FrmPrincipal();

               
                frmPrincipal.Show();

        
                this.Hide();
            }

        }

        private void reportesClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            FrmReporteClientes frmReporte = new FrmReporteClientes();
            AbrirFormularioEnPanel(frmReporte);
        }

        private void reporteProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporteProductos frmReporte = new FrmReporteProductos();
            AbrirFormularioEnPanel(frmReporte);
        }

        private void reporteVendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporteVendedores frmReporte = new FrmReporteVendedores();
            AbrirFormularioEnPanel(frmReporte);
        }

        private void reporteFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporteFacturas frmReporte = new FrmReporteFacturas();
            AbrirFormularioEnPanel(frmReporte);
        }

        private void stockProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("Stock productos");
            AbrirFormularioEnPanel(frm);
        }

        private void mejorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("Mejor cliente");
            AbrirFormularioEnPanel(frm);
        }

        private void productosCercaAExpirarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("Productos cerca a expirar");
            AbrirFormularioEnPanel(frm);
        }

        private void capitalMasInversionEnProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("Total sum $ productos");
            AbrirFormularioEnPanel(frm);
        }

        private void porcentajeDeComprasPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("porcentaje de compras por cliente");
            AbrirFormularioEnPanel(frm);
        }

        private void porcentajeComprasFemeninoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("porcentaje compras femenino");
            AbrirFormularioEnPanel(frm);
        }

        private void porcentajeComprasMasculinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Consultas_linq frm = new Frm_Consultas_linq("porcentaje compras masculino");
            AbrirFormularioEnPanel(frm);
        }

    } 
}    
