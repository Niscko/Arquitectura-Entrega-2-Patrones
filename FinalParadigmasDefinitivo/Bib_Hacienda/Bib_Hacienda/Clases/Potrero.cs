using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib_Hacienda.Creacion;
using Bib_Hacienda.Estrategias;
using Bib_Hacienda.Eventos;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Clases
{
    public class Potrero
    {

        //Atributos
        public enum l_tipos_potreros {ternero, novillo, cebon};
        private string identificacion;
        private List<Res> l_reses = new List<Res>();
        private l_tipos_potreros tipo_potrero;
        private CreadorRes creador_res;
        private SelectorEstrategiaTipoRes selector_tipo_res;
        private RecolectorMensajesEventos recolector;

        //Eventos
        private PublisherPotreroMitad publisher_potrero_mitad;
        private PublisherPotreroLleno publisher_potrero_lleno;
        private PublisherPesoVenta publisher_peso_venta;
        private PublisherPesoMin publisher_peso_min;

        //EventHandler
        internal void EventHandler() { }

        //Constructor
        public Potrero(string identificacion, l_tipos_potreros tipo_potrero)
        {
            this.Identificacion = identificacion;
            this.tipo_potrero = tipo_potrero;
            this.creador_res = CatalogoCreadoresRes.De(tipo_potrero);
            this.selector_tipo_res = SelectorEstrategiaTipoRes.CrearPredeterminado();
            this.publisher_potrero_mitad = new PublisherPotreroMitad();
            this.publisher_potrero_lleno = new PublisherPotreroLleno();
            this.publisher_peso_venta = new PublisherPesoVenta(selector_tipo_res);
            this.publisher_peso_min = new PublisherPesoMin(selector_tipo_res);
            this.recolector = new RecolectorMensajesEventos();
            SuscribirEventos();
        }

        private void SuscribirEventos()
        {
            publisher_peso_venta.evt_peso_venta += recolector.Recibir;
            publisher_peso_min.evt_peso_min += recolector.Recibir;
            publisher_potrero_mitad.evt_potrero_mitad += recolector.Recibir;
            publisher_potrero_lleno.evt_potrero_lleno += recolector.Recibir;
        }

        internal void DesuscribirEventos()
        {
            publisher_peso_venta.evt_peso_venta -= recolector.Recibir;
            publisher_peso_min.evt_peso_min -= recolector.Recibir;
            publisher_potrero_mitad.evt_potrero_mitad -= recolector.Recibir;
            publisher_potrero_lleno.evt_potrero_lleno -= recolector.Recibir;
        }

        //Metodo para añadir las reces al potrero
        public string anadir_res(string nombre, ushort edad, uint peso) 
        {
            try
            {
                //Validar parámetros
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    throw new ArgumentException("El nombre de la res no puede estar vacío", nameof(nombre));
                }

                ushort cantidad_reses;
                Res res = null;

                if (l_reses.Count() == ReglaPotrero.max_reses_potrero)
                {
                    //Validacion de potrero lleno
                    throw new Exception($"La res no puede ser añadida al potrero {this.identificacion} porque este está lleno");
                }
                else
                {
                    if (creador_res.EdadPermitida(edad))
                    {
                        res = creador_res.Crear(nombre, edad, peso);
                        l_reses.Add(res);

                        //Cuenta las reses actuales en el potrero
                        cantidad_reses = (ushort)L_reses.Count();

                        recolector.Limpiar();

                        //Disparar los eventos (la suscripción ya está hecha una sola vez)
                        publisher_potrero_mitad.Informar_Potrero_Mitad(cantidad_reses, this);
                        publisher_potrero_lleno.Informar_Potrero_Lleno(cantidad_reses, this);
                        publisher_peso_min.Informar_Peso_Min(res);
                        publisher_peso_venta.Informar_Peso_Venta(res);
                       
                        //Construir mensaje de retorno
                        string mensaje_final = $"La res {nombre} ha sido añadida al potrero {this.identificacion} con exito.";
                        string mensajes_eventos = recolector.Texto();
                        if (!string.IsNullOrEmpty(mensajes_eventos))
                        {
                            mensaje_final += "\n" + mensajes_eventos;
                        }

                        return mensaje_final;

                    }
                    else
                    {
                        throw new Exception($"La res no puede ser añadida al potrero {this.identificacion} porque su edad no corresponde al tipo de potrero");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado en el metodo anadir_res: " + ex.Message);
            }

        }

        //Metodo para buscar res por el nombre
        public Res buscar_res(string nombre)
        {
            try
            {
                // Validar nombre
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    throw new ArgumentException("El nombre de búsqueda no puede estar vacío.");
                }

                // Buscar la res que contengan el texto (ignorando mayúsculas/minúsculas)
                var res_encontrada = l_reses
                    .Where(p => p.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                // Si no hay resultados
                if (res_encontrada.Count == 0)
                {
                    throw new Exception($"No se encontró ningúna vaca con el nombre o coincidencia '{nombre}'.");
                }

                // Si hay más de un resultado, mostrar opciones
                if (res_encontrada.Count > 1)
                {
                    throw new Exception($" se encontró mas de una res con el nombre o coincidencia '{nombre}'.");
                }

                //  devolver potrero
                return res_encontrada.First();
            }
            catch (Exception er)
            {
                throw new Exception("Error inesperado en el método buscar_potrero: " + er.Message);
            }
        }

        //Accesores
        public List<Res> L_reses { get => l_reses; set => l_reses = value; }
        public string Identificacion { get => identificacion; set => identificacion = value; }
        public l_tipos_potreros Tipo_potrero { get => tipo_potrero; set => tipo_potrero = value; }

    }
}
