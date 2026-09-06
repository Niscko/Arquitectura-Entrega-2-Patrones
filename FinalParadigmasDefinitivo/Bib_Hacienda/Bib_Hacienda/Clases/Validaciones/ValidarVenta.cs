using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Valida objetos de tipo Venta (ADR-04 — ISP).
    /// Implementa solo IValidador&lt;Venta&gt;, no métodos de otras entidades.
    /// Eliminadas las excepciones NotImplementedException.
    /// </summary>
    public class ValidadorVenta : Validacion<Venta>
    {
        public override bool Validar(Venta venta)
        {
            if (venta == null || venta.Potrero == null || venta.Res == null || venta.Monto <= 0)
            {
                return false;
            }
            return true;
        }

        //Método de compatibilidad con el nombre original para el interceptor
        public virtual bool ValidarVenta(Venta venta)
        {
            return Validar(venta);
        }
    }
}
