using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Aspectos;
using Bib_Hacienda.Interfaces;
using Castle.DynamicProxy;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para persistencia de reses (ADR-03 — SRP/DIP).
    /// </summary>
    public class ResRepositoryTxt : IResRepository
    {
        private readonly IAlmacenamiento<Res> _almacenamiento;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResRepositoryTxt(IAlmacenamiento<Res> almacenamiento, IHttpContextAccessor httpContextAccessor)
        {
            _almacenamiento = almacenamiento;
            _httpContextAccessor = httpContextAccessor;
        }

        public void CargarReses(List<Potrero> potreros)
        {
            try
            {
                var lineas = _almacenamiento.CargarLineas();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 5)
                    {
                        string nombrePotrero = partes[0];
                        string nombre = partes[1];
                        uint peso = uint.TryParse(partes[2], out var p) ? p : 0;
                        ushort edad = ushort.TryParse(partes[3], out var e) ? e : (ushort)0;
                        string tipo = partes[4];

                        var potrero = potreros.FirstOrDefault(pot =>
                            string.Equals(pot.Identificacion, nombrePotrero, StringComparison.OrdinalIgnoreCase));

                        if (potrero != null)
                        {
                            Res res = tipo switch
                            {
                                "Ternero" => new Ternero(nombre, peso, edad),
                                "Novillo" => new Novillo(nombre, peso, edad),
                                "Cebon" => new Cebon(nombre, peso, edad),
                                _ => new Ternero(nombre, peso, edad)
                            };

                            potrero.L_reses.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar reses: {ex.Message}");
            }
        }

        public string GuardarReses(List<Potrero> potreros)
        {
            try
            {
                var interceptor = new InterceptorValidarInformacion(_httpContextAccessor);
                var proxyGenerator = new ProxyGenerator();
                var validadorProxy = proxyGenerator.CreateClassProxy<ValidadorRes>(interceptor);

                var lineas = new List<string>();
                bool esValida;

                foreach (var potrero in potreros)
                {
                    foreach (var res in potrero.L_reses)
                    {
                        esValida = validadorProxy.ValidarRes(res);
                        if (!esValida)
                        {
                            var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                            return mensaje ?? "Error de validación en res";
                        }

                        string tipoRes = res.GetType().Name;
                        lineas.Add($"{potrero.Identificacion}|{res.Nombre}|{res.Peso}|{res.Edad}|{tipoRes}");
                    }
                }

                _almacenamiento.GuardarLineas(lineas);

                return _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString()
                    ?? "Guardado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar reses: {ex.Message}", ex);
            }
        }
    }
}
