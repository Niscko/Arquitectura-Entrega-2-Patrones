using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Objeto de valor que representa un chip de geolocalización (ADR-02/08).
    /// Se compone dentro de Res como atributo opcional (0..1).
    /// SRP: Res no absorbe responsabilidades de rastreo GPS.
    /// OCP: Nuevas capacidades de rastreo se agregan aquí, no en Res.
    /// </summary>
    public class Chip
    {
        //Atributos
        private string chipId;
        private double latitud;
        private double longitud;
        private DateTime ultimaLectura;

        //Constructor
        public Chip(string chipId)
        {
            this.ChipId = chipId;
            this.Latitud = 0;
            this.Longitud = 0;
            this.UltimaLectura = DateTime.Now;
        }

        //Constructor completo
        public Chip(string chipId, double latitud, double longitud, DateTime ultimaLectura)
        {
            this.ChipId = chipId;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.UltimaLectura = ultimaLectura;
        }

        //Accesores
        public string ChipId { get => chipId; set => chipId = value; }
        public double Latitud { get => latitud; set => latitud = value; }
        public double Longitud { get => longitud; set => longitud = value; }
        public DateTime UltimaLectura { get => ultimaLectura; set => ultimaLectura = value; }
    }
}
