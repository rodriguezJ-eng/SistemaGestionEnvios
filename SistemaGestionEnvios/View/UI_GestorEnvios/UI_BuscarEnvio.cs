public static class UI_BuscarEnvio
{
    public static void UI_Titulo()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR ENVIO POR NÚMERO DE GUÍA ===\n");
        Console.Write("  Número de guía: ");
    }

    public static void UI_EnvioEncontrado(Envio encontrado)
    {
        Console.WriteLine("\n  Envío encontrado:");
        encontrado.MostrarInformacion();
    }
}