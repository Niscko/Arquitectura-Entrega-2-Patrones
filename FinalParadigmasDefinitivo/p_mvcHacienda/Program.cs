using Bib_Hacienda.Aspectos;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Interfaces;
using p_mvcHacienda.Servicios;
using p_mvcHacienda.Servicios.Almacenamiento;
using p_mvcHacienda.Servicios.Repositorios;

namespace p_mvcHacienda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // --- Configuración de Autenticación por Cookies ---
            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.Cookie.Name = "HaciendaSoft.Auth";
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            // Agregar HttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // --- Registro de Almacenamiento y Repositorios (ADR-03 + ADR-07) ---
            var datosDir = Path.Combine(builder.Environment.ContentRootPath, "Datos");
            if (!Directory.Exists(datosDir))
            {
                Directory.CreateDirectory(datosDir);
            }

            builder.Services.AddSingleton<IAlmacenamiento<Potrero>>(new AlmacenamientoTxt<Potrero>(Path.Combine(datosDir, "Potreros.txt")));
            builder.Services.AddSingleton<IAlmacenamiento<Res>>(new AlmacenamientoTxt<Res>(Path.Combine(datosDir, "Reses.txt")));
            builder.Services.AddSingleton<IAlmacenamiento<Venta>>(new AlmacenamientoTxt<Venta>(Path.Combine(datosDir, "Ventas.txt")));
            builder.Services.AddSingleton<IAlmacenamiento<Usuario>>(new AlmacenamientoTxt<Usuario>(Path.Combine(datosDir, "Usuarios.txt")));

            builder.Services.AddSingleton<IPotreroRepository, PotreroRepositoryTxt>();
            builder.Services.AddSingleton<IResRepository, ResRepositoryTxt>();
            builder.Services.AddSingleton<IVentaRepository, VentaRepositoryTxt>();
            builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepositoryTxt>();

            builder.Services.AddSingleton<IVacunaRepository>(sp =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var almDisponibles = new AlmacenamientoTxt<Vacuna>(Path.Combine(datosDir, "Vacunas.txt"));
                var almAplicadas = new AlmacenamientoTxt<Vacuna>(Path.Combine(datosDir, "VacunasAplicadas.txt"));
                return new VacunaRepositoryTxt(almDisponibles, almAplicadas, httpContextAccessor);
            });

            // --- Registro de Validadores (ADR-04) ---
            builder.Services.AddSingleton<IValidador<Potrero>, ValidadorPotrero>();
            builder.Services.AddSingleton<IValidador<Res>, ValidadorRes>();
            builder.Services.AddSingleton<IValidador<Venta>, ValidadorVenta>();
            builder.Services.AddSingleton<IValidador<Vacuna>, ValidadorVacuna>();

            // --- Fachada de Persistencia ---
            builder.Services.AddSingleton<PersistenciaService>();

            // --- Hacienda como Singleton (Fachada de Dominio) ---
            builder.Services.AddSingleton<Hacienda>(sp =>
            {
                var hacienda = new Hacienda();
                var persistencia = sp.GetRequiredService<PersistenciaService>();

                // Cargar datos al iniciar
                try
                {
                    var potreros = persistencia.CargarPotreros();
                    foreach (var potrero in potreros)
                    {
                        hacienda.L_potreros.Add(potrero);
                    }

                    persistencia.CargarReses(hacienda.L_potreros);
                    persistencia.CargarVacunasAplicadas(hacienda.L_potreros);

                    var ventas = persistencia.CargarVentas(hacienda.L_potreros);
                    foreach (var venta in ventas)
                    {
                        hacienda.L_ventas.Add(venta);
                    }

                    var vacunas = persistencia.CargarVacunas();
                    foreach (var vacuna in vacunas)
                    {
                        hacienda.L_vacunas.Add(vacuna);
                    }

                    Console.WriteLine($"Datos cargados: {potreros.Count} potreros, {ventas.Count} ventas, {vacunas.Count} vacunas");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar datos: {ex.Message}");
                }

                return hacienda;
            });

            // --- Registro de Servicios de Dominio (ADR-01, ADR-02, ADR-06) ---
            builder.Services.AddSingleton<PotreroService>();
            builder.Services.AddSingleton<IPotreroService>(sp => sp.GetRequiredService<PotreroService>());

            builder.Services.AddSingleton<ResService>();
            builder.Services.AddSingleton<IResService>(sp => sp.GetRequiredService<ResService>());

            builder.Services.AddSingleton<VacunaService>();
            builder.Services.AddSingleton<IVacunaService>(sp => sp.GetRequiredService<VacunaService>());

            builder.Services.AddSingleton<VentaService>();
            builder.Services.AddSingleton<IVentaService>(sp => sp.GetRequiredService<VentaService>());

            builder.Services.AddSingleton<IGeolocalizacionService, GeolocalizacionService>();
            builder.Services.AddSingleton<IHistoriaClinicaService, HistoriaClinicaService>();

            builder.Services.AddSingleton<UsuarioService>(sp =>
            {
                var persistencia = sp.GetRequiredService<PersistenciaService>();
                var usuarioService = new UsuarioService(persistencia);
                usuarioService.CargarUsuarios();
                return usuarioService;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // --- Habilitar Autenticación y Autorización ---
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}