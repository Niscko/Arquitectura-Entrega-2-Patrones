using System;
using System.Collections.Generic;
using static Bib_Hacienda.Clases.Potrero;

namespace Bib_Hacienda.Creacion
{
    /// <summary>
    /// Punto de composición del Factory Method.
    /// Un tipo nuevo se agrega registrando su CreadorRes, sin tocar Potrero.anadir_res.
    /// </summary>
    public static class CatalogoCreadoresRes
    {
        private static readonly Dictionary<l_tipos_potreros, CreadorRes> registro =
            new Dictionary<l_tipos_potreros, CreadorRes>
            {
                { l_tipos_potreros.ternero, new CreadorTernero() },
                { l_tipos_potreros.cebon, new CreadorCebon() },
                { l_tipos_potreros.novillo, new CreadorNovillo() }
            };

        public static CreadorRes De(l_tipos_potreros tipo)
        {
            if (!registro.TryGetValue(tipo, out CreadorRes creador))
            {
                throw new ArgumentException($"No hay un creador registrado para el tipo de potrero '{tipo}'.");
            }
            return creador;
        }
    }
}
