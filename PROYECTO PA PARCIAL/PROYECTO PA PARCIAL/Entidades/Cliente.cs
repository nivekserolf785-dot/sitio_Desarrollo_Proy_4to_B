using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class Cliente
    {
        private string cedula;
        private string nombre;
        private string apellido;
        private string celular;
        private string direccion;
        private string ciudad;
        private string correo;

        public Cliente()
        {
        }

        public Cliente(string ced, string nom, string ape, string cel, string dir, string ciu, string cor)
        {
            this.Cedula = ced;
            this.Nombre = nom;
            this.Apellido = ape;
            this.Celular = cel;
            this.Direccion = dir;
            this.Ciudad = ciu;
            this.Correo = cor;
        }

        public string Cedula { get => cedula; set => cedula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Celular { get => celular; set => celular = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Correo { get => correo; set => correo = value; }

        public string GetCliente()
        {
            string datos = $"{this.Cedula};{this.Nombre};{this.Apellido};{this.Celular};{this.Direccion};{this.Ciudad};{this.Correo};";
            return datos;
        }
    }
}