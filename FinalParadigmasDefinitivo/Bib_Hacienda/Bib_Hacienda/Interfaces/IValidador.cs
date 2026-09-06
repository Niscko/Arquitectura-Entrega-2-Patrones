using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz genérica de validación (ADR-04 — ISP).
    /// Cada validador implementa solo el contrato de su propia entidad.
    /// Reemplaza a IValidarInformacion para eliminar NotImplementedException.
    /// </summary>
    public interface IValidador<T>
    {
        bool Validar(T entidad);
    }
}
