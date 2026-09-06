using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios
{
    /// <summary>
    /// Servicio de ventas (UML - Imagen 1 - DIP).
    /// Depende de IVentaRepository (+ registrarVenta(items)).
    /// </summary>
    public class VentaService : IVentaService
    {
        private readonly Hacienda _hacienda;
        private readonly PersistenciaService _persistencia;
        private readonly IVentaRepository _ventaRepository;

        public VentaService(Hacienda hacienda, PersistenciaService persistencia, IVentaRepository ventaRepository)
        {
            _hacienda = hacienda;
            _persistencia = persistencia;
            _ventaRepository = ventaRepository;
        }

        public VentaService(Hacienda hacienda, PersistenciaService persistencia)
        {
            _hacienda = hacienda;
            _persistencia = persistencia;
        }

        public List<Venta> ObtenerTodasLasVentas()
        {
            return _hacienda.L_ventas.OrderByDescending(v => v.Fecha).ToList();
        }

        public List<Venta> ObtenerVentasPorPotrero(string potreroId)
        {
            return _hacienda.L_ventas
                .Where(v => v.Potrero.Identificacion == potreroId)
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        public List<Venta> ObtenerVentasPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return _hacienda.L_ventas
                .Where(v => v.Fecha >= fechaInicio && v.Fecha <= fechaFin)
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        public string RegistrarVenta(Potrero potrero, DateTime fecha, List<ItemVenta> items)
        {
            var venta = new Venta(potrero, fecha, items);
            _hacienda.L_ventas.Add(venta);

            if (_ventaRepository != null)
            {
                return _ventaRepository.GuardarVentas(_hacienda.L_ventas);
            }
            else
            {
                return _persistencia.GuardarVentas(_hacienda.L_ventas);
            }
        }

        public Dictionary<string, object> ObtenerEstadisticas()
        {
            var ventas = _hacienda.L_ventas;

            return new Dictionary<string, object>
            {
                { "TotalVentas", ventas.Count },
                { "MontoTotal", ventas.Sum(v => v.Monto) },
                { "PromedioVenta", ventas.Any() ? ventas.Average(v => v.Monto) : 0 },
                { "VentasEsteMes", ventas.Count(v => v.Fecha.Month == DateTime.Now.Month && v.Fecha.Year == DateTime.Now.Year) },
                { "MontoEsteMes", ventas.Where(v => v.Fecha.Month == DateTime.Now.Month && v.Fecha.Year == DateTime.Now.Year).Sum(v => v.Monto) }
            };
        }
    }
}
