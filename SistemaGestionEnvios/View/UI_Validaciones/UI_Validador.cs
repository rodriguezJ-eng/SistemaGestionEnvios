public static class UI_Validador
{
    public static void LeerNumeroGuia()
    {
        Console.Write("\n  Número de guía : ");
    }

    public static void LeerNombre(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerLugar(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerCategoria()
    {
        Console.WriteLine("\n  Categoría:");
        Console.WriteLine("  1. Nacional");
        Console.WriteLine("  2. Internacional");
        Console.Write("  Seleccione: ");
    }

    public static void LeerEnteroPositivo(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerDoublePositivo(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerDecimalPositivo(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerSiNo(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerCodigoPaquete()
    {
        Console.Write("  Codigo paquete : ");
    }

    public static void LeerTexto(string msj)
    {
        Console.Write(msj);
    }

    public static void LeerPlaca()
    {
        Console.Write("  Placa del camión: ");
    }
}