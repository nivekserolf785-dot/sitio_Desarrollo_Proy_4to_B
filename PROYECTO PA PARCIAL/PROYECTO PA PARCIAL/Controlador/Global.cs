using System;
using System.IO;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public static class Global
    {
        public static string GetPath()
        {
          
            string path = @"C:\Users\kevin\Desktop\Proy_cuarto_Semestre\sitio_Desarrollo_Proy_4to_B\PROYECTO PA PARCIAL\PROYECTO PA PARCIAL\Data\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}