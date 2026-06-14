public static class UI_Sistema
{
    public static void UI_MostrarMenu(GestorEnvios gestor)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE GESTIÓN DE ENVÍOS            ║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine($"║  Envíos registrados: {gestor.ContarEnvios(), -24}║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine("║  1. Registrar envío                          ║");
        Console.WriteLine("║  2. Mostrar todos los envíos                 ║");
        Console.WriteLine("║  3. Buscar envío por número de guía          ║");
        Console.WriteLine("║  4. Filtrar envíos                           ║");
        Console.WriteLine("║  5. Ordenar envíos                           ║");
        Console.WriteLine("║  6. Modificar envío                          ║");
        Console.WriteLine("║  7. Eliminar envío                           ║");
        Console.WriteLine("║  0. Salir                                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.Write("\n  Seleccione una opción: ");
    }

    public static void UI_Pausa()
    {
        Console.Write("\n  Presione Enter para continuar...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void UI_RegistrarNuevoEnvio()
    {
        Console.Clear();
        Console.WriteLine("══ REGISTRAR NUEVO ENVÍO ══\n");

        Console.WriteLine("  Tipo de envío:");
        Console.WriteLine("  1. Terrestre");
        Console.WriteLine("  2. Marítimo");
        Console.WriteLine("  3. Aéreo");
        Console.Write("  Seleccione: ");
    }

    public static void UI_FiltrarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== FILTRAR ENVÍOS ===\n");
        Console.WriteLine("  Filtrar por:");
        Console.WriteLine("  1. Tipo de envío (Terrestre / Marítimo / Aéreo)");
        Console.WriteLine("  2. Estado (Pendiente / En transito / Entregado / Cancelado)");
        Console.WriteLine("  3. Categoría de envío");
        Console.WriteLine("  4. Remitente");
        Console.Write("\n  Seleccione: ");
    }

    public static void UI_OrdenarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== ORDENAR ENVÍOS ===\n");
        Console.WriteLine("  Ordenar por:");
        Console.WriteLine("  1. Fecha de registro (más reciente primero)");
        Console.WriteLine("  2. Número de guía");
        Console.WriteLine("  3. Estado");
        Console.WriteLine("  4. Costo total (mayor a menor)");
        Console.Write("\n  Seleccione: ");
    }
}