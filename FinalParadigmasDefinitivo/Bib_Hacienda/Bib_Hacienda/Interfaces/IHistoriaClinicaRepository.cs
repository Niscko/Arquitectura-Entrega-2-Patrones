using Bib_Hacienda.Clases;
using System.Collections.Generic;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Repositorio específico para historias clínicas (UML - Persistencia DIP).
    /// </summary>
    public interface IHistoriaClinicaRepository
    {
        HistoriaClinica CargarHistoriaClinica(string resNombre);
        void GuardarHistoriaClinica(string resNombre, HistoriaClinica historia);
    }
}
