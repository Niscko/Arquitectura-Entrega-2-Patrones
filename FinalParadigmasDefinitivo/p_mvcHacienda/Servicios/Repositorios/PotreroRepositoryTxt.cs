using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Aspectos;
using Bib_Hacienda.Interfaces;
using Castle.DynamicProxy;
using static Bib_Hacienda.Clases.Potrero;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para persistencia de potreros (ADR-03 — SRP/DIP).
    /// Usa IAlmacenamiento&lt;Potrero&gt; para desacoplar del formato .txt (ADR-07).
    /// </summary>
    public class PotreroRepositoryTxt : IPotreroRepository
    {
        private readonly IAlmacenamiento<Potrero> _almacenamiento;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PotreroRepositoryTxt(IAlmacenamiento<Potrero> almacenamiento, IHttpContextAccessor httpContextAccessor)
        {
            _almacenamiento = almacenamiento;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Potrero> CargarPotreros()
        {
            try
            {
                var lineas = _almacenamiento.CargarLineas();
                var potreros = new List<Potrero>();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 2)
                    {
                        string identificacion = partes[0];
                        if (Enum.TryParse<l_tipos_potreros>(partes[1], out var tipo))
                        {
                            potreros.Add(new Potrero(identificacion, tipo));
                        }
                    }
                }

                return potreros;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar potreros: {ex.Message}");
            }
        }

        public string GuardarPotreros(List<Potrero> potreros)
        {
            try
            {
                //Validar usando proxy con interceptor
                var interceptor = new InterceptorValidarInformacion(_httpContextAccessor);
                var proxyGenerator = new ProxyGenerator();
                var validadorProxy = proxyGenerator.CreateClassProxy<ValidadorPotrero>(interceptor);

                bool esValido;
                foreach (var potrero in potreros)
                {
                    esValido = validadorProxy.ValidarPotrero(potrero);
                    if (!esValido)
                    {
                        var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                        return mensaje ?? "Error de validación en potrero";
                    }
                }

                //Serializar y guardar
                var lineas = potreros.Select(p => $"{p.Identificacion}|{p.Tipo_potrero}").ToList();
                _almacenamiento.GuardarLineas(lineas);

                return _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString()
                    ?? "Guardado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar potreros: {ex.Message}", ex);
            }
        }
    }
}
