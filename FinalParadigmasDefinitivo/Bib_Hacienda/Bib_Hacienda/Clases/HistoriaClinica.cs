using System;
using System.Collections.Generic;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Objeto de valor que representa la historia clínica de una res (UML - Imagen 2).
    /// </summary>
    public class HistoriaClinica
    {
        private List<EventoClinico> eventos;

        public HistoriaClinica()
        {
            this.eventos = new List<EventoClinico>();
        }

        public List<EventoClinico> Eventos { get => eventos; set => eventos = value; }

        public void RegistrarEvento(EventoClinico evento)
        {
            eventos.Add(evento);
        }

        public void AgregarEvento(EventoClinico evento)
        {
            RegistrarEvento(evento);
        }

        public List<EventoClinico> ObtenerEventos()
        {
            return new List<EventoClinico>(eventos);
        }
    }
}
