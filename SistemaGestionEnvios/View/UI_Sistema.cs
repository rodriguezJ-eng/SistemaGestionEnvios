/// <summary>
/// Gestiona la impresión principal del sistema operativo, menús principales y renderizadores transversales de resultados.
/// </summary>
public static class UI_Sistema
{
    /// <summary>
    /// Dibuja el marco de la interfaz de usuario principal de la aplicación, mostrando un contador en tiempo real de registros activos.
    /// </summary>
    /// <param name="gestor">Referencia del controlador lógico de datos.</param>
    public static void UI_MostrarMenu(GestorEnvios gestor)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE GESTIÓN DE ENVÍOS            ║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine($"║  Envíos registrados: {gestor.ContarEnvios(), -24}║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine("║  1. Registrar envío                          ║");
        Console.WriteLine("║  2. Mostrar todos los envíos                 ║");
        Console.WriteLine("║  3. Buscar envío por número de guía          ║");
        Console.WriteLine("║  4. Filtrar envíos                           ║");
        Console.WriteLine("║  5. Ordenar envíos                           ║");
        Console.WriteLine("║  6. Modificar envío                          ║");
        Console.WriteLine("║  7. Eliminar envío                           ║");
        Console.WriteLine("║  0. Salir                                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.Write("\n  Seleccione una opción: ");
    }

    /// <summary>
    /// Congela el hilo de la consola forzando una espera hasta que el operador presione Enter. Limpia la pantalla después.
    /// </summary>
    public static void UI_Pausa()
    {
        Console.Write("\n  Presione Enter para continuar...");
        Console.ReadLine();
        Console.Clear();
    }

    /// <summary>
    /// Renderiza un listado filtrado u ordenado de elementos. Si no contiene datos, muestra un aviso neutral.
    /// </summary>
    /// <param name="lista">Colección genérica filtrada a proyectar.</param>
    public static void MostrarResultados(List<Envio> lista)
    {
        Console.WriteLine();

        if (lista == null || lista.Count == 0)
        {
            Console.WriteLine("  No se encontraron envíos con ese criterio.");
            return;
        }

        Console.WriteLine($"  {lista.Count} envío(s) encontrado(s):");
        int contador = 1;

        foreach (Envio envio in lista)
        {
            string Encabezado = $"{new string('-', 10)} ENVIO #{contador} | NUMERO DE GUIA: {envio.NumeroGuia} {new string('-', 10)}";
            Console.WriteLine($"\n{Encabezado}");
            envio.MostrarInformacionEnvio();
            Console.WriteLine(new string('-', Encabezado.Length));
            contador++;
        }
    }

    /// <summary>
    /// Envía un mensaje de cierre al finalizar el ciclo principal de ejecución del programa.
    /// </summary>
    public static void Despedir()
    {
        Console.WriteLine("\n  Hasta luego.\n");
    }
}