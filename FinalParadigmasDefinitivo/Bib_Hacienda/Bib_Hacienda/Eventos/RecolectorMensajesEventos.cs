using System.Collections.Generic;
using System.Linq;

namespace Bib_Hacienda.Eventos
{
    /// <summary>
    /// Observador concreto (P-01). Se suscribe una sola vez y acumula mensajes
    /// de la operación en curso. Limpiar() evita que se mezclen operaciones.
    /// </summary>
    public class RecolectorMensajesEventos
    {
        private readonly List<string> mensajes = new List<string>();

        public void Recibir(string mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                mensajes.Add(mensaje);
            }
        }

        public void Limpiar()
        {
            mensajes.Clear();
        }

        public string Texto()
        {
            if (mensajes.Count == 0)
            {
                return "";
            }
            return string.Join("\n", mensajes);
        }
    }
}
