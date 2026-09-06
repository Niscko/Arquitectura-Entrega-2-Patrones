using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios
{
    /// <summary>
    /// Fachada de Persistencia (ADR-03 — SRP/DIP).
    /// Mantiene la misma interfaz pública consumida por los controladores,
    /// pero delega internamente a los repositorios específicos de cada entidad.
    /// </summary>
    public class PersistenciaService
    {
        private readonly IPotreroRepository _potreroRepository;
        private readonly IResRepository _resRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly IVacunaRepository _vacunaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PersistenciaService(
            IPotreroRepository potreroRepository,
            IResRepository resRepository,
            IVentaRepository ventaRepository,
            IVacunaRepository vacunaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _potreroRepository = potreroRepository;
            _resRepository = resRepository;
            _ventaRepository = ventaRepository;
            _vacunaRepository = vacunaRepository;
            _usuarioRepository = usuarioRepository;
        }

        #region Guardar Datos

        public string GuardarPotreros(List<Potrero> potreros)
        {
            return _potreroRepository.GuardarPotreros(potreros);
        }

        public string GuardarReses(List<Potrero> potreros)
        {
            return _resRepository.GuardarReses(potreros);
        }

        public string GuardarVentas(List<Venta> ventas)
        {
            return _ventaRepository.GuardarVentas(ventas);
        }

        public string GuardarVacunas(List<Vacuna> vacunas)
        {
            return _vacunaRepository.GuardarVacunas(vacunas);
        }

        public string GuardarVacunasAplicadas(List<Potrero> potreros)
        {
            return _vacunaRepository.GuardarVacunasAplicadas(potreros);
        }

        public string GuardarUsuarios(List<Usuario> usuarios)
        {
            return _usuarioRepository.GuardarUsuarios(usuarios);
        }

        #endregion

        #region Cargar Datos

        public List<Potrero> CargarPotreros()
        {
            return _potreroRepository.CargarPotreros();
        }

        public void CargarReses(List<Potrero> potreros)
        {
            _resRepository.CargarReses(potreros);
        }

        public List<Venta> CargarVentas(List<Potrero> potreros)
        {
            return _ventaRepository.CargarVentas(potreros);
        }

        public List<Vacuna> CargarVacunas()
        {
            return _vacunaRepository.CargarVacunas();
        }

        public void CargarVacunasAplicadas(List<Potrero> potreros)
        {
            _vacunaRepository.CargarVacunasAplicadas(potreros);
        }

        public List<Usuario> CargarUsuarios()
        {
            return _usuarioRepository.CargarUsuarios();
        }

        #endregion
    }
}