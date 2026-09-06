using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Aspectos;
using Bib_Hacienda.Interfaces;
using Castle.DynamicProxy;
using System.Globalization;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para persistencia de ventas (ADR-03 — SRP/DIP).
    /// </summary>
    public class VentaRepositoryTxt : IVentaRepository
    {
        private readonly IAlmacenamiento<Venta> _almacenamiento;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VentaRepositoryTxt(IAlmacenamiento<Venta> almacenamiento, IHttpContextAccessor httpContextAccessor)
        {
            _almacenamiento = almacenamiento;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Venta> CargarVentas(List<Potrero> potreros)
        {
            try
            {
                var lineas = _almacenamiento.CargarLineas();
                var ventas = new List<Venta>();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 7)
                    {
                        string nombrePotrero = partes[0].Trim();
                        if (!DateTime.TryParseExact(partes[1].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                        {
                            continue;
                        }
                        string resNombre = partes[2];
                        uint resPeso = uint.TryParse(partes[3], out var p) ? p : 0;
                        ushort resEdad = ushort.TryParse(partes[4], out var e) ? e : (ushort)0;
                        string resTipo = partes[5];
                        uint monto = uint.TryParse(partes[6], out var m) ? m : 0;

                        var potrero = potreros.FirstOrDefault(pot =>
                            string.Equals(pot.Identificacion, nombrePotrero, StringComparison.OrdinalIgnoreCase));

                        if (potrero == null)
                        {
                            potrero = new Potrero(nombrePotrero, Potrero.l_tipos_potreros.ternero);
                        }

                        Res res = resTipo switch
                        {
                            "Ternero" => new Ternero(resNombre, resPeso, resEdad),
                            "Novillo" => new Novillo(resNombre, resPeso, resEdad),
                            "Cebon" => new Cebon(resNombre, resPeso, resEdad),
                            _ => new Ternero(resNombre, resPeso, resEdad)
                        };

                        ventas.Add(new Venta(potrero, fecha, res, monto));
                    }
                }

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar ventas: {ex.Message}");
            }
        }

        public string GuardarVentas(List<Venta> ventas)
        {
            try
            {
                var interceptor = new InterceptorValidarInformacion(_httpContextAccessor);
                var proxyGenerator = new ProxyGenerator();
                var validadorProxy = proxyGenerator.CreateClassProxy<ValidadorVenta>(interceptor);

                bool esValida;
                foreach (var venta in ventas)
                {
                    esValida = validadorProxy.ValidarVenta(venta);
                    if (!esValida)
                    {
                        var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                        return mensaje ?? "Error de validación en venta";
                    }
                }

                var lineas = new List<string>();
                foreach (var venta in ventas)
                {
                    string fecha = venta.Fecha.ToString("yyyy-MM-dd");
                    string tipoRes = venta.Res.GetType().Name;
                    lineas.Add($"{venta.Potrero.Identificacion}|{fecha}|{venta.Res.Nombre}|{venta.Res.Peso}|{venta.Res.Edad}|{tipoRes}|{venta.Monto}");
                }

                _almacenamiento.GuardarLineas(lineas);

                return _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString()
                    ?? "Guardado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar ventas: {ex.Message}", ex);
            }
        }
    }
}
