using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Creacion
{
    /// <summary>
    /// Creador abstracto del Factory Method (P-05).
    /// Cada creador concreto instancia un tipo de Res y conoce el rango de edad de su producto.
    /// </summary>
    public abstract class CreadorRes
    {
        public abstract byte EdadMinima { get; }
        public abstract byte EdadMaxima { get; }

        public abstract Res Crear(string nombre, ushort edad, uint peso);

        public bool EdadPermitida(ushort edad)
        {
            return edad >= EdadMinima && edad <= EdadMaxima;
        }
    }
}
