using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Repositorio específico para persistencia de ventas (ADR-03 — SRP/DIP).
    /// </summary>
    public interface IVentaRepository
    {
        List<Venta> CargarVentas(List<Potrero> potreros);
        string GuardarVentas(List<Venta> ventas);
    }
}
