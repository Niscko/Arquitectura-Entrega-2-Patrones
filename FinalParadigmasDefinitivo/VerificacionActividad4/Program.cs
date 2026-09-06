using Bib_Hacienda.Clases;
using static Bib_Hacienda.Clases.Potrero;

internal static class Program
{
    private static readonly DateTime FechaAplicacion = new DateTime(2026, 1, 1);
    private static readonly DateTime FechaVencimiento = new DateTime(2028, 1, 1);
    private static int pasaron;
    private static int fallaron;

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("ACTIVIDAD 4.2 — Doce casos (8 del Reto 1 + 4 que recorren los patrones)");
        Console.WriteLine("Antes = salida congelada del AS-IS. Después = ejecución TO-BE.\n");

        Caso01_CrearPotrero();
        Caso02_AnadirResConEventoPeso();
        Caso03_EdadInvalida();
        Caso04_AlimentarRes();
        Caso05_CrearVacunaBacteriana();
        Caso06_LoteDuplicado();
        Caso07_AplicarVacunaEsquemaIncompleto();
        Caso08_VenderRes();
        Caso09_FactoryTresTipos();
        Caso10_ObserverSinDuplicar();
        Caso11_StrategyTopesPorTipo();
        Caso12_BuilderValidacionComun();

        Console.WriteLine($"\nResumen: {pasaron} coinciden, {fallaron} difieren. Total 12.");
        Environment.Exit(fallaron == 0 ? 0 : 1);
    }

    private static void Caso01_CrearPotrero()
    {
        var h = new Hacienda();
        Comparar("C-01 (Reto 1) crear potrero",
            "El potrero P1 se a añadido a la hacienda. ",
            Ejecutar(() => h.crear_potrero("P1", l_tipos_potreros.ternero)));
    }

    private static void Caso02_AnadirResConEventoPeso()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        Comparar("C-02 (Reto 1) añadir ternero bajo peso mínimo",
            "La res Luna ha sido añadida al potrero PT con exito.\n[Evento] La res 'Luna' tiene un peso 100, está en desnutrición.",
            Ejecutar(() => h.anadir_res_potrero("PT", "Luna", 6, 100)));
    }

    private static void Caso03_EdadInvalida()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        Comparar("C-03 (Reto 1) edad fuera de rango del potrero",
            "Error inesperado en el método anadir_res_potrero: Error inesperado en el metodo anadir_res: La res no puede ser añadida al potrero PT porque su edad no corresponde al tipo de potrero",
            Ejecutar(() => h.anadir_res_potrero("PT", "Vieja", 20, 200)));
    }

    private static void Caso04_AlimentarRes()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        h.anadir_res_potrero("PT", "Luna", 6, 100);
        Comparar("C-04 (Reto 1) alimentar res",
            "La res 'Luna' ha sido alimentada, ahora pesa 110 kg.\n[Evento] La res 'Luna' tiene un peso 110, está en desnutrición.",
            Ejecutar(() => h.alimentar_res("PT", "Luna", 10)));
    }

    private static void Caso05_CrearVacunaBacteriana()
    {
        var h = new Hacienda();
        Comparar("C-05 (Reto 1) crear vacuna bacteriana",
            "Vacuna bacteriana 'Aftosa' del lote 'L001' agregada al inventario con éxito. Período de aplicación: 3 semanas.",
            Ejecutar(() => h.crear_vacuna("Aftosa", "L001", FechaVencimiento, FechaAplicacion, 3u)));
    }

    private static void Caso06_LoteDuplicado()
    {
        var h = new Hacienda();
        h.crear_vacuna("Aftosa", "L001", FechaVencimiento, FechaAplicacion, 3u);
        Comparar("C-06 (Reto 1) lote duplicado",
            "Error inesperado en el método crear_vacuna (bacteriana): Ya existe una vacuna con el lote 'L001' en el inventario",
            Ejecutar(() => h.crear_vacuna("Aftosa", "L001", FechaVencimiento, FechaAplicacion, 3u)));
    }

    private static void Caso07_AplicarVacunaEsquemaIncompleto()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        h.anadir_res_potrero("PT", "Luna", 6, 180);
        h.crear_vacuna("Aftosa", "L001", FechaVencimiento, FechaAplicacion, 3u);
        var vacuna = h.L_vacunas.First(v => v.Lote == "L001");
        Comparar("C-07 (Reto 1) aplicar vacuna, esquema incompleto",
            "Vacuna aplicada correctamente a la res Luna. [Evento] La res 'Luna' aún no ha completado su esquema de vacunación. Bacterianas: 1, Vivas: 0",
            Ejecutar(() => h.aplicar_vacuna(vacuna, "Luna", "PT")));
    }

    private static void Caso08_VenderRes()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        h.anadir_res_potrero("PT", "Luna", 6, 180);
        Comparar("C-08 (Reto 1) vender res",
            "Venta de la res Luna realizada con exito",
            Ejecutar(() => h.vender_res("PT", "Luna", 500000)));
    }

    private static void Caso09_FactoryTresTipos()
    {
        var h = new Hacienda();
        h.crear_potrero("T", l_tipos_potreros.ternero);
        h.crear_potrero("C", l_tipos_potreros.cebon);
        h.crear_potrero("N", l_tipos_potreros.novillo);
        h.anadir_res_potrero("T", "T1", 6, 180);
        h.anadir_res_potrero("C", "C1", 20, 300);
        h.anadir_res_potrero("N", "N1", 50, 500);
        string tipos = string.Join(",",
            h.buscar_potrero("T").L_reses[0].GetType().Name,
            h.buscar_potrero("C").L_reses[0].GetType().Name,
            h.buscar_potrero("N").L_reses[0].GetType().Name);
        Comparar("C-09 (nuevo, Factory Method) tipos concretos según potrero",
            "Ternero,Cebon,Novillo",
            tipos);
    }

    private static void Caso10_ObserverSinDuplicar()
    {
        var h = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        h.anadir_res_potrero("PT", "Luna", 6, 100);
        string primera = Ejecutar(() => h.alimentar_res("PT", "Luna", 10));
        string segunda = Ejecutar(() => h.alimentar_res("PT", "Luna", 10));
        int eventosSegunda = segunda.Split("[Evento]", StringSplitOptions.RemoveEmptyEntries).Length - 1;
        string obtenido = $"1:{primera}\n2:{segunda}\neventos_en_segunda={eventosSegunda}";
        string esperado =
            "1:La res 'Luna' ha sido alimentada, ahora pesa 110 kg.\n[Evento] La res 'Luna' tiene un peso 110, está en desnutrición.\n" +
            "2:La res 'Luna' ha sido alimentada, ahora pesa 120 kg.\n[Evento] La res 'Luna' tiene un peso 120, está en desnutrición.\n" +
            "eventos_en_segunda=1";
        Comparar("C-10 (nuevo, Observer) segunda alimentación no duplica handlers", esperado, obtenido);
    }

    private static void Caso11_StrategyTopesPorTipo()
    {
        var terneros = HaciendaConPotrero("PT", l_tipos_potreros.ternero);
        terneros.anadir_res_potrero("PT", "T1", 6, 180);
        string cuartoTernero = AplicarNBacterianas(terneros, "PT", "T1", 4);

        var cebones = HaciendaConPotrero("PC", l_tipos_potreros.cebon);
        cebones.anadir_res_potrero("PC", "C1", 20, 300);
        string segundaCebon = AplicarNBacterianas(cebones, "PC", "C1", 2);

        bool topeTernero = cuartoTernero.Contains("Ya tiene las 3 permitidas");
        bool topeCebon = segundaCebon.Contains("Ya tiene las 1 permitidas");
        Comparar("C-11 (nuevo, Strategy) tope bacterianas por tipo",
            "ternero=3;cebon=1",
            $"ternero={(topeTernero ? "3" : "FAIL")};cebon={(topeCebon ? "1" : "FAIL")}");
    }

    private static void Caso12_BuilderValidacionComun()
    {
        var h = new Hacienda();
        string bacteriana = Ejecutar(() => h.crear_vacuna(" ", "L001", FechaVencimiento, FechaAplicacion, 3u));
        string viva = Ejecutar(() => h.crear_vacuna(" ", "L002", FechaVencimiento, FechaAplicacion, Viva.enum_l_atenuaciones.Atenuacion10));
        string lote = Ejecutar(() => h.crear_vacuna(" ", "LB", FechaVencimiento, FechaAplicacion, 3u, 2u));
        bool mismoMensaje =
            bacteriana.Contains("El nombre de la vacuna no puede estar vacío") &&
            viva.Contains("El nombre de la vacuna no puede estar vacío") &&
            lote.Contains("El nombre de la vacuna no puede estar vacío");
        Comparar("C-12 (nuevo, Builder) validación común en sobrecargas",
            "mismo_mensaje=true",
            $"mismo_mensaje={mismoMensaje.ToString().ToLowerInvariant()}");
    }

    private static Hacienda HaciendaConPotrero(string id, l_tipos_potreros tipo)
    {
        var h = new Hacienda();
        h.crear_potrero(id, tipo);
        return h;
    }

    private static string AplicarNBacterianas(Hacienda h, string potrero, string res, int n)
    {
        string ultimo = "";
        for (int i = 1; i <= n; i++)
        {
            string lote = $"X{potrero}{res}{i}";
            h.crear_vacuna($"Vac{i}", lote, FechaVencimiento, FechaAplicacion, 3u);
            var vacuna = h.L_vacunas.First(v => v.Lote == lote);
            ultimo = Ejecutar(() => h.aplicar_vacuna(vacuna, res, potrero));
        }
        return ultimo;
    }

    private static string Ejecutar(Func<string> accion)
    {
        try
        {
            return accion();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private static void Comparar(string titulo, string esperado, string obtenido)
    {
        bool ok = esperado == obtenido;
        if (ok) pasaron++; else fallaron++;
        Console.WriteLine("────────────────────────────────────────");
        Console.WriteLine(titulo);
        Console.WriteLine(ok ? "RESULTADO: COINCIDEN" : "RESULTADO: DIFEREN");
        Console.WriteLine("ANTES (AS-IS esperado):");
        Console.WriteLine(esperado);
        Console.WriteLine("DESPUÉS (TO-BE obtenido):");
        Console.WriteLine(obtenido);
    }
}
