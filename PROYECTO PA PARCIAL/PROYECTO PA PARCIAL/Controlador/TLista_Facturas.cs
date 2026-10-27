using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_Facturas
    {
        public static List<Factura> listaFacturas = new List<Factura>();
        public static string archivoCsv = Global.GetPath() + "facturas.csv";

        // --- MÉTODOS DE ARCHIVO CSV ---
        public static void Guardar()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(archivoCsv, false))
                {
                    foreach (var f in listaFacturas)
                    {
                        sw.WriteLine(f.GetFactura());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el archivo CSV de facturas: " + ex.Message);
            }
        }

        public static void Leer()
        {
            try
            {
                if (File.Exists(archivoCsv))
                {
                    listaFacturas.Clear();
                    string[] lineas = File.ReadAllLines(archivoCsv);
                    foreach (var linea in lineas)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] partes = linea.Split(';');
                            if (partes.Length >= 6)
                            {
                                // Asumiendo formato: NumFactura;Ruc;CedulaCliente;FechaEmision;TotalFactura;FormaPago
                                Factura f = new Factura(
                                    partes[0],
                                    partes[1],
                                    partes[2],
                                    partes[3],
                                    double.TryParse(partes[4], out double tot) ? tot : 0.0,
                                    partes[5]
                                );
                                listaFacturas.Add(f);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer el archivo CSV de facturas: " + ex.Message);
            }
        }
        public int Cantidad()
        {
            return listaFacturas.Count;
        }

        public void Limpiar()
        {
            listaFacturas.Clear();
        }

       
        public Factura GetFactura(int posicion)
        {
            if (posicion >= 0 && posicion < listaFacturas.Count)
            {
                return listaFacturas[posicion];
            }
            return null;
        }

      
        public int Localizar(string numFactura)
        {
            for (int i = 0; i < Cantidad(); i++)
            {
                Factura ob = listaFacturas[i];
                if (numFactura.Equals(ob.NumFactura, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Insertar(Factura f)
        {
            int pos = Localizar(f.NumFactura);
            if (pos == -1)
            {
                // Valida que el cliente exista usando el controlador TLista_Cliente
                TLista_Cliente validadorCliente = new TLista_Cliente();
                if (validadorCliente.Localizar(f.CedulaCliente) == -1)
                {
                    throw new Exception("** El cliente con cédula " + f.CedulaCliente + " no está registrado **");
                }

                if (f.FormaPago != "Efectivo" && f.FormaPago != "Transferencia")
                {
                    throw new Exception("** Forma de pago inválida: debe ser 'Efectivo' o 'Transferencia' **");
                }

                listaFacturas.Add(f);
            }
            else
            {
                throw new Exception("** La factura con número " + f.NumFactura + " ya existe **");
            }
        }

        public void Modificar(Factura f, string numFactura)
        {
            int pos = Localizar(numFactura);
            if (pos > -1)
            {
                TLista_Cliente validadorCliente = new TLista_Cliente();
                if (validadorCliente.Localizar(f.CedulaCliente) == -1)
                {
                    throw new Exception("** El cliente con cédula " + f.CedulaCliente + " no está registrado **");
                }

                if (f.FormaPago != "Efectivo" && f.FormaPago != "Transferencia")
                {
                    throw new Exception("** Forma de pago inválida: debe ser 'Efectivo' o 'Transferencia' **");
                }

                listaFacturas[pos] = f;
            }
            else
            {
                throw new Exception("** No existe factura con número: " + numFactura + " **");
            }
        }

        // Este método permite eliminar una factura de la lista mediante su número
        public void Eliminar(string numFactura)
        {
            int pos = Localizar(numFactura);
            if (pos > -1)
            {
                listaFacturas.RemoveAt(pos);
            }
            else
            {
                throw new Exception("** No existe factura con número: " + numFactura + " **");
            }
        }
    }
}