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
        envio.MostrarInformacion();
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
        else if(i == 4)
        {
            Console.Write($"  Destino [{envio.Destino}]: ");
        }
        else if(i == 5)
        {
            Console.Write($"  Categoria [{envio.CategoriaEnvio}]: ");
        }
}