using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Estrategias
{
    public class EstrategiaTernero : IEstrategiaTipoRes
    {
        public bool AplicaA(Res res) => res is Ternero;
        public ushort PesoMinimo => ReglaRes.peso_min_ternero;
        public ushort PesoRecomendadoVenta => ReglaRes.peso_recom_venta_ternero;
        public byte MaxBacterianas => ReglaVacuna.max_bac_ternero;
        public byte MaxVivas => ReglaVacuna.max_viv_ternero;
    }
}
