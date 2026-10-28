using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class TLista_ItemCarrito
    {
        public static List<ItemCarrito> listaCarrito = new List<ItemCarrito>();
        public int Cantidad()
        {
            return listaCarrito.Count;
        }
        public void Limpiar()
        {
            listaCarrito.Clear();
        }
        public int Localizar(string codigoProd)
        {
            int pos = -1;
            for (int i = 0; i < Cantidad(); i++)
            {
                ItemCarrito item = listaCarrito[i];
                if (codigoProd.Equals(item.Codigo, StringComparison.OrdinalIgnoreCase))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }
        public void Insertar(Producto prod, int cantidad)
        {
            if (prod == null)
            {
                throw new Exception("Por favor seleccione un producto.");
            }

            if (cantidad <= 0)
            {
                throw new Exception("Ingrese una cantidad válida mayor a cero.");
            }

            if (cantidad > prod.Stock)
            {
                throw new Exception($"Stock insuficiente. Stock actual disponible: {prod.Stock}");
            }

            int pos = Localizar(prod.Codigo);
            if (pos > -1)
            {
                ItemCarrito existente = listaCarrito[pos];
                if ((existente.Cantidad + cantidad) > prod.Stock)
                {
                    throw new Exception("La cantidad total excede el stock disponible del producto.");
                }
                existente.Cantidad += cantidad;
            }
            else
            {
                ItemCarrito nuevo = new ItemCarrito(prod.Codigo, prod.Nombre, prod.Precio, cantidad);
                listaCarrito.Add(nuevo);
            }
        }
        public void Eliminar(string codigoProd)
        {
            int pos = Localizar(codigoProd);
            if (pos > -1)
            {
                listaCarrito.RemoveAt(pos);
            }
            else
            {
                throw new Exception("Seleccione un producto válido de la tabla superior para eliminar.");
            }
        }
        public ItemCarrito GetItem(int posicion)
        {
            ItemCarrito item = null;
            if (posicion >= 0 && posicion < listaCarrito.Count)
            {
                item = listaCarrito[posicion];
            }
            return item;
        }
        public double CalcularSubtotalGeneral()
        {
            double sub = 0;
            foreach (var item in listaCarrito)
            {
                sub += item.Subtotal;
            }
            return sub;
        }

        public double CalcularIva()
        {
            return CalcularSubtotalGeneral() * 0.15; // 15% IVA
        }
        public double CalcularTotalFinal()
        {
            return CalcularSubtotalGeneral() + CalcularIva();
        }
    }
}