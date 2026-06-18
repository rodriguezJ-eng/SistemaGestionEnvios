/// <summary>
/// Estructura la interfaz de consola encargada de mutar los estados e información de envíos existentes.
/// </summary>
public static class UI_ModificarEnvio
{
    /// <summary>
    /// Imprime el encabezado general del submódulo y solicita la llave de búsqueda.
    /// </summary>
    public static void Menu ()
    {
        Console.Clear();
        Console.WriteLine("== MODIFICAR ENVIO ==\n");

        Console.Write("  Numero de guia a modificar: ");
    }

    /// <summary>
    /// Dibuja el estado actual del envío en el sistema antes de iniciar la captura de mutaciones.
    /// </summary>
    /// <param name="envio">Instancia del envío seleccionado.</param>
    public static void MostrarDatosActuales(Envio envio)
    {
        Console.WriteLine("\n  Datos actuales:");
        envio.MostrarInformacionEnvio();
    }

    /// <summary>
    /// Despliega la instrucción de edición para notificar que los campos vacíos retendrán la información original.
    /// </summary>
    public static void TituloFormulario()
    {
        Console.WriteLine("\n  Nuevos datos (Enter para conservar el actual):\n");
    }

    /// <summary>
    /// Genera las etiquetas de los campos del formulario inyectando los valores actuales como placeholders en línea.
    /// </summary>
    /// <param name="i">Índice del campo secuencial en proceso de edición.</param>
    /// <param name="envio">Objeto de envío que provee los strings por defecto.</param>
    public static void Formulario(int i, Envio envio)
    {
        if (i == 1)
        {
            Console.Write($"  Remitente [{envio.Remitente}]: ");
        }
        else if (i == 2)
        {
            Console.Write($"  Destinatario [{envio.Destinatario}]: ");
        }
        else if (i == 3)
        {
            Console.Write($"  Origen [{envio.Origen}]: ");
        }
        else if (i == 4)
        {
            Console.Write($"  Destino [{envio.Destino}]: ");
        }
        else if (i == 5)
        {
            Console.Write($"  Categoria [{envio.CategoriaEnvio}]: ");
        }
    }

    /// <summary>
    /// Pregunta al operario si requiere modificar el estado logístico de la carga.
    /// </summary>
    public static void DeseaModificarEstado()
    {
        Console.Write("\n  Actualizar estado? (s/n): ");
    }

    /// <summary>
    /// Presenta la botonera de estados disponibles y retorna la cadena correspondiente a la opción capturada.
    /// </summary>
    /// <returns>String sanitizado que mapea con el estado de la máquina de estados.</returns>
    /// <exception cref="ArgumentException">Lanzada si el índice introducido no coincide con las opciones del menú.</exception>
    public static string LeerNuevoEstado()
    {
        Console.WriteLine("  Estados disponibles:");
        Console.WriteLine("  1. Pendiente");
        Console.WriteLine("  2. En transito");
        Console.WriteLine("  3. Entregado");
        Console.WriteLine("  4. Cancelado");
        Console.Write("  Seleccione: ");

        string opcion = Console.ReadLine()?.Trim();

        return opcion switch
        {
            "1" => "Pendiente",
            "2" => "En transito",
            "3" => "Entregado",
            "4" => "Cancelado",
            _ => throw new ArgumentException("Opción no válida.")
        };
    }
}