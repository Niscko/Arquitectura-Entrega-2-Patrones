using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Valida objetos de tipo Res (ADR-04 — ISP).
    /// Implementa solo IValidador&lt;Res&gt;, no métodos de otras entidades.
    /// Eliminadas las excepciones NotImplementedException.
    /// </summary>
    public class ValidadorRes : Validacion<Res>
    {
        public override bool Validar(Res res)
        {
            if (res == null || string.IsNullOrWhiteSpace(res.Nombre) || res.Peso <= 0 || res.Edad <= 0)
            {
                return false;
            }
            return true;
        }

        //Método de compatibilidad con el nombre original para el interceptor
        public virtual bool ValidarRes(Res res)
        {
            return Validar(res);
        }
    }
}
