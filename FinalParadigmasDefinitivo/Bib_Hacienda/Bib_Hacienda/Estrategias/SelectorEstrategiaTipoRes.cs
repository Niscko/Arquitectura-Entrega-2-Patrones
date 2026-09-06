using System.Collections.Generic;
using System.Linq;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace Bib_Hacienda.Estrategias
{
    /// <summary>
    /// Elige la estrategia cuyo AplicaA coincide. No hay switch por tipo.
    /// Un tipo nuevo se cubre creando la estrategia y registrándola aquí en la composición.
    /// </summary>
    public class SelectorEstrategiaTipoRes
    {
        private readonly IReadOnlyList<IEstrategiaTipoRes> estrategias;

        public SelectorEstrategiaTipoRes(params IEstrategiaTipoRes[] estrategias)
        {
            this.estrategias = estrategias;
        }

        public static SelectorEstrategiaTipoRes CrearPredeterminado()
        {
            return new SelectorEstrategiaTipoRes(
                new EstrategiaTernero(),
                new EstrategiaCebon(),
                new EstrategiaNovillo());
        }

        public IEstrategiaTipoRes? Para(Res res)
        {
            return estrategias.FirstOrDefault(e => e.AplicaA(res));
        }
    }
}
