using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Controlador
{
    public static class Global
    {
        public static string GetPath()
        {
            // Obtiene la ruta base del ejecutable y retrocede hasta la carpeta del proyecto
            string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

            // Si compila en bin\Debug\, subimos niveles para llegar a la raíz del proyecto
            DirectoryInfo dir = new DirectoryInfo(directorioBase);
            while (dir != null && dir.GetDirectories("src").Length == 0 && dir.Parent != null)
            {
                dir = dir.Parent;
            }

            string path = dir != null ? Path.Combine(dir.FullName, "Data") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            // Si la carpeta Data no existe, la creamos automáticamente
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path + Path.DirectorySeparatorChar;
        }
    }
}