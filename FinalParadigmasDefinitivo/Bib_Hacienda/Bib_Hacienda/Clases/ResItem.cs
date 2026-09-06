using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Implementación de ItemVenta para ventas de reses vivas (ADR-05 — OCP/LSP).
    /// Mantiene compatibilidad con el modelo actual de Venta.
    /// Precondiciones: no exige condiciones adicionales respecto a ItemVenta.
    /// Postcondiciones: Monto >= 0, Describir() retorna descripción no nula.
    /// </summary>
    public class ResItem : ItemVenta
    {
        //Atributos
        private Res res;

        //Constructor
        public ResItem(Res res, uint monto) : base(monto)
        {
            this.Res = res;
        }

        //Accesores
        public Res Res { get => res; set => res = value; }

        //Implementación de Describir
        public override string Describir()
        {
            return $"Res: {res.Nombre} (Peso: {res.Peso} kg, Edad: {res.Edad} meses)";
        }
    }
}
