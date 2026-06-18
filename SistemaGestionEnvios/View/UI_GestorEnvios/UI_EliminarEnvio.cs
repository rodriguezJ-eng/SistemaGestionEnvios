/// <summary>
/// Gestiona la salida en pantalla del proceso de baja o borrado de guías del sistema.
/// </summary>
public static class  UI_EliminarEnvio
{
    /// <summary>
    /// Presenta el título del módulo de depuración de registros.
    /// </summary>
    public static void Titulo()
    {
        Console.Clear();
        Console.WriteLine("== ELIMINAR ENVIO ==\n");
    }

    /// <summary>
    /// Imprime el indicador de lectura del código alfanumérico a eliminar.
    /// </summary>
    public static void PedirNumeroGuia()
    {
        Console.Write("  Numero de guia a eliminar: ");
    }

    /// <summary>
    /// Despliega el resumen de datos del envío que está en cola de eliminación para validación visual del usuario.
    /// </summary>
    /// <param name="envio">Objeto de envío preseleccionado.</param>
    public static void DatosDelEnvio(Envio envio)
    {
        Console.WriteLine("\n  Datos del envío a eliminar:");
        envio.MostrarInformacionEnvio();
    }

    /// <summary>
    /// Solicita confirmación final para mitigar pérdidas accidentales de datos en el repositorio.
    /// </summary>
    public static void PreguntaDeSeguridadParaInseguros()
    {
        Console.Write("\n  Confirma la eliminación? (s/n): ");
    }
}