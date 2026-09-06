using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Clase abstracta que representa un ítem vendible (ADR-05 — OCP/LSP).
    /// Venta se vuelve agnóstica al tipo de ítem vendido.
    /// Nuevos tipos de producto se agregan como subclases sin modificar Venta.
    /// ResItem y ProductoItem son sustituibles por ItemVenta (LSP verificado).
    /// </summary>
    public abstract class ItemVenta
    {
        //Atributos
        private uint monto;

        //Constructor
        protected ItemVenta(uint monto)
        {
            this.Monto = monto;
        }

        //Accesores
        public uint Monto { get => monto; set => monto = value; }

        //Métodos abstractos
        public abstract string Describir();
    }
}
