using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Aspectos;
using Bib_Hacienda.Interfaces;
using Castle.DynamicProxy;
using System.Globalization;
using static Bib_Hacienda.Clases.Viva;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para persistencia de vacunas (ADR-03 — SRP/DIP).
    /// </summary>
    public class VacunaRepositoryTxt : IVacunaRepository
    {
        private readonly IAlmacenamiento<Vacuna> _almacenamientoVacunas;
        private readonly IAlmacenamiento<Vacuna> _almacenamientoVacunasAplicadas;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VacunaRepositoryTxt(
            IAlmacenamiento<Vacuna> almacenamientoVacunas,
            IAlmacenamiento<Vacuna> almacenamientoVacunasAplicadas,
            IHttpContextAccessor httpContextAccessor)
        {
            _almacenamientoVacunas = almacenamientoVacunas;
            _almacenamientoVacunasAplicadas = almacenamientoVacunasAplicadas;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Vacuna> CargarVacunas()
        {
            try
            {
                var lineas = _almacenamientoVacunas.CargarLineas();
                var vacunas = new List<Vacuna>();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 6)
                    {
                        string nombre = partes[0];
                        string lote = partes[1];
                        if (!DateTime.TryParseExact(partes[2].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVenc))
                            continue;
                        if (!DateTime.TryParseExact(partes[3].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAplic))
                            continue;

                        string tipo = partes[4].Trim();
                        uint periodo = uint.TryParse(partes[5].Trim(), out var per) ? per : 0u;

                        Vacuna vacuna;
                        if (tipo.Equals("Bacteriana", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!uint.TryParse(partes[5].Trim(), out periodo) || periodo < 2 || periodo > 4)
                                continue;
                            try
                            {
                                vacuna = new Bacteriana(nombre, lote, fechaVenc, fechaAplic, periodo);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        else
                        {
                            vacuna = new Viva(nombre, lote, fechaVenc, fechaAplic, enum_l_atenuaciones.Atenuacion10);
                        }

                        vacunas.Add(vacuna);
                    }
                }

                return vacunas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar vacunas: {ex.Message}");
            }
        }

        public string GuardarVacunas(List<Vacuna> vacunas)
        {
            try
            {
                var interceptor = new InterceptorValidarInformacion(_httpContextAccessor);
                var proxyGenerator = new ProxyGenerator();
                var validadorProxy = proxyGenerator.CreateClassProxy<ValidadorVacuna>(interceptor);

                bool esValida;
                foreach (var vacuna in vacunas)
                {
                    esValida = validadorProxy.ValidarVacuna(vacuna);
                    if (!esValida)
                    {
                        var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                        return mensaje ?? "Error de validación en vacuna";
                    }
                }

                var lineas = new List<string>();
                foreach (var vacuna in vacunas)
                {
                    string fechaVenc = vacuna.Fecha_vencimiento.ToString("yyyy-MM-dd");
                    string fechaAplic = vacuna.Fecha_aplicacion.ToString("yyyy-MM-dd");
                    string tipo = vacuna.GetType().Name;
                    uint periodo = vacuna is Bacteriana bacteriana ? bacteriana.Periodo_aplicacion : 0;
                    lineas.Add($"{vacuna.Nombre}|{vacuna.Lote}|{fechaVenc}|{fechaAplic}|{tipo}|{periodo}");
                }

                _almacenamientoVacunas.GuardarLineas(lineas);

                return _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString()
                    ?? "Guardado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar vacunas: {ex.Message}", ex);
            }
        }

        public void CargarVacunasAplicadas(List<Potrero> potreros)
        {
            try
            {
                var lineas = _almacenamientoVacunasAplicadas.CargarLineas();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 8)
                    {
                        string nombrePotrero = partes[0].Trim();
                        string nombreRes = partes[1];
                        string nombreVacuna = partes[2];
                        string lote = partes[3];
                        if (!DateTime.TryParseExact(partes[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVenc))
                            continue;
                        if (!DateTime.TryParseExact(partes[5].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAplic))
                            continue;

                        string tipo = partes[6];
                        uint periodo = uint.TryParse(partes[7].Trim(), out var per) ? per : 0u;

                        var potrero = potreros.FirstOrDefault(p => string.Equals(p.Identificacion, nombrePotrero, StringComparison.OrdinalIgnoreCase));
                        if (potrero != null)
                        {
                            try
                            {
                                var res = potrero.buscar_res(nombreRes);
                                if (res != null)
                                {
                                    Vacuna vacuna;
                                    if (tipo == "Bacteriana")
                                    {
                                        vacuna = new Bacteriana(nombreVacuna, lote, fechaVenc, fechaAplic, periodo);
                                    }
                                    else
                                    {
                                        vacuna = new Viva(nombreVacuna, lote, fechaVenc, fechaAplic, enum_l_atenuaciones.Atenuacion10);
                                    }
                                    res.L_vacunas_aplicadas.Add(vacuna);
                                }
                            }
                            catch
                            {
                                // Res no encontrada, skip
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar vacunas aplicadas: {ex.Message}");
            }
        }

        public string GuardarVacunasAplicadas(List<Potrero> potreros)
        {
            try
            {
                var interceptor = new InterceptorValidarInformacion(_httpContextAccessor);
                var proxyGenerator = new ProxyGenerator();
                var validadorResProxy = proxyGenerator.CreateClassProxy<ValidadorRes>(interceptor);
                var validadorVacunaProxy = proxyGenerator.CreateClassProxy<ValidadorVacuna>(interceptor);

                var lineas = new List<string>();

                foreach (var potrero in potreros)
                {
                    foreach (var res in potrero.L_reses)
                    {
                        bool resValida = validadorResProxy.ValidarRes(res);
                        if (!resValida)
                        {
                            var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                            return mensaje ?? "Error de validación en res";
                        }

                        foreach (var vacuna in res.L_vacunas_aplicadas)
                        {
                            bool vacunaValida = validadorVacunaProxy.ValidarVacuna(vacuna);
                            if (!vacunaValida)
                            {
                                var mensaje = _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString();
                                return mensaje ?? "Error de validación en vacuna aplicada";
                            }

                            string fechaVenc = vacuna.Fecha_vencimiento.ToString("yyyy-MM-dd");
                            string fechaAplic = vacuna.Fecha_aplicacion.ToString("yyyy-MM-dd");
                            string tipo = vacuna.GetType().Name;
                            uint periodo = vacuna is Bacteriana bac ? bac.Periodo_aplicacion : 0;
                            lineas.Add($"{potrero.Identificacion}|{res.Nombre}|{vacuna.Nombre}|{vacuna.Lote}|{fechaVenc}|{fechaAplic}|{tipo}|{periodo}");
                        }
                    }
                }

                _almacenamientoVacunasAplicadas.GuardarLineas(lineas);

                return _httpContextAccessor.HttpContext?.Items["ResultadoValidacion"]?.ToString()
                    ?? "Guardado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar vacunas aplicadas: {ex.Message}", ex);
            }
        }
    }
}
