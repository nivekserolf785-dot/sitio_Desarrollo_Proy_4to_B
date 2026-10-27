using PROYECTO_PA_PARCIAL.Controlador;
using PROYECTO_PA_PARCIAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Test
{
    public class TestSistema
    {
        public static void EjecutarPruebas()
        {
            try
            {
                // 1. Limpiar lista o preparar datos
                TLista_Cliente.listaClientes.Clear();

                // 2. Crear clientes de prueba
                Cliente c1 = new Cliente("0701234567", "Kevin", "Flores", 'M', "0999999999", "Av. Central", "Pasaje", "kevin@mail.com");
                Cliente c2 = new Cliente("0709876543", "Kerly", "Zambrano", 'F', "0988888888", "Calle Bolivar", "Machala", "kerly@mail.com");

                // 3. Insertar usando métodos estáticos o directamente a la lista estática
                TLista_Cliente.listaClientes.Add(c1);
                TLista_Cliente.listaClientes.Add(c2);

                // 4. Guardar y leer en CSV de forma estática
                TLista_Cliente.Guardar();
                TLista_Cliente.Leer();

                Console.WriteLine("¡Clientes guardados y leídos desde CSV exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la prueba: " + ex.Message);
            }
        }
    }
}