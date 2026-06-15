public static class UI_FiltrarEnvios
{
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

    public static void UI_OpcionBusqueda(int op)
    {
        if(op = 1)
        {
            Console.Write("  Tipo (Terrestre / Maritimo / Aereo): ");
            return;
        }
        else if (op = 2)
        {
            Console.Write("  Estado: ");
            return;
        }
        else if (op = 3)
        {
            Console.Write("  Categoría: ");
            return;
        }
        else if (op = 4)
        {
            Console.Write("  Remitente (o parte del nombre): ");
            return;
        }
    }
}