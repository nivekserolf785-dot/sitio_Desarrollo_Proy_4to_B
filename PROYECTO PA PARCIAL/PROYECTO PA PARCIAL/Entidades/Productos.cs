using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class Producto
    {
        private string codigo;
        private string nombre;
        private string categoria;
        private string marca;
        private double precio;
        private double stock;
        private string fechaCaducidad;

        // Constructor vacío
        public Producto()
        { }
        public Producto(string codigo, string nombre, string categoria, string marca, double precio, double stock, string fechaCaducidad)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Categoria = categoria;
            this.Marca = marca;
            this.Precio = precio;
            this.Stock = stock;
            this.FechaCaducidad = fechaCaducidad;
        }
public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Marca { get => marca; set => marca = value; }
        public double Precio { get => precio; set => precio = value; }
        public double Stock { get => stock; set => stock = value; }
        public string FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }

        public string GetProducto()
        {
            string datos = this.Nombre + ";";
            datos += this.Marca + ";";
            datos += this.Codigo + ";";
            datos += this.Precio + ";";
            datos += this.Categoria + ";";
            datos += this.Stock + ";";
            return datos;

        }
    }
}