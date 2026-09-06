using Bib_Hacienda.Clases;
using System.Collections.Generic;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de reses (ADR-06 — DIP).
    /// </summary>
    public interface IResService
    {
        List<(Potrero Potrero, Res Res)> ObtenerTodasLasReses();
        Res? BuscarRes(string potreroId, string nombreRes);
        Dictionary<string, object> ObtenerEstadisticas();
    }
}
