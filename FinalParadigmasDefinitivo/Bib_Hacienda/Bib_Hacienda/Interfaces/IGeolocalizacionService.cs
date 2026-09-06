using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de geolocalización (UML - Imagen 1 - SRP).
    /// </summary>
    public interface IGeolocalizacionService
    {
        void AsignarChip(Res res, string chipId);
        void ActualizarUbicacion(string chipId, double latitud, double longitud);
        void ActualizarUbicacion(Chip chip);
        Chip ObtenerChip(Res res);
    }
}
