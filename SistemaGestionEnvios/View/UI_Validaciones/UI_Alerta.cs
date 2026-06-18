/// <summary>
/// Proporciona componentes visuales para mostrar alertas de advertencia, error y éxito
/// formateadas con colores y bordes en la consola.
/// </summary>
public static class UI_Alerta
{
    /// <summary>
    /// Muestra un banner de advertencia en color amarillo oscuro, limpia la consola por defecto y pausa el flujo.
    /// </summary>
    /// <param name="mensaje">Detalle textual de la advertencia.</param>
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

    /// <summary>
    /// Muestra un banner de advertencia permitiendo controlar la limpieza de pantalla y la pausa del hilo.
    /// </summary>
    /// <param name="mensaje">Detalle textual de la advertencia.</param>
    /// <param name="limpiar">Indica si se debe borrar el contenido previo de la consola.</param>
    /// <param name="pausar">Indica si se requiere detener el flujo hasta que el usuario presione Enter.</param>
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

    /// <summary>
    /// Muestra un banner de error crítico en color rojo, limpia la pantalla y pausa la ejecución.
    /// </summary>
    /// <param name="mensaje">Mensaje de excepción o error validado.</param>
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

    /// <summary>
    /// Muestra un banner de error crítico permitiendo parametrizar la limpieza y pausa de la consola.
    /// </summary>
    /// <param name="mensaje">Mensaje de error a desplegar.</param>
    /// <param name="limpiar">Indica si se limpia la pantalla previamente.</param>
    /// <param name="pausar">Indica si el sistema debe esperar una confirmación de lectura.</param>
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

    /// <summary>
    /// Despliega una alerta verde indicando que un proceso u operación concluyó satisfactoriamente.
    /// </summary>
    /// <param name="mensaje">Resumen del éxito operativo.</param>
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

    /// <summary>
    /// Despliega una alerta verde de éxito con configuración explícita de comportamiento visual.
    /// </summary>
    /// <param name="mensaje">Resumen del éxito operativo.</param>
    /// <param name="limpiar">Determina si borra la consola antes de pintar el marco.</param>
    /// <param name="pausar">Determina si congela la pantalla al finalizar el mensaje.</param>
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