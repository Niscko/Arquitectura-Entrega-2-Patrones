using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Strategy (P-02 / P-03): reglas que varían según el tipo de res.
    /// Cada implementación concreta aplica a un solo tipo; no contiene cascadas.
    /// </summary>
    public interface IEstrategiaTipoRes
    {
        bool AplicaA(Res res);
        ushort PesoMinimo { get; }
        ushort PesoRecomendadoVenta { get; }
        byte MaxBacterianas { get; }
        byte MaxVivas { get; }
    }
}
