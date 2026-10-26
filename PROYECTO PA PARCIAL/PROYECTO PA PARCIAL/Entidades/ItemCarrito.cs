using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class ItemCarrito
    {
        private string codigo;
        private string nombre;
        private double precioUnitario;
        private int cantidad;
        public double Subtotal => PrecioUnitario * Cantidad;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public double PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }

        public ItemCarrito()
        {
        }

        public ItemCarrito(string codigo, string nombre, double precioUnitario, int cantidad)
        {
            Codigo = codigo;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }
    }
}