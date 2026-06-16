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

    public static void MostrarResultados(List<Envio> lista)
    {
        Console.WriteLine();

        if (lista == null || lista.Count == 0)
        {
            Console.WriteLine("  No se encontraron envíos con ese criterio.");
            return;
        }

        Console.WriteLine($"  {lista.Count} envío(s) encontrado(s):");
        Console.WriteLine(new string('-', 50));

        foreach (Envio envio in lista)
        {
            envio.MostrarInformacionEnvio();
            Console.WriteLine(new string('-', 50));
        }
    }

    public static void Despedir()
    {
        Console.WriteLine("\n  Hasta luego.\n");
    }
}