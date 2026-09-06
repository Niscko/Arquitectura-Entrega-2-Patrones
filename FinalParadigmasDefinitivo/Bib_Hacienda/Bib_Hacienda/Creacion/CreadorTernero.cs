using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Creacion
{
    public class CreadorTernero : CreadorRes
    {
        public override byte EdadMinima => 0;
        public override byte EdadMaxima => ReglaRes.edad_max_ternero;

        public override Res Crear(string nombre, ushort edad, uint peso)
        {
            return new Ternero(nombre, peso, edad);
        }
    }
}
