/// <summary>
/// Encapsula las llamadas y solicitudes visuales de datos tipados utilizadas por los métodos de captura y parseo seguro.
/// </summary>
public static class UI_Validador
{
    /// <summary>
    /// Prompt para captura del alfanumérico identificador de guía.
    /// </summary>
    public static void LeerNumeroGuia()
    {
        Console.Write("\n  Número de guía : ");
    }

    /// <summary>
    /// Prompt para captura de nombres de personas u organizaciones.
    /// </summary>
    /// <param name="msj">Texto de instrucción personalizado.</param>
    public static void LeerNombre(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt para captura de ciudades, sucursales u oficinas.
    /// </summary>
    /// <param name="msj">Texto de instrucción personalizado.</param>
    public static void LeerLugar(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Despliega las dos opciones geográficas de clasificación de alcance.
    /// </summary>
    public static void LeerCategoria()
    {
        Console.WriteLine("\n  Categoría:");
        Console.WriteLine("  1. Nacional");
        Console.WriteLine("  2. Internacional");
        Console.Write("  Seleccione: ");
    }

    /// <summary>
    /// Prompt genérico para números enteros mayores a cero (ej. Distancias, cantidades).
    /// </summary>
    /// <param name="msj">Instrucción o variable solicitada.</param>
    public static void LeerEnteroPositivo(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt genérico para reales de precisión doble de carácter positivo (ej. Dimensiones, pesos).
    /// </summary>
    /// <param name="msj">Instrucción o variable solicitada.</param>
    public static void LeerDoublePositivo(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt genérico para tipos Decimal de carácter positivo (ej. Valores monetarios declarados).
    /// </summary>
    /// <param name="msj">Instrucción o variable solicitada.</param>
    public static void LeerDecimalPositivo(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt estándar para confirmación (s/n).
    /// </summary>
    /// <param name="msj">Pregunta dicotómica formulada.</param>
    public static void LeerSiNo(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt explícito para capturar el código interno de barras de un paquete.
    /// </summary>
    public static void LeerCodigoPaquete()
    {
        Console.Write("  Codigo paquete : ");
    }

    /// <summary>
    /// Prompt versátil para la recolección de entradas de texto sin formato estricto.
    /// </summary>
    /// <param name="msj">Instrucción de captura.</param>
    public static void LeerTexto(string msj)
    {
        Console.Write(msj);
    }

    /// <summary>
    /// Prompt orientado a la lectura de registros de matrículas de vehículos de distribución vial.
    /// </summary>
    public static void LeerPlaca()
    {
        Console.Write("  Placa del camión: ");
    }
}