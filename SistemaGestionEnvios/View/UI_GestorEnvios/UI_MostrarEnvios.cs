/// <summary>
/// Controla el renderizado masivo y formateado de listas completas de guías de envío.
/// </summary>
public static class UI_MostrarEnvios
{
    /// <summary>
    /// Limpia la pantalla y escribe el encabezado del reporte general de inventario logístico.
    /// </summary>
    public static void UI_MostrarEnviosTitulo()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE ENVIOS ===\n");
    }

    /// <summary>
    /// Genera la iteración visual estructurando separadores dinámicos que encierran el bloque informativo de cada objeto.
    /// </summary>
    /// <param name="envios">Colección genérica con los objetos tipo Envios a listar.</param>
    public static void UI_MostrarEnviosListados(List<Envio> envios)
    {
        Console.WriteLine($"  Total: {envios.Count} envío(s)\n");

        int contador = 1;
        foreach (Envio envio in envios)
        {
            string Encabezado = $"{new string('-', 10)} ENVIO #{contador} | NUMERO DE GUIA: {envio.NumeroGuia} {new string('-', 10)}";
            Console.WriteLine($"\n{Encabezado}");
            envio.MostrarInformacionEnvio();
            Console.WriteLine(new string('-', Encabezado.Length));
            contador++;
        }
    }
}