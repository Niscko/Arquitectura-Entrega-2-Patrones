using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para historias clínicas (UML - Imagen 1 - DIP).
    /// </summary>
    public class HistoriaClinicaRepositoryTxt : IHistoriaClinicaRepository
    {
        private readonly IAlmacenamiento<HistoriaClinica> _almacenamiento;

        public HistoriaClinicaRepositoryTxt(IAlmacenamiento<HistoriaClinica> almacenamiento)
        {
            _almacenamiento = almacenamiento;
        }

        public HistoriaClinica CargarHistoriaClinica(string resNombre)
        {
            var lineas = _almacenamiento.CargarLineas();
            var historia = new HistoriaClinica();

            foreach (var linea in lineas)
            {
                var partes = linea.Split('|');
                if (partes.Length >= 5 && string.Equals(partes[0].Trim(), resNombre, StringComparison.OrdinalIgnoreCase))
                {
                    string descripcion = partes[1];
                    if (DateTime.TryParse(partes[2], out var fecha))
                    {
                        string tipo = partes[3];
                        string vet = partes[4];
                        string res = partes.Length > 5 ? partes[5] : "";

                        historia.RegistrarEvento(new EventoClinico(descripcion, fecha, tipo, vet, res));
                    }
                }
            }

            return historia;
        }

        public void GuardarHistoriaClinica(string resNombre, HistoriaClinica historia)
        {
            if (historia == null) return;

            var lineas = new List<string>();
            foreach (var evt in historia.Eventos)
            {
                lineas.Add($"{resNombre}|{evt.Descripcion}|{evt.Fecha:yyyy-MM-dd}|{evt.Tipo}|{evt.Veterinario}|{evt.Resultado}");
            }

            _almacenamiento.GuardarLineas(lineas);
        }
    }
}
