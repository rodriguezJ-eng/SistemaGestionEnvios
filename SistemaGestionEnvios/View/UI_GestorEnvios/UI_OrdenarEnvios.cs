/// <summary>
/// Administra el menú de selección de algoritmos y criterios de ordenación de listas.
/// </summary>
public static class UI_OrdenarEnvios
{
    /// <summary>
    /// Presenta al operador las propiedades cuantitativas y cualitativas bajo las cuales puede ordenar los registros.
    /// </summary>
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