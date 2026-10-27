namespace PROYECTO_PA_PARCIAL.Formulario
{
    partial class Frm_Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Inicio));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteVendedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.reporteFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.clienteToolStripMenuItem,
            this.toolStripMenuItem5});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportesClientesToolStripMenuItem,
            this.reporteProductosToolStripMenuItem,
            this.reporteVendedoresToolStripMenuItem,
            this.reporteFacturasToolStripMenuItem});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            resources.ApplyResources(this.clienteToolStripMenuItem, "clienteToolStripMenuItem");
            this.clienteToolStripMenuItem.Click += new System.EventHandler(this.clienteToolStripMenuItem_Click);
            // 
            // reportesClientesToolStripMenuItem
            // 
            this.reportesClientesToolStripMenuItem.Name = "reportesClientesToolStripMenuItem";
            resources.ApplyResources(this.reportesClientesToolStripMenuItem, "reportesClientesToolStripMenuItem");
            this.reportesClientesToolStripMenuItem.Click += new System.EventHandler(this.reportesClientesToolStripMenuItem_Click);
            // 
            // reporteProductosToolStripMenuItem
            // 
            this.reporteProductosToolStripMenuItem.Name = "reporteProductosToolStripMenuItem";
            resources.ApplyResources(this.reporteProductosToolStripMenuItem, "reporteProductosToolStripMenuItem");
            this.reporteProductosToolStripMenuItem.Click += new System.EventHandler(this.reporteProductosToolStripMenuItem_Click);
            // 
            // reporteVendedoresToolStripMenuItem
            // 
            this.reporteVendedoresToolStripMenuItem.Name = "reporteVendedoresToolStripMenuItem";
            resources.ApplyResources(this.reporteVendedoresToolStripMenuItem, "reporteVendedoresToolStripMenuItem");
            this.reporteVendedoresToolStripMenuItem.Click += new System.EventHandler(this.reporteVendedoresToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.panelContenedor, "panelContenedor");
            this.panelContenedor.Name = "panelContenedor";
            // 
            // reporteFacturasToolStripMenuItem
            // 
            this.reporteFacturasToolStripMenuItem.Name = "reporteFacturasToolStripMenuItem";
            resources.ApplyResources(this.reporteFacturasToolStripMenuItem, "reporteFacturasToolStripMenuItem");
            this.reporteFacturasToolStripMenuItem.Click += new System.EventHandler(this.reporteFacturasToolStripMenuItem_Click);
            // 
            // Frm_Inicio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::PROYECTO_PA_PARCIAL.Properties.Resources.DETALLE;
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Inicio";
            this.Load += new System.EventHandler(this.FrmGerente_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.ToolStripMenuItem reportesClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteVendedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteFacturasToolStripMenuItem;
    }
}