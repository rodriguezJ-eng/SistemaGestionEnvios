public static class UI_Alerta
{
    public static void MostrarAdvertencia(string mensaje)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("==============================");
        Console.WriteLine("        ¡ADVERTENCIA!");
        Console.WriteLine("==============================");

        Console.ResetColor();
        Console.WriteLine("\n==============================");
        Console.WriteLine("Detalles de la advertencia:");
        Console.WriteLine(mensaje);
        Console.WriteLine("==============================");
        UI_Sistema.UI_Pausa();
    }


    public static void MostrarAdvertencia(string mensaje, bool limpiar, bool pausar)
    {
        if (limpiar)
        {
            Console.Clear();
        }
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\n==============================");
        Console.WriteLine("        ¡ADVERTENCIA!");
        Console.WriteLine("==============================");

        Console.ResetColor();
        Console.WriteLine("\n==============================");
        Console.WriteLine("Detalles de la advertencia:");
        Console.WriteLine(mensaje);
        Console.WriteLine("==============================");
        if (pausar)
        {
            UI_Sistema.UI_Pausa();
        }
    }

    public static void MostrarError(string mensaje)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("==============================");
        Console.WriteLine("    ¡HA OCURRIDO UN ERROR !");
        Console.WriteLine("==============================");

        Console.ResetColor();
        Console.WriteLine("\n==============================");
        Console.WriteLine("Detalles del error:");
        Console.WriteLine(mensaje);
        Console.WriteLine("==============================");
        UI_Sistema.UI_Pausa();
    }

    public static void MostrarError(string mensaje, bool limpiar, bool pausar)
    {
        if (limpiar)
        {
            Console.Clear();
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n==============================");
        Console.WriteLine("    ¡HA OCURRIDO UN ERROR !");
        Console.WriteLine("==============================");

        Console.ResetColor();
        Console.WriteLine("\n==============================");
        Console.WriteLine("Detalles del error:");
        Console.WriteLine(mensaje);
        Console.WriteLine("==============================");
        if (pausar)
        {
            UI_Sistema.UI_Pausa();
        }
    }


    public static void MostrarExito(string mensaje)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("===================================");
        Console.WriteLine("   ¡ACCIÓN REALIZADA CON ÉXITO!");
        Console.WriteLine("===================================");

        Console.ResetColor();
        Console.WriteLine("\n===================================");
        Console.WriteLine("Detalles:");
        Console.WriteLine(mensaje);
        Console.WriteLine("===================================");
        UI_Sistema.UI_Pausa();
    }

    public static void MostrarExito(string mensaje, bool limpiar, bool pausar)
    {
        if (limpiar)
        {
            Console.Clear();
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n===================================");
        Console.WriteLine("   ¡ACCIÓN REALIZADA CON ÉXITO!");
        Console.WriteLine("===================================");

        Console.ResetColor();
        Console.WriteLine("\n===================================");
        Console.WriteLine("Detalles:");
        Console.WriteLine(mensaje);
        Console.WriteLine("===================================");
        if (pausar)
        {
            UI_Sistema.UI_Pausa();
        }
    }
}