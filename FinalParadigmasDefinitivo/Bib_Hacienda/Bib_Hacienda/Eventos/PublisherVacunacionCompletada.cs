using Bib_Hacienda.Clases;
using Bib_Hacienda.Estrategias;
using Bib_Hacienda.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Eventos
{
    public class PublisherVacunacionCompletada
    {
        //Definicion del delegado y el evento (con mensaje)
        public delegate void dele_vacunacion_completada(string mensaje);
        public event dele_vacunacion_completada evt_vacunacion_completada;

        private readonly SelectorEstrategiaTipoRes selector;

        public PublisherVacunacionCompletada() : this(SelectorEstrategiaTipoRes.CrearPredeterminado())
        {
        }

        public PublisherVacunacionCompletada(SelectorEstrategiaTipoRes selector)
        {
            this.selector = selector;
        }

        //Metodo para informar que una res ha completado su esquema de vacunacion
        public bool Informar_Vacunacion_Completada(Res res, ushort contador_bacterianas, ushort contador_vivas)
        {
            try
            {
                if (res == null)
                {
                    throw new ArgumentNullException(nameof(res), "La res no puede ser null");
                }

                IEstrategiaTipoRes? estrategia = selector.Para(res);
                bool esquema_completo = estrategia != null
                    && contador_bacterianas >= estrategia.MaxBacterianas
                    && contador_vivas >= estrategia.MaxVivas;

                // Disparar el evento con el mensaje apropiado
                if (evt_vacunacion_completada != null)
                {
                    string mensaje;
                    if (esquema_completo)
                    {
                        mensaje = $"[Evento] La res '{res.Nombre}' ha completado su esquema de vacunación.";
                    }
                    else
                    {
                        mensaje = $"[Evento] La res '{res.Nombre}' aún no ha completado su esquema de vacunación. Bacterianas: {contador_bacterianas}, Vivas: {contador_vivas}";
                    }
                    evt_vacunacion_completada(mensaje);
                }

                return esquema_completo;
            }
            catch (Exception er)
            {
                throw new Exception("[evento] Error inesperado en el metodo Informar_Vacunacion_Completada: " + er.Message);
            }
        }
    }
}
