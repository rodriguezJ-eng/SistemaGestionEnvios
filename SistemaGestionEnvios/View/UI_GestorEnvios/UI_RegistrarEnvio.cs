public static class UI_RegistrarEnvio
{
    public static void UI_RegistrarNuevoEnvio()
    {
        Console.Clear();
        Console.WriteLine("══ REGISTRAR NUEVO ENVÍO ══\n");

        Console.WriteLine("  Tipo de envío:");
        Console.WriteLine("  1. Terrestre");
        Console.WriteLine("  2. Marítimo");
        Console.WriteLine("  3. Aéreo");
        Console.Write("  Seleccione: ");
    }

    public static void UI_DatosEnvioBasicos()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVIO ══\n");
    }

    public static void UI_DatosPaqueteDelEnvio(int numero)
    {
        Console.Clear();
        Console.WriteLine($"══ DATOS DEL PAQUETE #{numero} ══\n");
    }

    public static void UI_DatosEnvioTerrestre()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO TERRESTRE ══\n");
    }

    public static void UI_DatosEnvioMaritimo()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO MARÍTIMO ══\n");
    }

    public static void UI_DatosEnvioAereo()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO AÉREO ══\n");
    }

    public static void UI_DatosGeneralesEnvio(Envio envio)
    {
        Console.Clear();
        Console.WriteLine("══ DATOS GENERALES DEL ENVÍO ══\n");
        envio.MostrarInformacionEnvio();
        UI_Sistema.UI_Pausa();
        /* Console.WriteLine("\n  Detalles específicos:");
         Console.WriteLine($"\n  Envío registrado. Número de guía asignado: {envio.NumeroGuia}");*/
        UI_Alerta.MostrarExito($"Envío registrado. Número de guía asignado: {envio.NumeroGuia}");
    }
}