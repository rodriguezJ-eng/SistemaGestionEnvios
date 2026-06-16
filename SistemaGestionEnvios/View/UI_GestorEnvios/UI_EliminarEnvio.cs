public static class  UI_EliminarEnvio
{
    public static void Titulo()
    {
        Console.Clear();
        Console.WriteLine("== ELIMINAR ENVIO ==\n");
    }

    public static void PedirNumeroGuia()
    {
        Console.Write("  Numero de guia a eliminar: ");
    }

    public static void DatosDelEnvio(Envio envio)
    {
        Console.WriteLine("\n  Datos del envío a eliminar:");
        envio.MostrarInformacionEnvio();
    }

    public static void PreguntaDeSeguridadParaInseguros()
    {
        Console.Write("\n  Confirma la eliminación? (s/n): ");
    }
}