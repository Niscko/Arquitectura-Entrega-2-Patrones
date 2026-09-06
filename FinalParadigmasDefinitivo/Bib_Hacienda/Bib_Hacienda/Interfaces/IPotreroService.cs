using Bib_Hacienda.Clases;
using System.Collections.Generic;
using static Bib_Hacienda.Clases.Potrero;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de potreros (ADR-06 — DIP).
    /// </summary>
    public interface IPotreroService
    {
        string CrearPotrero(string identificacion, l_tipos_potreros tipo);
        List<Potrero> ObtenerTodosLosPotreros();
        Potrero? ObtenerPotreroPorIdentificacion(string identificacion);
        string AgregarRes(string potreroId, string nombreRes, ushort edad, uint peso);
        Dictionary<string, object> ObtenerEstadisticas();
    }
}
