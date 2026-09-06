using System;
using System.Collections.Generic;
using System.Linq;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Construccion
{
    /// <summary>
    /// Builder concreto de Vacuna (P-04). Reúne parámetros y ejecuta las
    /// validaciones comunes una sola vez. No absorbe la lógica de lotes.
    /// </summary>
    public class VacunaBuilder
    {
        private string nombre = "";
        private string lote = "";
        private DateTime fecha_aplicacion;
        private DateTime fecha_vencimiento;
        private uint? periodo_aplicacion;
        private Viva.enum_l_atenuaciones? grado_atenuacion;
        private IEnumerable<Vacuna> inventario = Enumerable.Empty<Vacuna>();
        private bool verificar_lote_unico = true;

        public VacunaBuilder ConNombre(string nombre)
        {
            this.nombre = nombre;
            return this;
        }

        public VacunaBuilder ConLote(string lote)
        {
            this.lote = lote;
            return this;
        }

        public VacunaBuilder ConFechas(DateTime fecha_aplicacion, DateTime fecha_vencimiento)
        {
            this.fecha_aplicacion = fecha_aplicacion;
            this.fecha_vencimiento = fecha_vencimiento;
            return this;
        }

        public VacunaBuilder ContraInventario(IEnumerable<Vacuna> inventario)
        {
            this.inventario = inventario ?? Enumerable.Empty<Vacuna>();
            return this;
        }

        public VacunaBuilder ComoBacteriana(uint periodo_aplicacion)
        {
            this.periodo_aplicacion = periodo_aplicacion;
            this.grado_atenuacion = null;
            return this;
        }

        public VacunaBuilder ComoViva(Viva.enum_l_atenuaciones grado_atenuacion)
        {
            this.grado_atenuacion = grado_atenuacion;
            this.periodo_aplicacion = null;
            return this;
        }

        public VacunaBuilder SinVerificarLoteUnico()
        {
            this.verificar_lote_unico = false;
            return this;
        }

        public VacunaBuilder ValidarParametros()
        {
            ValidarComun();
            return this;
        }

        public Vacuna Construir()
        {
            ValidarComun();
            if (periodo_aplicacion.HasValue)
            {
                return new Bacteriana(nombre, lote, fecha_vencimiento, fecha_aplicacion, periodo_aplicacion.Value);
            }
            if (grado_atenuacion.HasValue)
            {
                return new Viva(nombre, lote, fecha_vencimiento, fecha_aplicacion, grado_atenuacion.Value);
            }
            throw new InvalidOperationException("Debe indicar si la vacuna es bacteriana o viva.");
        }

        private void ValidarComun()
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la vacuna no puede estar vacío", nameof(nombre));

            if (string.IsNullOrWhiteSpace(lote))
                throw new ArgumentException("El lote de la vacuna no puede estar vacío", nameof(lote));

            if (fecha_vencimiento <= fecha_aplicacion)
                throw new Exception("La fecha de vencimiento debe ser posterior a la fecha de aplicación");

            if (verificar_lote_unico && inventario.Any(v => v.Lote.Equals(lote, StringComparison.OrdinalIgnoreCase)))
                throw new Exception($"Ya existe una vacuna con el lote '{lote}' en el inventario");
        }
    }
}
