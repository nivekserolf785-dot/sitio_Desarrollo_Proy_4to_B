using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class Login
    {
        private string clave;
        private string rol; 

        public string Clave { get => clave; set => clave = value; }
        public string Rol { get => rol; set => rol = value; }

        public Login()
        {
        }
        public Login(string clav, string rl)
        {
            this.Clave = clav;
            this.Rol = rl;
        }
        public string GetLogin()
        {
            string datos = $"{this.Clave};{this.Rol};";
            return datos;
        }
    }
}