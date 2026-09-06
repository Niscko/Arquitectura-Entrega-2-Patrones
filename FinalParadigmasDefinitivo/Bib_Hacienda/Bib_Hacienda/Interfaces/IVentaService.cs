using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de ventas (UML - Imagen 1 - DIP).
    /// </summary>
    public interface IVentaService
    {
        List<Venta> ObtenerTodasLasVentas();
        List<Venta> ObtenerVentasPorPotrero(string potreroId);
        List<Venta> ObtenerVentasPorFechas(DateTime fechaInicio, DateTime fechaFin);
        string RegistrarVenta(Potrero potrero, DateTime fecha, List<ItemVenta> items);
        Dictionary<string, object> ObtenerEstadisticas();
    }
}
