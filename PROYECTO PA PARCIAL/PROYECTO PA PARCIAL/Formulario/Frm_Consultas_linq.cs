using PROYECTO_PA_PARCIAL.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_PA_PARCIAL.Formulario
{
    public partial class Frm_Consultas_linq : Form
    {
        private string tipoReporte;

        public Frm_Consultas_linq(string tipo)
        {
            InitializeComponent();
            this.tipoReporte = tipo;
        }

        private void Frm_Consultas_linq_Load(object sender, EventArgs e)
        {
            CargarConsultaLinQ();
        }

        private void CargarConsultaLinQ()
        {
            try
            {
                TLista_Productos.Leer();
                TLista_Cliente.Leer();
                TLista_Facturas.Leer();

                switch (tipoReporte)
                {
                    case "Stock productos":
                        this.Text = "Consulta: Stock de Productos";
                        var stockQuery = TLista_Productos.listaProductos
                            .Select(p => new { Código = p.Codigo, Producto = p.Nombre, Precio = p.Precio, Stock = p.Stock })
                            .ToList();
                        dataGridView1.DataSource = stockQuery;
                        break;

                    case "Mejor cliente":
                        this.Text = "Consulta: Mejor Cliente (por monto en facturas)";
                        var mejorClienteQuery = TLista_Facturas.listaFacturas
                            .GroupBy(f => f.CedulaCliente)
                            .Select(g => new {
                                Cedula = g.Key,
                                TotalComprado = g.Sum(f => f.TotalFactura),
                                CantidadFacturas = g.Count()
                            })
                            .OrderByDescending(x => x.TotalComprado)
                            .Join(TLista_Cliente.listaClientes,
                                  f => f.Cedula,
                                  c => c.Cedula,
                                  (f, c) => new {
                                      Cedula = c.Cedula,
                                      Cliente = c.NombreCompleto,
                                      TotalComprado = $"${f.TotalComprado:0.00}",
                                      FacturasEmitidas = f.CantidadFacturas
                                  })
                            .ToList();
                        dataGridView1.DataSource = mejorClienteQuery;
                        break;

                    case "Productos cerca a expirar":
                        this.Text = "Consulta: Productos Próximos a Caducar (Menos de 1 mes)";

                        DateTime fechaHoy = DateTime.Now;
                        DateTime fechaLimite = fechaHoy.AddDays(30);

                        var porExpirarQuery = TLista_Productos.listaProductos
                            .Where(p => p.FechaCaducidad <= fechaLimite)
                            .Select(p => new {
                                Código = p.Codigo,
                                Producto = p.Nombre,
                                Stock = p.Stock,
                                Fecha_Caducidad = p.FechaCaducidad.ToString("dd/MM/yyyy"),
                                Estado = p.FechaCaducidad < fechaHoy ? "¡VENCIDO!" : "¡Próximo a expirar!"
                            })
                            .ToList();

                        dataGridView1.DataSource = porExpirarQuery;
                        break;

                    case "Total sum $ productos":
                        this.Text = "Consulta: Valor Total del Inventario";
                        var valorInventario = TLista_Productos.listaProductos
                            .Sum(p => p.Precio * p.Stock);

                        var totalInvTabla = new List<object> {
                            new { Descripcion = "Valor Total General del Inventario en Bodega", ValorTotal = $"${valorInventario:0.00}" }
                        };
                        dataGridView1.DataSource = totalInvTabla;
                        break;

                    case "porcentaje de compras por cliente":
                        this.Text = "Consulta: Porcentaje de Compras por Cliente";
                        double granTotalVentas = TLista_Facturas.listaFacturas.Sum(f => f.TotalFactura);
                        if (granTotalVentas == 0) granTotalVentas = 1;

                        var porcentajeClientes = TLista_Facturas.listaFacturas
                            .GroupBy(f => f.CedulaCliente)
                            .Select(g => new {
                                Cedula = g.Key,
                                TotalCliente = g.Sum(f => f.TotalFactura)
                            })
                            .Join(TLista_Cliente.listaClientes,
                                  f => f.Cedula,
                                  c => c.Cedula,
                                  (f, c) => new {
                                      Cliente = c.NombreCompleto,
                                      TotalComprado = f.TotalCliente,
                                      Porcentaje = $"{((f.TotalCliente / granTotalVentas) * 100):0.00}%"
                                  })
                            .ToList();
                        dataGridView1.DataSource = porcentajeClientes;
                        break;

                    case "porcentaje compras femenino":
                        this.Text = "Consulta: Porcentaje de Compras - Género Femenino";
                        double totalGenV = TLista_Facturas.listaFacturas.Sum(f => f.TotalFactura);
                        if (totalGenV == 0) totalGenV = 1;

                        var comprasFemenino = TLista_Facturas.listaFacturas
                            .Join(TLista_Cliente.listaClientes,
                                  f => f.CedulaCliente,
                                  c => c.Cedula,
                                  (f, c) => new { f.TotalFactura, c.Sexo })
                            .Where(x => x.Sexo == 'F' || x.Sexo == 'f')
                            .Sum(x => x.TotalFactura);

                        double porcFem = (comprasFemenino / totalGenV) * 100;

                        dataGridView1.DataSource = new List<object> {
                            new { Categoria = "Clientes Femenino", TotalVentas = $"${comprasFemenino:0.00}", PorcentajeGlobal = $"{porcFem:0.02}%" }
                        };
                        break;

                    case "porcentaje compras masculino":
                        this.Text = "Consulta: Porcentaje de Compras - Género Masculino";
                        double totalGenM = TLista_Facturas.listaFacturas.Sum(f => f.TotalFactura);
                        if (totalGenM == 0) totalGenM = 1;

                        var comprasMasculino = TLista_Facturas.listaFacturas
                            .Join(TLista_Cliente.listaClientes,
                                  f => f.CedulaCliente,
                                  c => c.Cedula,
                                  (f, c) => new { f.TotalFactura, c.Sexo })
                            .Where(x => x.Sexo == 'M' || x.Sexo == 'm')
                            .Sum(x => x.TotalFactura);

                        double porcMasc = (comprasMasculino / totalGenM) * 100;

                        dataGridView1.DataSource = new List<object> {
                            new { Categoria = "Clientes Masculino", TotalVentas = $"${comprasMasculino:0.00}", PorcentajeGlobal = $"{porcMasc:0.02}%" }
                        };
                        break;

                    default:
                        MessageBox.Show("Consulta no reconocida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta LINQ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}