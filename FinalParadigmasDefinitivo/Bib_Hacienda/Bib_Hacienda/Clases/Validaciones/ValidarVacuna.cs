using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Valida objetos de tipo Vacuna (ADR-04 — ISP).
    /// Implementa solo IValidador&lt;Vacuna&gt;, no métodos de otras entidades.
    /// Eliminadas las excepciones NotImplementedException.
    /// </summary>
    public class ValidadorVacuna : Validacion<Vacuna>
    {
        public override bool Validar(Vacuna vacuna)
        {
            if (vacuna == null || string.IsNullOrWhiteSpace(vacuna.Nombre) || string.IsNullOrWhiteSpace(vacuna.Lote))
            {
                return false;
            }
            return true;
        }

        //Método de compatibilidad con el nombre original para el interceptor
        public virtual bool ValidarVacuna(Vacuna vacuna)
        {
            return Validar(vacuna);
        }
    }
}
