using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de historia clínica (UML - Imagen 1 - SRP/DIP).
    /// Contiene registrarEvento y consultarHistoria.
    /// </summary>
    public interface IHistoriaClinicaService
    {
        void RegistrarEventoClinico(Res res, EventoClinico evento);
        HistoriaClinica ConsultarHistoria(Res res);
        HistoriaClinica ObtenerHistoriaClinica(Res res);
    }
}
