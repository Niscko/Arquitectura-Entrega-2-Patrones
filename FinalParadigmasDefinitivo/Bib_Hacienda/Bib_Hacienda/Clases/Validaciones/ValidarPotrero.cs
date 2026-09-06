using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Valida objetos de tipo Potrero (ADR-04 — ISP).
    /// Implementa solo IValidador&lt;Potrero&gt;, no métodos de otras entidades.
    /// Eliminadas las excepciones NotImplementedException.
    /// </summary>
    public class ValidadorPotrero : Validacion<Potrero>
    {
        public override bool Validar(Potrero potrero)
        {
            if (potrero == null || string.IsNullOrWhiteSpace(potrero.Identificacion))
            {
                return false;
            }
            return true;
        }

        //Método de compatibilidad con el nombre original para el interceptor
        public virtual bool ValidarPotrero(Potrero potrero)
        {
            return Validar(potrero);
        }
    }
}
