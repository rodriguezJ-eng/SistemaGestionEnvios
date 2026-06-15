public static class Validador
{
    public static string LeerNumeroGuia()
    {
        string? numeroGuia;

        do
        {
            Console.Write("\n  Número de guía : ");
            numeroGuia = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(numeroGuia))
            {
                Console.WriteLine("Error: El número de guía es obligatorio.");
                continue;
            }

            if (numeroGuia.StartsWith("-"))
            {
                Console.WriteLine("Error: No se permiten números negativos.");
                continue;
            }

            if (!long.TryParse(numeroGuia, out _))
            {
                Console.WriteLine("Error: Solo se permiten números.");
                continue;
            }

            if (numeroGuia.Length != 10)
            {
                Console.WriteLine("Error: El número de guía debe tener exactamente 10 dígitos.");
                continue;
            }

            return numeroGuia;

        } while (true);
    }

    public static string LeerNombre(string mensaje)
    {
        string? nombre;

        do
        {
            Console.Write(mensaje);
            nombre = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Error: El campo es obligatorio.");
                continue;
            }

            if (nombre.Length < 3)
            {
                Console.WriteLine("Error: Debe tener al menos 3 caracteres.");
                continue;
            }

            if (nombre.Length > 50)
            {
                Console.WriteLine("Error: No puede tener más de 50 caracteres.");
                continue;
            }

            bool valido = true;

            foreach (char c in nombre)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    valido = false;
                    break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("Error: Solo se permiten letras y espacios.");
                continue;
            }

            return nombre;

        } while (true);
    }

    public static string LeerLugar(string mensaje)
    {
        string? lugar;

        do
        {
            Console.Write(mensaje);
            lugar = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(lugar))
            {
                Console.WriteLine("Error: El campo es obligatorio.");
                continue;
            }

            if (lugar.Length < 3)
            {
                Console.WriteLine("Error: Debe tener al menos 3 caracteres.");
                continue;
            }

            if (lugar.Length > 100)
            {
                Console.WriteLine("Error: No puede tener más de 100 caracteres.");
                continue;
            }

            bool valido = true;

            foreach (char c in lugar)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    valido = false;
                    break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("Error: Solo se permiten letras y espacios.");
                continue;
            }

            return lugar;

        } while (true);
    }

    public static string LeerCategoria()
    {
        do
        {
            Console.WriteLine("\n  Categoría:");
            Console.WriteLine("  1. Nacional");
            Console.WriteLine("  2. Internacional");
            Console.Write("  Seleccione: ");

            string opcion = Console.ReadLine()?.Trim();

            if (opcion == "1")
                return "Nacional";

            if (opcion == "2")
                return "Internacional";

            Console.WriteLine("Error: Seleccione 1 o 2.");

        } while (true);
    }

    public static int LeerEnteroPositivo(string mensaje)
    {
        int valor;

        do
        {
            Console.Write(mensaje);

            if (!int.TryParse(Console.ReadLine()?.Trim(), out valor))
            {
                Console.WriteLine("Error: Debe ingresar un número entero válido.");
                continue;
            }

            if (valor <= 0)
            {
                Console.WriteLine("Error: Debe ser mayor que cero.");
                continue;
            }

            return valor;

        } while (true);
    }

    public static double LeerDoublePositivo(string mensaje)
    {
        double valor;

        do
        {
            Console.Write(mensaje);

            if (!double.TryParse(Console.ReadLine()?.Trim(), out valor))
            {
                Console.WriteLine("Error: Debe ingresar un número válido.");
                continue;
            }

            if (valor <= 0)
            {
                Console.WriteLine("Error: Debe ser mayor que cero.");
                continue;
            }

            return valor;

        } while (true);
    }

    public static decimal LeerDecimalPositivo(string mensaje)
    {
        decimal valor;

        do
        {
            Console.Write(mensaje);

            if (!decimal.TryParse(Console.ReadLine()?.Trim(), out valor))
            {
                Console.WriteLine("Error: Debe ingresar un número válido.");
                continue;
            }

            if (valor < 0)
            {
                Console.WriteLine("Error: No se permiten valores negativos.");
                continue;
            }

            return valor;

        } while (true);
    }

    public static bool LeerSiNo(string mensaje)
    {
        do
        {
            Console.Write(mensaje);

            string respuesta = Console.ReadLine()?.Trim().ToLower();

            if (respuesta == "s")
                return true;

            if (respuesta == "n")
                return false;

            Console.WriteLine("Error: Ingrese únicamente 's' o 'n'.");

        } while (true);
    }

    public static string LeerCodigoPaquete()
    {
        string? codigo;

        do
        {
            Console.Write("  Codigo paquete : ");
            codigo = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.WriteLine("Error: El código es obligatorio.");
                continue;
            }

            if (codigo.Length != 8)
            {
                Console.WriteLine("Error: Debe tener exactamente 8 caracteres.");
                continue;
            }

            bool valido = true;

            foreach (char c in codigo)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    valido = false;
                    break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("Error: Solo se permiten letras y números.");
                continue;
            }

            return codigo;

        } while (true);
    }

    public static string LeerTexto(string mensaje, int minimo, int maximo)
    {
        string? texto;

        do
        {
            Console.Write(mensaje);
            texto = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                Console.WriteLine("Error: El campo es obligatorio.");
                continue;
            }

            if (texto.Length < minimo)
            {
                Console.WriteLine($"Error: Debe tener al menos {minimo} caracteres.");
                continue;
            }

            if (texto.Length > maximo)
            {
                Console.WriteLine($"Error: No puede tener más de {maximo} caracteres.");
                continue;
            }

            return texto;

        } while (true);
    }

 /*   public static string LeerTipoPaquete()
    {
        do
        {
            Console.WriteLine("\n  Tipo de paquete:");
            Console.WriteLine("  1. Pequeño");
            Console.WriteLine("  2. Mediano");
            Console.WriteLine("  3. Grande");
            Console.Write("  Seleccione: ");

            string? opcion = Console.ReadLine()?.Trim();

            if (opcion == "1") return "Pequeño";
            if (opcion == "2") return "Mediano";
            if (opcion == "3") return "Grande";

            Console.WriteLine("Error: Seleccione 1, 2 o 3.");

        } while (true);
    }
 */
    public static string CalcularTipoPaquete(double peso, double alto, double ancho, double largo)
    {
        if (peso <= 0 || alto <= 0 || ancho <= 0 || largo <= 0)
            return "Error: debe ser mayor a 0";

        double volumen = alto * ancho * largo;
        string categoriaTamaño = "";
        string categoriaPeso = "";

        // se busca saber la descripcion del objeto con respecto a su volumen
        if (volumen <= 10000) categoriaTamaño = "pequeño";
        else if (volumen <= 50000) categoriaTamaño = "Mediano";
        else categoriaTamaño = "Grande";

        // se busca saber la descripcion del objeto conrespecto a su oeso
        if (peso <= 2) categoriaPeso = "Liviano";
        else if (peso <= 10) categoriaPeso = "Moderado";
        else categoriaPeso = "Pesado";

        //se presenta si es pequeño mediano o grande con respecto a volumen, y presenta liviano moderado o pesado dependiendo en kg
        return $"{categoriaTamaño} y {categoriaPeso}";
    }

    public static string? LeerPlaca()
    {
        string? placa;

        do
        {
            Console.Write("  Placa del camión: ");
            placa = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Error: La placa es obligatoria.");
                continue;
            }

            if (placa.Length < 6 || placa.Length > 10)
            {
                Console.WriteLine("Error: La placa debe tener entre 6 y 10 caracteres.");
                continue;
            }

            return placa;

        } while (true);
    }
}