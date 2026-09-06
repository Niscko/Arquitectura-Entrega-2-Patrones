using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios
{
    /// <summary>
    /// Servicio de historia clínica (UML - Imagen 1 - SRP/DIP).
    /// </summary>
    public class HistoriaClinicaService : IHistoriaClinicaService
    {
        private readonly IHistoriaClinicaRepository _repository;

        public HistoriaClinicaService(IHistoriaClinicaRepository repository)
        {
            _repository = repository;
        }

        public HistoriaClinicaService()
        {
        }

        public void RegistrarEventoClinico(Res res, EventoClinico evento)
        {
            if (res.HistoriaClinica == null)
            {
                res.HistoriaClinica = new HistoriaClinica();
            }
            res.HistoriaClinica.RegistrarEvento(evento);

            if (_repository != null)
            {
                _repository.GuardarHistoriaClinica(res.Nombre, res.HistoriaClinica);
            }
        }

        public HistoriaClinica ConsultarHistoria(Res res)
        {
            if (res.HistoriaClinica != null)
            {
                return res.HistoriaClinica;
            }

            if (_repository != null)
            {
                res.HistoriaClinica = _repository.CargarHistoriaClinica(res.Nombre);
                return res.HistoriaClinica;
            }

            return new HistoriaClinica();
        }

        public HistoriaClinica ObtenerHistoriaClinica(Res res)
        {
            return ConsultarHistoria(res);
        }
    }
}
