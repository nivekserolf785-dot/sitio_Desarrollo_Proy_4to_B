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
        private DateTime fechaCaducidad;

  
        public Producto()
        { }
        public Producto(string codigo, string nombre, string categoria, string marca, double precio, double stock, DateTime fechaCaducidad)
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
        public DateTime FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
        public string NombreCategoria => $"{this.Nombre} - {this.Categoria}";
        public string GetProducto()
        {
            return $"{this.Nombre};{this.Marca};{this.Codigo};{this.Precio};{this.Categoria};{this.Stock};";
        }
    }
}