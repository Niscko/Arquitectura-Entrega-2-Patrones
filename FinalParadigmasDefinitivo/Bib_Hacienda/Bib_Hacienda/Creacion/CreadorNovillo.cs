using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Creacion
{
    public class CreadorNovillo : CreadorRes
    {
        public override byte EdadMinima => (byte)(ReglaRes.edad_max_cebon + 1);
        public override byte EdadMaxima => byte.MaxValue;

        public override Res Crear(string nombre, ushort edad, uint peso)
        {
            return new Novillo(nombre, peso, edad);
        }
    }
}
