using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_Vendedor
    {
        public static List<Vendedor> listaVendedores = new List<Vendedor>();
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