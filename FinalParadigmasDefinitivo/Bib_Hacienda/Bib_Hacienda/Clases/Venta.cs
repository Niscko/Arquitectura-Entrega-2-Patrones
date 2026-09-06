using System;
using System.Collections.Generic;
using System.Linq;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Entidad Venta (UML - Imagen 2).
    /// Posee una lista de ItemVenta (+ items: List<ItemVenta>) y método Total() (+ total(): decimal).
    /// </summary>
    public class Venta
    {
        private Potrero potrero;
        private DateTime fecha;
        private Res res;
        private uint monto;
        private List<ItemVenta> items;

        public Venta(Potrero potrero, DateTime fecha, Res res, uint monto)
        {
            this.Potrero = potrero;
            this.Fecha = fecha;
            this.Res = res;
            this.Monto = monto;
            this.items = new List<ItemVenta> { new ResItem(res, monto) };
        }

        public Venta(Potrero potrero, DateTime fecha, List<ItemVenta> items)
        {
            this.Potrero = potrero;
            this.Fecha = fecha;
            this.items = items ?? new List<ItemVenta>();
            this.Monto = (uint)this.items.Sum(i => (long)i.Monto);
            var primerResItem = this.items.OfType<ResItem>().FirstOrDefault();
            this.Res = primerResItem?.Res;
        }

        public Potrero Potrero { get => potrero; set => potrero = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public Res Res { get => res; set => res = value; }
        public uint Monto { get => monto; set => monto = value; }
        public List<ItemVenta> Items { get => items; set => items = value; }

        public decimal Total()
        {
            if (items != null && items.Count > 0)
            {
                return items.Sum(i => (decimal)i.Monto);
            }
            return (decimal)monto;
        }
    }
}
