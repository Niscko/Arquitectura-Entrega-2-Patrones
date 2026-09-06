using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Abstracción entre los repositorios y el medio físico (ADR-07 — DIP).
    /// La implementación actual es AlmacenamientoTxt; una futura migración
    /// a SQL Server solo requiere una nueva implementación de esta interfaz.
    /// </summary>
    public interface IAlmacenamiento<T>
    {
        List<string> CargarLineas();
        void GuardarLineas(List<string> lineas);
    }
}
