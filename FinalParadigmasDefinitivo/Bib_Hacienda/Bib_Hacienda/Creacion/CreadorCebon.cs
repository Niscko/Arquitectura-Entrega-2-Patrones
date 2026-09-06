using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Creacion
{
    public class CreadorCebon : CreadorRes
    {
        public override byte EdadMinima => (byte)(ReglaRes.edad_max_ternero + 1);
        public override byte EdadMaxima => ReglaRes.edad_max_cebon;

        public override Res Crear(string nombre, ushort edad, uint peso)
        {
            return new Cebon(nombre, peso, edad);
        }
    }
}
