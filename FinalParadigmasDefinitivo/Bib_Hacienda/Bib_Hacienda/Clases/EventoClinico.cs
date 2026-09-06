using System;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Representa un evento clínico individual (UML - Imagen 2).
    /// Contiene fecha, tipo, veterinario, resultado y descripción.
    /// </summary>
    public class EventoClinico
    {
        private string descripcion;
        private DateTime fecha;
        private string tipo;
        private string veterinario;
        private string resultado;

        public EventoClinico(string descripcion, DateTime fecha, string tipo, string veterinario = "", string resultado = "")
        {
            this.Descripcion = descripcion;
            this.Fecha = fecha;
            this.Tipo = tipo;
            this.Veterinario = veterinario;
            this.Resultado = resultado;
        }

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Veterinario { get => veterinario; set => veterinario = value; }
        public string Resultado { get => resultado; set => resultado = value; }
    }
}
