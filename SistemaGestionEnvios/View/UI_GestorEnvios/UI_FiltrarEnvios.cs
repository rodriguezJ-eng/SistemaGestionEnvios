/// <summary>
/// Controla los menús interactivos destinados a la segregación de listas según propiedades compartidas.
/// </summary>
public static class UI_FiltrarEnvios
{
    /// <summary>
    /// Presenta las propiedades y criterios disponibles mediante los cuales el usuario puede filtrar los envíos.
    /// </summary>
    public static void UI_Menu()
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

    /// <summary>
    /// Evalúa la opción de filtrado y escribe el indicador exacto correspondiente al dato esperado.
    /// </summary>
    /// <param name="op">Identificador numérico de la opción elegida.</param>
    public static void UI_OpcionBusqueda(int op)
    {
        if(op == 1)
        {
            Console.Write("  Tipo (Terrestre / Maritimo / Aereo): ");
            return;
        }
        else if (op == 2)
        {
            Console.Write("  Estado: ");
            return;
        }
        else if (op == 3)
        {
            Console.Write("  Categoría: ");
            return;
        }
        else if (op == 4)
        {
            Console.Write("  Remitente (o parte del nombre): ");
            return;
        }
    }
}