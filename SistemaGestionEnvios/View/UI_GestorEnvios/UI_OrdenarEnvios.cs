public static class UI_OrdenarEnvios
{
    public static void UI_Menu()
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