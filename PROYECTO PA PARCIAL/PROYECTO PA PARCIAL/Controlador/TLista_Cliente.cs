using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_Cliente
    {
    
        public static List<Cliente> listaClientes = new List<Cliente>();
        public int Cantidad()
        {
            return listaClientes.Count;
        }
        public void Limpiar()
        {
            listaClientes.Clear();
        }
        public int Localizar(string cedula)
        {
            int pos = -1;
            for (int i = 0; i < Cantidad(); i++)
            {
                Cliente c = listaClientes[i];
                if (cedula.Equals(c.Cedula, StringComparison.OrdinalIgnoreCase))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }

        public void Insertar(Cliente c)
        {
            int pos = Localizar(c.Cedula);
            if (pos == -1)
            {
                listaClientes.Add(c);
            }
            else
            {
                throw new Exception("Cliente ya registrado con cédula: " + c.Cedula);
            }
        }

        // Este método permite modificar un cliente existente buscando su cédula y actualizando sus campos con un botón
        public void Modificar(Cliente c, string cedula)
        {
            int pos = Localizar(cedula);
            if (pos > -1)
            {
                listaClientes[pos] = c;
            }
            else
            {
                throw new Exception("**No existe cliente con cédula: " + cedula + "**");
            }
        }

        // Este método permite eliminar un cliente de la lista mediante un botón y su número de cédula
        public void Eliminar(string cedula)
        {
            int pos = Localizar(cedula);
            if (pos > -1)
            {
                listaClientes.RemoveAt(pos);
            }
            else
            {
                throw new Exception("**No existe cliente con cédula: " + cedula + "**");
            }
        }

        // Este método permite obtener un cliente específico según su posición al hacer clic en una fila del DataGridView
        public Cliente GetCliente(int posicion)
        {
            Cliente c = null;
            if (posicion >= 0 && posicion < listaClientes.Count)
            {
                c = listaClientes[posicion];
            }
            return c;
        }

        // Este método permite escribir texto en un TextBox y filtrar los clientes que coincidan en cédula, nombre o ciudad
        public List<Cliente> BuscarClientes(string texto)
        {
            List<Cliente> resultado = new List<Cliente>();
            texto = texto.ToLower();

            for (int i = 0; i < listaClientes.Count; i++)
            {
                Cliente c = listaClientes[i];
                if (c.Cedula.ToLower().Contains(texto)
                    || c.Nombre.ToLower().Contains(texto)
                    || c.Ciudad.ToLower().Contains(texto))
                {
                    resultado.Add(c);
                }
            }
            return resultado;
        }
    }
}
