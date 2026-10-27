using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_Vendedor
    {
        public static List<Vendedor> listaVendedores = new List<Vendedor>();
        public static string archivoCsv = Global.GetPath() + "vendedores.csv";
        public static void Leer()
        {
            listaVendedores.Clear();
            if (File.Exists(archivoCsv))
            {
                string[] lineas = File.ReadAllLines(archivoCsv);
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] partes = linea.Split(';');
                    if (partes.Length >= 5)
                    {
                        Vendedor v = new Vendedor(
                            partes[0], // Cedulav
                            partes[1], // Nombrev
                            partes[2], // Apellidov
                            partes[3], // Celularv
                            partes[4]  // Ciudadv
                        );
                        listaVendedores.Add(v);
                    }
                }
            }
        }

        public static void Guardar()
        {
            List<string> lineas = new List<string>();
            foreach (var v in listaVendedores)
            {
                // Aprovechamos el método GetVendedor() que ya tiene su entidad
                lineas.Add(v.GetVendedor().TrimEnd(';'));
            }
            File.WriteAllLines(archivoCsv, lineas);
        }
        public int Cantidad()
        {
            return listaVendedores.Count;
        }
        public void Limpiar()
        {
            listaVendedores.Clear();
        }
        public Vendedor GetVendedor(int posicion)
        {
            Vendedor v = null;
            if (posicion >= 0 && posicion < listaVendedores.Count)
            {
                v = listaVendedores[posicion];
            }
            return v;
        }
        public int Localizar(string cedulav)
        {
            int pos = -1;
            for (int i = 0; i < Cantidad(); i++)
            {
                Vendedor ob = listaVendedores[i];
                if (cedulav.Equals(ob.Cedulav, StringComparison.OrdinalIgnoreCase))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }
        public void Insertar(Vendedor v)
        {
            int pos = Localizar(v.Cedulav);
            if (pos == -1)
            {
                listaVendedores.Add(v);
            }
            else
            {
                throw new Exception("** Vendedor ya registrado con cédula: " + v.Cedulav + " **");
            }
        }
        public void Modificar(Vendedor v, string cedula)
        {
            int pos = Localizar(cedula);
            if (pos > -1)
            {
                listaVendedores[pos] = v;
            }
            else
            {
                throw new Exception("** No existe vendedor con cédula: " + cedula + " **");
            }
        }
        public List<Vendedor> BuscarVendedores(string texto)
        {
            texto = texto.ToLower();
            return listaVendedores.Where(v =>
                v.Cedulav.ToLower().Contains(texto) ||
                v.Nombrev.ToLower().Contains(texto) ||
                v.Apellidov.ToLower().Contains(texto)).ToList();
        }
        public void Eliminar(string cedula)
        {
            int pos = Localizar(cedula);
            if (pos > -1)
            {
                listaVendedores.RemoveAt(pos);
            }
            else
            {
                throw new Exception("** No existe vendedor con cédula: " + cedula + " **");
            }
        }
    }
}