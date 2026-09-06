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
    public class PublisherPesoVenta
    {
        //Definicion del delegado y el evento
        public delegate void dele_peso_venta(string peso_venta);
        public event dele_peso_venta evt_peso_venta;

        private readonly SelectorEstrategiaTipoRes selector;

        public PublisherPesoVenta() : this(SelectorEstrategiaTipoRes.CrearPredeterminado())
        {
        }

        public PublisherPesoVenta(SelectorEstrategiaTipoRes selector)
        {
            this.selector = selector;
        }

        //Metodo para informar si la res está apta para la venta
        public void Informar_Peso_Venta(Res res)
        {
            try
            {
                    IEstrategiaTipoRes? estrategia = selector.Para(res);
                    ushort peso_apto = estrategia != null ? estrategia.PesoRecomendadoVenta : (ushort)0;

                    //Informar si la res está apta para la venta
                    if (res.Peso >= peso_apto)
                    {
                        string mensaje = $"[Evento] La res '{res.Nombre}' tiene un peso {res.Peso}, apta para venta.";

                        if (evt_peso_venta != null)
                        {
                            evt_peso_venta(mensaje);
                        }
                        else
                        {
                            // Si no hay suscriptores, solo no hacer nada (el evento es opcional)
                        }
                    }
                
            }
            catch (Exception er)
            {
                throw new Exception("Error inesperado en el metodo Informar_Peso_Venta: " + er.Message);
            }
        }
    }
}
