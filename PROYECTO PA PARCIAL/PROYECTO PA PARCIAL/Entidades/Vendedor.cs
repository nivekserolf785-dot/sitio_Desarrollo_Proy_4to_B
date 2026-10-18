using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class Vendedor
    {
        private string cedulav;
        private string nombrev;
        private string apellidov;
        private string celularv;
        private string ciudadv;
        public Vendedor()
        {}
        public Vendedor(string cedv, string nomv, string apev, string celv, string ciuv)
        {
            this.Cedulav = cedv;
            this.Nombrev = nomv;
            this.Apellidov = apev;
            this.Celularv = celv;
            this.Ciudadv = ciuv;
        }
        public string Ciudadv { get => Ciudadv1; set => Ciudadv1 = value; }
        public string Cedulav { get => cedulav; set => cedulav = value; }
        public string Nombrev { get => nombrev; set => nombrev = value; }
        public string Apellidov { get => apellidov; set => apellidov = value; }
        public string Celularv { get => celularv; set => celularv = value; }
        public string Ciudadv1 { get => ciudadv; set => ciudadv = value; }
        public string GetVendedor()
        {
            string datos = $"{this.Cedulav};{this.Nombrev};{this.Apellidov};{this.Celularv};{this.Ciudadv};";
            return datos;
        }
    }
}