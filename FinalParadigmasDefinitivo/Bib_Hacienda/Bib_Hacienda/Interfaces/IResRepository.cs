using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Repositorio específico para persistencia de reses (ADR-03 — SRP/DIP).
    /// </summary>
    public interface IResRepository
    {
        void CargarReses(List<Potrero> potreros);
        string GuardarReses(List<Potrero> potreros);
    }
}
