using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bib_Hacienda.Clases.Viva;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de vacunas (ADR-06 — DIP).
    /// </summary>
    public interface IVacunaService
    {
        string CrearVacuna(string nombre, string lote, DateTime fechaVencimiento, DateTime fechaAplicacion, uint? periodoAplicacion, enum_l_atenuaciones? atenuacion);
        string AplicarVacuna(string potreroId, string nombreRes, string loteVacuna);
        List<Vacuna> ObtenerVacunasDisponibles();
        List<Vacuna> ObtenerVacunasAplicadas(string potreroId, string nombreRes);
        Dictionary<string, object> ObtenerEstadisticas();
    }
}
