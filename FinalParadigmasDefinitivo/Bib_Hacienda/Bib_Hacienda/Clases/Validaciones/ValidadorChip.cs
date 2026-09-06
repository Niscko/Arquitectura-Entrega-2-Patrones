using Bib_Hacienda.Interfaces;

namespace Bib_Hacienda.Clases.Validaciones
{
    /// <summary>
    /// Validador de Chip (UML - Imagen 1 - ISP).
    /// Implementa IValidador&lt;Chip&gt;.
    /// </summary>
    public class ValidadorChip : Validacion<Chip>
    {
        public override bool Validar(Chip chip)
        {
            if (chip == null || string.IsNullOrWhiteSpace(chip.ChipId))
            {
                return false;
            }
            return true;
        }

        public virtual bool ValidarChip(Chip chip)
        {
            return Validar(chip);
        }
    }
}
