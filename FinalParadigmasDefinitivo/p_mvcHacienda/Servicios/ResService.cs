using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios
{
    /// <summary>
    /// Servicio de reses (UML - Imagen 1 - DIP).
    /// Depende de IResRepository.
    /// </summary>
    public class ResService : IResService
    {
        private readonly Hacienda _hacienda;
        private readonly PersistenciaService _persistencia;
        private readonly IResRepository _resRepository;

        public ResService(Hacienda hacienda, PersistenciaService persistencia, IResRepository resRepository)
        {
            _hacienda = hacienda;
            _persistencia = persistencia;
            _resRepository = resRepository;
        }

        public ResService(Hacienda hacienda, PersistenciaService persistencia)
        {
            _hacienda = hacienda;
            _persistencia = persistencia;
        }

        public List<(Potrero Potrero, Res Res)> ObtenerTodasLasReses()
        {
            var resesConPotrero = new List<(Potrero, Res)>();

            foreach (var potrero in _hacienda.L_potreros)
            {
                foreach (var res in potrero.L_reses)
                {
                    resesConPotrero.Add((potrero, res));
                }
            }

            return resesConPotrero;
        }

        public Res BuscarRes(string potreroId, string nombreRes)
        {
            try
            {
                var potrero = _hacienda.buscar_potrero(potreroId);
                return potrero.buscar_res(nombreRes);
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, object> ObtenerEstadisticas()
        {
            var todasLasReses = ObtenerTodasLasReses();

            return new Dictionary<string, object>
            {
                { "TotalReses", todasLasReses.Count },
                { "Terneros", todasLasReses.Count(r => r.Res is Ternero) },
                { "Cebones", todasLasReses.Count(r => r.Res is Cebon) },
                { "Novillos", todasLasReses.Count(r => r.Res is Novillo) },
                { "PesoPromedio", todasLasReses.Any() ? todasLasReses.Average(r => r.Res.Peso) : 0 }
            };
        }
    }
}
