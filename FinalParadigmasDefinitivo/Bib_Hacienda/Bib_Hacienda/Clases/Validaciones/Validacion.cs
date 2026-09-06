using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Clase base abstracta para validaciones (ADR-04 — ISP).
    /// Conservada como clase base abstracta genérica.
    /// Ahora implementa IValidador&lt;T&gt; en vez de IValidarInformacion.
    /// Se mantiene por retrocompatibilidad con el interceptor existente.
    /// </summary>
    public abstract class Validacion<T> : IValidador<T>
    {
        //Método abstracto que las clases hijas implementan para su entidad específica
        public abstract bool Validar(T entidad);
    }
}
