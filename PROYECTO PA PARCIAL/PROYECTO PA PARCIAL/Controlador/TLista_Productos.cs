using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    internal class TLista_Productos
    {
        public static List<Producto> listaProductos = new List<Producto>();

        // Este método permite obtener la cantidad total de productos registrados en la lista
        public int Cantidad()
        {
            return listaProductos.Count;
        }

        // Este método permite vaciar por completo todos los productos de la lista
        public void Limpiar()
        {
            listaProductos.Clear();
        }

        // Este método realiza una búsqueda secuencial utilizando el código del producto
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

        // Este método permite ingresar un nuevo producto validando que el código no esté repetido
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

        // Este método permite modificar un producto existente buscando su código y actualizando sus datos
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

        // Este método permite eliminar un producto de la lista mediante su código
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

        // Este método permite obtener un producto específico según su posición en la lista
        public Producto GetProducto(int posicion)
        {
            Producto p = null;
            if (posicion >= 0 && posicion < listaProductos.Count)
            {
                p = listaProductos[posicion];
            }
            return p;
        }

        // Este método permite filtrar los productos escribiendo texto en un TextBox
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
    }
}