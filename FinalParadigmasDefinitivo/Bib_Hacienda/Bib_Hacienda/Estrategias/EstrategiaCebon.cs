using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Estrategias
{
    public class EstrategiaCebon : IEstrategiaTipoRes
    {
        public bool AplicaA(Res res) => res is Cebon;
        public ushort PesoMinimo => ReglaRes.peso_min_cebon;
        public ushort PesoRecomendadoVenta => ReglaRes.peso_recom_venta_cebon;
        public byte MaxBacterianas => ReglaVacuna.max_bac_cebon;
        public byte MaxVivas => ReglaVacuna.max_viv_cebon;
    }
}
