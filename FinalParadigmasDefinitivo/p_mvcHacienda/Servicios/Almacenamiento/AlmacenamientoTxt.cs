using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios.Almacenamiento
{
    /// <summary>
    /// Implementación de IAlmacenamiento&lt;T&gt; para archivos .txt (ADR-07 — DIP).
    /// Preserva el comportamiento observable actual (archivos .txt) pero aísla
    /// el punto de cambio para una futura migración a otra fuente de datos.
    /// </summary>
    public class AlmacenamientoTxt<T> : IAlmacenamiento<T>
    {
        private readonly string _rutaArchivo;

        public AlmacenamientoTxt(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public List<string> CargarLineas()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<string>();
            }

            return File.ReadAllLines(_rutaArchivo)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();
        }

        public void GuardarLineas(List<string> lineas)
        {
            var directorio = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            File.WriteAllLines(_rutaArchivo, lineas);
        }
    }
}
