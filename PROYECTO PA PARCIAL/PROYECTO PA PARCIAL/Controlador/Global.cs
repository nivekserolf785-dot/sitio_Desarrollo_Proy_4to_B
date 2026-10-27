using System;
using System.IO;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public static class Global
    {
        public static string GetPath()
        {
            // Ruta fija y absoluta hacia la carpeta Data de su proyecto en el Escritorio
            string path = @"C:\Users\kevin\Desktop\Proy_cuarto_Semestre\sitio_Desarrollo_Proy_4to_B\PROYECTO PA PARCIAL\PROYECTO PA PARCIAL\Data\";

            // Si por alguna razón la carpeta no existe, la creamos automáticamente
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}