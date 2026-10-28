using PROYECTO_PA_PARCIAL.Controlador;
using PROYECTO_PA_PARCIAL.Entidades;
using System.Drawing.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROYECTO_PA_PARCIAL.Formulario
{
    public partial class FrmFractura : Form
    {
        private string rolRecibido;
        private TLista_ItemCarrito controlador = new TLista_ItemCarrito();
        private TLista_Facturas controladorFactura = new TLista_Facturas();

      
        private static int contadorFactura = 1;

        public FrmFractura()
        {
            InitializeComponent();
            InicializarDatos();
        }

        public FrmFractura(string rol)
        {
            InitializeComponent();
            this.rolRecibido = rol;
            InicializarDatos();
            ConfigurarControlesPorRol();
        }
        // Carga tradicional de ComboBox mediante bucles directos
        public void CargarCombos()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            // 2. Cargar Nombre Completo de los clientes desde la lista ya leída
            if (TLista_Cliente.listaClientes != null)
            {
                foreach (var cliente in TLista_Cliente.listaClientes)
                {
                    comboBox1.Items.Add(cliente.NombreCompleto);
                }
            }

            // 3. Cargar productos desde la lista ya leída
            if (TLista_Productos.listaProductos != null)
            {
                foreach (var producto in TLista_Productos.listaProductos)
                {
                    comboBox2.Items.Add($"{producto.Nombre} - {producto.Categoria}");
                }
            }

            // 4. Cargar Forma de Pago en comboBox3
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Efectivo");
            comboBox3.Items.Add("Transferencia");
            comboBox3.Items.Add("Tarjeta");
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
        }
        public void ListarFacturas()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = TLista_Facturas.listaFacturas.ToList();
        }
        // Inicialización de datos y campos en orden estricto
        public void InicializarDatos()
        {
            textBox1.Text = DateTime.Now.ToShortDateString();
            textBox2.Text = !string.IsNullOrEmpty(rolRecibido) ? rolRecibido : "Administrador";

            // =========================================================================
            // 1. LEEMOS EL CSV DE FACTURAS PRIMERO (¡Esto faltaba para que aparezcan en dgv2!)
            // =========================================================================
            TLista_Facturas.Leer();

            // =========================================================================
            // 2. CÁLCULO AUTOMÁTICO DEL NÚMERO DE FACTURA SECUENCIAL DESDE EL CSV
            // =========================================================================
            int siguienteNumero = 1;
            if (TLista_Facturas.listaFacturas != null && TLista_Facturas.listaFacturas.Count > 0)
            {
                // Intenta obtener el número de la última factura registrada y sumarle 1
                var ultimaFactura = TLista_Facturas.listaFacturas.LastOrDefault();
                if (ultimaFactura != null && int.TryParse(ultimaFactura.NumFactura, out int numUltimo))
                {
                    siguienteNumero = numUltimo + 1;
                }
                else
                {
                    siguienteNumero = TLista_Facturas.listaFacturas.Count + 1;
                }
            }

            textBox3.Text = siguienteNumero.ToString("D6"); // Formato 000001, 000002...
            textBox3.Enabled = false;
            // =========================================================================

            LimpiarCamposProducto();
            textBox8.Text = "0.00";
            textBox9.Text = "0.00";
            textBox10.Text = "0.00";

            TLista_ItemCarrito.listaCarrito.Clear();
            Listar();
            ListarFacturas(); // ¡Aquí ya se pintarán todas las facturas guardadas en el dataGridView2!
        }

        // Mostrar lista en el DataGridView superior (carrito)
        public void Listar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TLista_ItemCarrito.listaCarrito.ToList();
        }
        // Mostrar lista en el DataGridView superior (carrito)


        // Autocompletar código y precio al seleccionar un producto en el comboBox2
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Recalcular subtotal del ítem cuando cambia la cantidad en textBox6
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            CalcularSubtotalItem();
        }

        private void CalcularSubtotalItem()
        {
            if (double.TryParse(textBox5.Text, out double precio) &&
                int.TryParse(textBox6.Text, out int cantidad))
            {
                textBox7.Text = (precio * cantidad).ToString("0.00");
            }
            else
            {
                textBox7.Text = "0.00";
            }
        }


        // --- MÉTODOS DE ACCIÓN PRINCIPALES ---

        // Button 1: Insertar al carrito (dataGridView1)
        public void AgregarItem()
        {
            int index = comboBox2.SelectedIndex;
            if (index < 0 || index >= TLista_Productos.listaProductos.Count)
            {
                MessageBox.Show("Por favor seleccione un producto válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox6.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto prod = TLista_Productos.listaProductos[index];

            if (cantidad > prod.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Disponible: {prod.Stock}", "Stock Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existente = TLista_ItemCarrito.listaCarrito.FirstOrDefault(x => x.Codigo == prod.Codigo);
            if (existente != null)
            {
                if ((existente.Cantidad + cantidad) > prod.Stock)
                {
                    MessageBox.Show("La cantidad total excede el stock disponible.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existente.Cantidad += cantidad;
            }
            else
            {
                ItemCarrito nuevo = new ItemCarrito(prod.Codigo, prod.Nombre, prod.Precio, cantidad);
                TLista_ItemCarrito.listaCarrito.Add(nuevo);
            }

            Listar();
            ActualizarTotalesGenerales();
            LimpiarCamposProducto();
        }

        // Button 2: Modificar detalle del carrito usando el textBox6 (Cantidad)
        public void ModificarItem()
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;
                if (index >= 0 && index < TLista_ItemCarrito.listaCarrito.Count)
                {
                    ItemCarrito item = TLista_ItemCarrito.listaCarrito[index];

                    // 1. Validar nueva cantidad
                    if (!int.TryParse(textBox6.Text.Trim(), out int nuevaCantidad) || nuevaCantidad <= 0)
                    {
                        MessageBox.Show("Ingrese una cantidad válida para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox6.Focus();
                        return;
                    }

                    // 2. Validar nuevo precio (por si se modificó en el textBox5)
                    if (!double.TryParse(textBox5.Text.Trim(), out double nuevoPrecio) || nuevoPrecio < 0)
                    {
                        MessageBox.Show("Ingrese un precio válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox5.Focus();
                        return;
                    }

                    Producto prod = TLista_Productos.listaProductos.FirstOrDefault(x => x.Codigo == item.Codigo);
                    if (prod != null && nuevaCantidad > prod.Stock)
                    {
                        MessageBox.Show($"Stock insuficiente. Disponible: {prod.Stock}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Respaldo de seguridad del cliente
                    string clienteActual = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : null;

                    // Actualizamos tanto la cantidad como el precio unitario
                    item.Cantidad = nuevaCantidad;
                    item.PrecioUnitario = nuevoPrecio; // <-- ¡Actualiza el precio en el ítem del carrito!

                    Listar();
                    ActualizarTotalesGenerales();

                    if (clienteActual != null)
                    {
                        comboBox1.SelectedItem = clienteActual;
                    }

                    MessageBox.Show("Ítem modificado correctamente en el carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCamposProducto();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la tabla superior para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Button 6: Eliminar un ítem del carrito
        public void EliminarItem()
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;
                if (index >= 0 && index < TLista_ItemCarrito.listaCarrito.Count)
                {
                    TLista_ItemCarrito.listaCarrito.RemoveAt(index);
                    Listar();
                    ActualizarTotalesGenerales();
                    MessageBox.Show("Producto eliminado del carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la tabla superior para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Button 3: Guardar Factura hacia el dataGridView2
        public void GuardarFactura()
        {
            if (TLista_ItemCarrito.listaCarrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos para registrar la factura.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // =========================================================================
            // BLINDAJE DE CLIENTE: Comprobamos el SelectedIndex O el Text del ComboBox
            // =========================================================================
            string nombreSeleccionado = "";
            if (comboBox1.SelectedItem != null)
            {
                nombreSeleccionado = comboBox1.SelectedItem.ToString();
            }
            else if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                nombreSeleccionado = comboBox1.Text.Trim();
            }

            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                MessageBox.Show("Por favor seleccione o escriba un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return;
            }
            // =========================================================================

            try
            {
                string numFactura = textBox3.Text.Trim();
                string rucEmpresa = "0705015865001";
                string fecha = textBox1.Text.Trim();
                string formaPago = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : "Efectivo";

                double total = double.TryParse(textBox10.Text.Trim(), out double t) ? t : 0.0;

                // Buscamos al cliente usando el nombre que capturamos de forma segura
                Cliente clienteEncontrado = TLista_Cliente.listaClientes.FirstOrDefault(c => c.NombreCompleto.Equals(nombreSeleccionado, StringComparison.OrdinalIgnoreCase));

                if (clienteEncontrado == null)
                {
                    MessageBox.Show("No se pudo identificar la cédula del cliente seleccionado en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cedulaCliente = clienteEncontrado.Cedula;

                Factura nuevaFactura = new Factura(numFactura, rucEmpresa, cedulaCliente, fecha, total, formaPago, TLista_ItemCarrito.listaCarrito.ToList());

                controladorFactura.Insertar(nuevaFactura);
                TLista_Facturas.Guardar();

                MessageBox.Show($"Factura {numFactura} registrada con éxito.", "Facturación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ListarFacturas();
                if (dataGridView2.Rows.Count > 0)
                {
                    int ultimaFila = dataGridView2.Rows.Count - 1;
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[ultimaFila].Selected = true;
                    dataGridView2.CurrentCell = dataGridView2.Rows[ultimaFila].Cells[0];
                }

                InicializarDatos(); // Esto autocalculará la siguiente factura correlativa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la factura: " + ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Actualizar subtotales generales (textBox8, textBox9, textBox10)
        public void ActualizarTotalesGenerales()
        {
            double subtotal = TLista_ItemCarrito.listaCarrito.Sum(x => x.Subtotal);
            double iva = subtotal * 0.15; // 15% IVA
            double total = subtotal + iva;

            textBox8.Text = subtotal.ToString("0.00"); // textBox8: Subtotal general
            textBox9.Text = iva.ToString("0.00");      // textBox9: IVA 15%
            textBox10.Text = total.ToString("0.00");   // textBox10: Total final
        }

        public void LimpiarCamposProducto()
        {
           
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox2.SelectedIndex = -1;
        }

        public void ConfigurarControlesPorRol()
        {
            if (rolRecibido == "ROL VENDEDOR")
            {
                // Configuraciones de permisos adicionales si se requieren
            }
        }


        // --- EVENTOS DE LOS BOTONES ---

        // Button 1: Insertar al carrito
        private void button1_Click(object sender, EventArgs e)
        {
            try { AgregarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // Button 2: Modificar detalle del carrito
        private void button2_Click(object sender, EventArgs e)
        {
            try { ModificarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // Button 3: Guardar Factura
        private void button3_Click(object sender, EventArgs e)
        {
            try { GuardarFactura(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // Button 4: (Opcional o libre)
        private void button4_Click(object sender, EventArgs e)
        {
        }

        // Button 5: Salir / Cerrar formulario
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Button 6: Eliminar ítem del carrito (papelera)
        private void button6_Click(object sender, EventArgs e)
        {
            try { EliminarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // Doble clic en la tabla inferior (dataGridView2) para ver el detalle impreso de la factura
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      
        private void FrmFractura_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. FORZAMOS LA LECTURA DE LOS ARCHIVOS CSV SIEMPRE AL ABRIR
                TLista_Cliente.Leer();
                TLista_Productos.Leer();
                TLista_Facturas.Leer();

                // 2. CARGAMOS LOS COMBOS Y DATOS
                CargarCombos();

                // 3. ESTILOS DE TABLAS
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos iniciales de la factura: " + ex.Message,
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            if (index >= 0 && index < TLista_Productos.listaProductos.Count)
            {
                Producto prod = TLista_Productos.listaProductos[index];

                textBox4.Text = prod.Codigo;
                textBox5.Text = prod.Precio.ToString("0.00");
                textBox6.Text = "1";
                CalcularSubtotalItem();
                textBox6.Focus();
            }
        }
        public void setDatos(ItemCarrito item)
        {
            if (item != null)
            {
                for (int i = 0; i < TLista_Productos.listaProductos.Count; i++)
                {
                    if (TLista_Productos.listaProductos[i].Codigo == item.Codigo)
                    {
                        comboBox2.SelectedIndex = i;
                        break;
                    }
                }

                textBox4.Text = item.Codigo;
                textBox5.Text = item.PrecioUnitario.ToString("0.00");
                textBox6.Text = item.Cantidad.ToString();
                textBox7.Text = item.Subtotal.ToString("0.00");
                textBox6.Focus();
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                if (index < TLista_Facturas.listaFacturas.Count)
                {
                    Factura facturaSeleccionada = TLista_Facturas.listaFacturas[index];

                    // =========================================================================
                    // BUSCAR EL NOMBRE DEL CLIENTE USANDO LA CÉDULA DE LA FACTURA
                    // =========================================================================
                    string nombreClienteComprobante = "Cliente General";
                    if (TLista_Cliente.listaClientes != null)
                    {
                        var clienteEncontrado = TLista_Cliente.listaClientes.FirstOrDefault(c => c.Cedula == facturaSeleccionada.CedulaCliente);
                        if (clienteEncontrado != null)
                        {
                            nombreClienteComprobante = $"{clienteEncontrado.Nombre} {clienteEncontrado.Apellido}";
                        }
                    }
                    // =========================================================================

                    string detalle =
                        "========================================\n" +
                        "          COMPROBANTE DE FACTURA        \n" +
                        "========================================\n" +
                        $"Nº Factura: {facturaSeleccionada.NumFactura}\n" +
                        $"Fecha: {facturaSeleccionada.FechaEmision} | RUC: {facturaSeleccionada.Ruc}\n" +
                        $"Cédula: {facturaSeleccionada.CedulaCliente}\n" +
                        $"Cliente: {nombreClienteComprobante}\n" +
                        $"Forma de Pago: {facturaSeleccionada.FormaPago}\n" +
                        "----------------------------------------\n" +
                        " DETALLE DE COMPRA:\n";

                    // Respaldo inteligente: Si la factura no trae ítems del CSV pero es la última o tenemos el carrito
                    var itemsAMostrar = facturaSeleccionada.DetalleItems;
                    if ((itemsAMostrar == null || itemsAMostrar.Count == 0) && TLista_ItemCarrito.listaCarrito != null && TLista_ItemCarrito.listaCarrito.Count > 0)
                    {
                        itemsAMostrar = TLista_ItemCarrito.listaCarrito.ToList();
                    }

                    // Bucle para pintar los productos
                    if (itemsAMostrar != null && itemsAMostrar.Count > 0)
                    {
                        foreach (var item in itemsAMostrar)
                        {
                            string nombreProd = !string.IsNullOrEmpty(item.Nombre) ? item.Nombre : "Producto";
                            detalle += $" - {nombreProd} (x{item.Cantidad}) ... ${item.Subtotal:0.00}\n";
                        }
                    }
                    else
                    {
                        double subCalc = facturaSeleccionada.TotalFactura / 1.15;
                        detalle += $" - Bolo Paradise Express (x1) ... ${subCalc:0.00}\n";
                    }

                    detalle += "----------------------------------------\n" +
                               $"TOTAL FACTURA: ${facturaSeleccionada.TotalFactura:0.00}\n" +
                               "========================================";

                    MessageBox.Show(detalle, "Detalle de Factura Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }





        private void FrmFractura_Activated(object sender, EventArgs e)
        {
            CargarCombos();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try { AgregarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try { ModificarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try { GuardarFactura(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try { EliminarItem(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                if (index < TLista_ItemCarrito.listaCarrito.Count)
                {
                    ItemCarrito item = TLista_ItemCarrito.listaCarrito[index];
                    setDatos(item);
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (TLista_Facturas.listaFacturas.Count == 0)
                {
                    MessageBox.Show("No hay facturas registradas en el sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int indexSeleccionado = -1;

                if (dataGridView2.CurrentRow != null)
                {
                    indexSeleccionado = dataGridView2.CurrentRow.Index;
                }
                else
                {
                    indexSeleccionado = TLista_Facturas.listaFacturas.Count - 1;
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[indexSeleccionado].Selected = true;
                    dataGridView2.CurrentCell = dataGridView2.Rows[indexSeleccionado].Cells[0];
                }

                Factura facturaSeleccionada = TLista_Facturas.listaFacturas[indexSeleccionado];

                string nombreClienteFactura = "Cliente General";
                if (TLista_Cliente.listaClientes != null)
                {
                    var clienteEncontrado = TLista_Cliente.listaClientes.FirstOrDefault(c => c.Cedula == facturaSeleccionada.CedulaCliente);
                    if (clienteEncontrado != null)
                    {
                        nombreClienteFactura = clienteEncontrado.NombreCompleto;
                    }
                }

                PrintDocument printDoc = new PrintDocument();

                printDoc.PrintPage += (s, ev) =>
                {
                    // FUENTES MÁS GRANDES PARA EVITAR EL ZOOM
                    Font fuenteTitulo = new Font("Courier New", 15, FontStyle.Bold);
                    Font fuenteTexto = new Font("Courier New", 12, FontStyle.Regular);
                    Font fuenteNegrita = new Font("Courier New", 12, FontStyle.Bold);

                    float yPos = 10;
                    float leftMargin = 10;

                    // Encabezado
                    ev.Graphics.DrawString("****************************************", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("            PARADISE EXPRESS            ", fuenteTitulo, Brushes.Black, leftMargin, yPos);
                    yPos += 22;
                    ev.Graphics.DrawString("          Pasaje - El Oro - Ecuador     ", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("****************************************", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 25;

                    // Datos de la factura
                    ev.Graphics.DrawString($"Nº Factura   : {facturaSeleccionada.NumFactura}", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString($"Fecha        : {facturaSeleccionada.FechaEmision}", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString($"RUC / Cédula : {facturaSeleccionada.CedulaCliente}", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString($"Cliente      : {nombreClienteFactura}", fuenteNegrita, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString($"Forma Pago   : {facturaSeleccionada.FormaPago}", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 22;

                    ev.Graphics.DrawString("----------------------------------------", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("CANT  DESCRIPCION          P.UNIT   SUB", fuenteNegrita, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("----------------------------------------", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 22;

                    // =========================================================================
                    // RECORRIDO IDÉNTICO AL DOBLE CLIC (Con respaldo inteligente al carrito actual)
                    // =========================================================================
                    var itemsAImprimir = facturaSeleccionada.DetalleItems;

                    if ((itemsAImprimir == null || itemsAImprimir.Count == 0) && TLista_ItemCarrito.listaCarrito != null && TLista_ItemCarrito.listaCarrito.Count > 0)
                    {
                        itemsAImprimir = TLista_ItemCarrito.listaCarrito.ToList();
                    }

                    if (itemsAImprimir != null && itemsAImprimir.Count > 0)
                    {
                        foreach (var item in itemsAImprimir)
                        {
                            string desc = !string.IsNullOrEmpty(item.Nombre) ? item.Nombre : "Producto";
                            if (desc.Length > 18) desc = desc.Substring(0, 18);

                            string lineaItem = $"{item.Cantidad,-5} {desc,-18} ${item.PrecioUnitario,5:0.00} ${item.Subtotal,5:0.00}";
                            ev.Graphics.DrawString(lineaItem, fuenteTexto, Brushes.Black, leftMargin, yPos);
                            yPos += 20;
                        }
                    }
                    else
                    {
                        // Respaldo por si es una factura antigua sin detalle
                        double subCalc = facturaSeleccionada.TotalFactura / 1.15;
                        string lineaItem = $"1     Consumo General    ${subCalc,5:0.00} ${subCalc,5:0.00}";
                        ev.Graphics.DrawString(lineaItem, fuenteTexto, Brushes.Black, leftMargin, yPos);
                        yPos += 20;
                    }
                    // =========================================================================

                    ev.Graphics.DrawString("----------------------------------------", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 22;

                    // Cálculo seguro usando el TotalFactura que ya trae la factura guardada
                    double totalCalculado = facturaSeleccionada.TotalFactura;
                    double subtotalGeneral = totalCalculado / 1.15; // Desglosando el subtotal antes del 15% de IVA
                    double ivaCalculado = totalCalculado - subtotalGeneral;

                    // Totales con fuente más grande
                    ev.Graphics.DrawString(String.Format("SUBTOTAL     : ${0,10:0.00}", subtotalGeneral), fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString(String.Format("IVA (15%)    : ${0,10:0.00}", ivaCalculado), fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 22;
                    ev.Graphics.DrawString(String.Format("TOTAL A PAGAR: ${0,10:0.00}", totalCalculado), fuenteTitulo, Brushes.Black, leftMargin, yPos);
                    yPos += 30;

                    ev.Graphics.DrawString("****************************************", fuenteTexto, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("       ¡GRACIAS POR SU PREFERENCIA!     ", fuenteNegrita, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    ev.Graphics.DrawString("****************************************", fuenteTexto, Brushes.Black, leftMargin, yPos);
                };

                PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                previewDialog.Document = printDoc;
                previewDialog.Width = 900;
                previewDialog.Height = 700;
                previewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar la vista previa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}