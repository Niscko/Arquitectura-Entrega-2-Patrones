using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios
{
    /// <summary>
    /// Servicio de geolocalización (UML - Imagen 1 - SRP).
    /// Contiene método ActualizarUbicacion(Chip chip).
    /// </summary>
    public class GeolocalizacionService : IGeolocalizacionService
    {
        public void AsignarChip(Res res, string chipId)
        {
            res.Chip = new Chip(chipId);
        }

        public void ActualizarUbicacion(string chipId, double latitud, double longitud)
        {
        }

        public void ActualizarUbicacion(Chip chip)
        {
            if (chip != null)
            {
                chip.UltimaLectura = System.DateTime.Now;
            }
        }

        public Chip ObtenerChip(Res res)
        {
            return res.Chip;
        }
    }
}
