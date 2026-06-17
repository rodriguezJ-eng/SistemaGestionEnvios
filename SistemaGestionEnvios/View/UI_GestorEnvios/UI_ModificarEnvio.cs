public static class UI_ModificarEnvio
{
    public static void Menu ()
    {
        Console.Clear();
        Console.WriteLine("== MODIFICAR ENVIO ==\n");

        Console.Write("  Numero de guia a modificar: ");
    }

    public static void MostrarDatosActuales(Envio envio)
    {
        Console.WriteLine("\n  Datos actuales:");
        envio.MostrarInformacionEnvio();
    }

    public static void TituloFormulario()
    {
        Console.WriteLine("\n  Nuevos datos (Enter para conservar el actual):\n");
    }

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

    public static void DeseaModificarEstado()
    {
        Console.Write("\n  Actualizar estado? (s/n): ");
    }

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