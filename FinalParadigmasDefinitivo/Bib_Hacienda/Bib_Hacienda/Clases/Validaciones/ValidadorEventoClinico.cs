using Bib_Hacienda.Interfaces;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Validador de EventoClinico (UML - Imagen 1 - ISP).
    /// Implementa IValidador&lt;EventoClinico&gt;.
    /// </summary>
    public class ValidadorEventoClinico : Validacion<EventoClinico>
    {
        public override bool Validar(EventoClinico evento)
        {
            if (evento == null || string.IsNullOrWhiteSpace(evento.Descripcion))
            {
                return false;
            }
            return true;
        }

        public virtual bool ValidarEventoClinico(EventoClinico evento)
        {
            return Validar(evento);
        }
    }
}
