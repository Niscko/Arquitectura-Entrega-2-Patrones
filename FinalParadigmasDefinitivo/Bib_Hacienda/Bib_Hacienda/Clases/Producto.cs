using System;

namespace Bib_Hacienda.Clases
{
    public enum TipoProducto
    {
        Carne,
        Leche,
        Piel,
        Otro
    }

    /// <summary>
    /// Entidad Producto (UML - Imagen 2).
    /// Representa un producto derivado vendido en la hacienda.
    /// </summary>
    public class Producto
    {
        public TipoProducto Tipo { get; set; }
        public double Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public Producto(TipoProducto tipo, double cantidad, decimal precioUnitario)
        {
            Tipo = tipo;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
