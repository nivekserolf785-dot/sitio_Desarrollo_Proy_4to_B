using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public class cLogin
    {
       
        public bool Validar(string claveIngresada, string rolSeleccionado)
        {
            if (rolSeleccionado == "GERENTE" && claveIngresada == "Emperador1996#")
            {
                return true;
            }
            if (rolSeleccionado == "VENDEDOR" && claveIngresada == "Vendor555")
            {
                return true;
            }
            return false;
        }
    }
}