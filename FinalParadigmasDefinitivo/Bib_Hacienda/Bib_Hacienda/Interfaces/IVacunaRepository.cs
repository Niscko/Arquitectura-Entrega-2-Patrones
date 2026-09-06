using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Repositorio específico para persistencia de vacunas (ADR-03 — SRP/DIP).
    /// </summary>
    public interface IVacunaRepository
    {
        List<Vacuna> CargarVacunas();
        string GuardarVacunas(List<Vacuna> vacunas);
        void CargarVacunasAplicadas(List<Potrero> potreros);
        string GuardarVacunasAplicadas(List<Potrero> potreros);
    }
}
