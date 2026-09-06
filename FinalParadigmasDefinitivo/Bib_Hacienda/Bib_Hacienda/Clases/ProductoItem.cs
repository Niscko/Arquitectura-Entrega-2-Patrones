using System;

namespace Bib_Hacienda.Clases
{
    /// <summary>
    /// Implementación de ItemVenta para productos derivados (UML - Imagen 2 - OCP/LSP).
    /// Contiene una referencia a la clase Producto (+ producto: Producto).
    /// </summary>
    public class ProductoItem : ItemVenta
    {
        public Producto Producto { get; set; }

        public ProductoItem(Producto producto) : base((uint)(producto.PrecioUnitario * (decimal)producto.Cantidad))
        {
            Producto = producto;
        }

        public ProductoItem(Producto producto, uint monto) : base(monto)
        {
            Producto = producto;
        }

        public override string Describir()
        {
            return $"Producto: {Producto.Tipo} (Cantidad: {Producto.Cantidad}, Precio Unitario: {Producto.PrecioUnitario:C})";
        }
    }
}
