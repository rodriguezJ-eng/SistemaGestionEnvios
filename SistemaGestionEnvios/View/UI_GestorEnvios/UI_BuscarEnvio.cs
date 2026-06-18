/// <summary>
/// Controla la impresión en consola para la consulta individual de registros logísticos.
/// </summary>
public static class UI_BuscarEnvio
{
    /// <summary>
    /// Dibuja la cabecera del módulo de búsquedas y solicita la entrada de la guía de rastreo.
    /// </summary>
    public static void UI_Titulo()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR ENVIO POR NÚMERO DE GUÍA ===\n");
        Console.Write("  Número de guía: ");
    }

    /// <summary>
    /// Imprime la etiqueta de éxito e instruye al objeto polimórfico a pintar su información específica.
    /// </summary>
    /// <param name="encontrado">Instancia de la clase base localizada.</param>
    public static void UI_EnvioEncontrado(Envio encontrado)
    {
        Console.WriteLine("\n  Envío encontrado:");
        encontrado.MostrarInformacionEnvio(); // Invocación polimórfica (Aéreo, Marítimo o Terrestre)
    }
}