using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_Productos
    {
        public static List<Producto> listaProductos = new List<Producto>();
        public static string archivoCsv = Global.GetPath() + "productos.csv";
        public static void Leer()
        {
            listaProductos.Clear();

            if (File.Exists(archivoCsv))
            {
                string[] lineas = File.ReadAllLines(archivoCsv);
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] partes = linea.Split(';');
                    if (partes.Length >= 7)
                    {
                        Producto p = new Producto(
                            partes[0], // Código
                            partes[1], // Nombre
                            partes[2], // Categoria
                            partes[3], // Marca
                            double.TryParse(partes[4], out double precio) ? precio : 0.0,
                            double.TryParse(partes[5], out double stock) ? stock : 0.0,
                            DateTime.TryParse(partes[6], out DateTime fecha) ? fecha : DateTime.Now
                        );
                        listaProductos.Add(p);
                    }
                }
            }
        }

        public static void Guardar()
        {
            List<string> lineas = new List<string>();

            foreach (var p in listaProductos)
            {
                string linea = $"{p.Codigo};{p.Nombre};{p.Categoria};{p.Marca};{p.Precio};{p.Stock};{p.FechaCaducidad:yyyy-MM-dd}";
                lineas.Add(linea);
            }

            File.WriteAllLines(archivoCsv, lineas);
        }
       
        public int Cantidad()
        {
            return listaProductos.Count;
        }

        // Este método permite vaciar por completo todos los productos de la lista
        public void Limpiar()
        {
            listaProductos.Clear();
        }
        public int Localizar(string codigoProd)
        {
            int pos = -1;
            for (int i = 0; i < Cantidad(); i++)
            {
                Producto ob = listaProductos[i];
                if (codigoProd.Equals(ob.Codigo, StringComparison.OrdinalIgnoreCase))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }

        public void Insertar(Producto p)
        {
            int pos = Localizar(p.Codigo);
            if (pos == -1)
            {
                listaProductos.Add(p);
            }
            else
            {
                throw new Exception("Producto ya agregado previamente: " + p.Codigo);
            }
        }
        public void Modificar(Producto p, string codigoProd)
        {
            int pos = Localizar(codigoProd);
            if (pos > -1)
            {
                listaProductos[pos] = p;
            }
            else
            {
                throw new Exception("**No existe un producto registrado con el código ingresado:** " + codigoProd);
            }
        }
        public void Eliminar(string codigoProd)
        {
            int pos = Localizar(codigoProd);
            if (pos > -1)
            {
                listaProductos.RemoveAt(pos);
            }
            else
            {
                throw new Exception("**No existe un producto registrado con el código ingresado:** " + codigoProd);
            }
        }

        public Producto GetProducto(int posicion)
        {
            Producto p = null;
            if (posicion >= 0 && posicion < listaProductos.Count)
            {
                p = listaProductos[posicion];
            }
            return p;
        }

        public List<Producto> BuscarProductos(string texto)
        {
            List<Producto> resultado = new List<Producto>();
            texto = texto.ToLower();

            for (int i = 0; i < listaProductos.Count; i++)
            {
                Producto p = listaProductos[i];
                if (p.Codigo.ToLower().Contains(texto)
                    || p.Nombre.ToLower().Contains(texto)
                    || p.Marca.ToLower().Contains(texto)
                    || p.Categoria.ToLower().Contains(texto))
                {
                    resultado.Add(p);
                }
            }
            return resultado;
        }

        public string GenerarCodigoAutomatico()
        {
            int nuevoNumero = 1;
            if (listaProductos.Count > 0)
            {
                int maxNum = 0;
                foreach (var p in listaProductos)
                {
                    if (p.Codigo != null && p.Codigo.StartsWith("P") && int.TryParse(p.Codigo.Substring(1), out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
                nuevoNumero = maxNum + 1;
            }
            return "P" + nuevoNumero.ToString("D3"); // Genera formatos como P023, P024, etc.
        }
    }
}