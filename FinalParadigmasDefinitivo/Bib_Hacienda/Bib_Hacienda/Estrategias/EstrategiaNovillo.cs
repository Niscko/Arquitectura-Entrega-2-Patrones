using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Estrategias
{
    public class EstrategiaNovillo : IEstrategiaTipoRes
    {
        public bool AplicaA(Res res) => res is Novillo;
        public ushort PesoMinimo => ReglaRes.peso_min_novillo;
        public ushort PesoRecomendadoVenta => ReglaRes.peso_recom_venta_novillo;
        public byte MaxBacterianas => ReglaVacuna.max_bac_novillo;
        public byte MaxVivas => ReglaVacuna.max_viv_novillo;
    }
}
