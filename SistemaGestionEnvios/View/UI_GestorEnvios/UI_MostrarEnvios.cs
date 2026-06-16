public static class UI_MostrarEnvios
{
    public static void UI_MostrarEnviosTitulo()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE ENVIOS ===\n");
    }

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