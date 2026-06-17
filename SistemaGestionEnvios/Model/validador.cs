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
                UI_Alerta.MostrarError("Error: El número de guía es obligatorio.", false, false);
                continue;
            }

            if (numeroGuia.StartsWith("-"))
            {
                UI_Alerta.MostrarError("Error: No se permiten números negativos.", false, false);
                continue;
            }

            if (!long.TryParse(numeroGuia, out _))
            {
                UI_Alerta.MostrarError("Error: Solo se permiten números.", false, false);
                continue;
            }

            if (numeroGuia.Length != 10)
            {
                UI_Alerta.MostrarError("Error: El número de guía debe tener exactamente 10 dígitos.", false, false);
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
                UI_Alerta.MostrarError("Error: El campo es obligatorio.", false, false);
                continue;
            }

            if (nombre.Length < 3)
            {
                UI_Alerta.MostrarError("Error: Debe tener al menos 3 caracteres.", false, false);
                continue;
            }

            if (nombre.Length > 50)
            {
                UI_Alerta.MostrarError("Error: No puede tener más de 50 caracteres.", false, false);
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
                UI_Alerta.MostrarError("Error: Solo se permiten letras y espacios.", false, false);
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
                UI_Alerta.MostrarError("Error: El campo es obligatorio.", false, false);
                continue;
            }

            if (lugar.Length < 3)
            {
                UI_Alerta.MostrarError("Error: Debe tener al menos 3 caracteres.", false, false);
                continue;
            }

            if (lugar.Length > 100)
            {
                UI_Alerta.MostrarError("Error: No puede tener más de 100 caracteres.", false, false);
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
                UI_Alerta.MostrarError("Error: Solo se permiten letras y espacios.", false, false);
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

            UI_Alerta.MostrarError("Error: Seleccione 1 o 2.", false, false);

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
                UI_Alerta.MostrarError("Error: Debe ingresar un número entero válido.", false, false);
                continue;
            }

            if (valor <= 0)
            {
                UI_Alerta.MostrarError("Error: Debe ser mayor que cero.", false, false);
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
                UI_Alerta.MostrarError("Error: Debe ingresar un número válido.", false, false);
                continue;
            }

            if (valor <= 0)
            {
                UI_Alerta.MostrarError("Error: Debe ser mayor que cero.", false, false);
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
                UI_Alerta.MostrarError("Error: Debe ingresar un número válido.", false, false);
                continue;
            }

            if (valor < 0)
            {
                UI_Alerta.MostrarError("Error: No se permiten valores negativos.", false, false);
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

            UI_Alerta.MostrarError("Error: Ingrese únicamente 's' o 'n'.", false, false);

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
                UI_Alerta.MostrarError("Error: El código es obligatorio.", false, false);
                continue;
            }

            if (codigo.Length != 8)
            {
                UI_Alerta.MostrarError("Error: Debe tener exactamente 8 caracteres.", false, false);
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
                UI_Alerta.MostrarError("Error: Solo se permiten letras y números.", false, false);
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
                UI_Alerta.MostrarError("Error: El campo es obligatorio.", false, false);
                continue;
            }

            if (texto.Length < minimo)
            {
                UI_Alerta.MostrarError($"Error: Debe tener al menos {minimo} caracteres.", false, false);
                continue;
            }

            if (texto.Length > maximo)
            {
                UI_Alerta.MostrarError($"Error: No puede tener más de {maximo} caracteres.", false, false);
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
            return UI_Alerta.MostrarError("Error: debe ser mayor a 0", false, false);

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
                UI_Alerta.MostrarError("Error: La placa es obligatoria.", false, false);
                continue;
            }

            if (placa.Length < 6 || placa.Length > 10)
            {
                UI_Alerta.MostrarError("Error: La placa debe tener entre 6 y 10 caracteres.", false, false);
                continue;
            }

            return placa;

        } while (true);
    }
}