/// <summary>
/// Representa un paquete fisico dentro de un envio.
/// Contiene las caracteristicas del objeto a transportar.
/// </summary>

public class Paquete
{
    
    private string? _CodigoPaquete;
    private string? _Contenido;
    private bool _EsFragil;
    private decimal _ValorDeclarado; // valor monetario declarado por el cliente, en caso de pérdida o daño, se reembolsa este valor.
    private string? _TipoPaquete;  // pequeño, mediano, grande, monetario, electrónico, perecedero, etc.
    private double _Peso; // en kg
    private double _Largo;
    private double _Alto;
    private double _Ancho;

    // Requerido por XmlSerializer 
    public Paquete()
    {
        _CodigoPaquete = string.Empty;
        _Contenido = string.Empty;
        _TipoPaquete = string.Empty;
    }

    public Paquete(string codigoPaquete, string contenido, bool esFragil, decimal valorDeclarado, string tipoPaquete, double peso, double largo, double alto, double ancho)
    {
        CodigoPaquete = codigoPaquete;
        Contenido = contenido;
        EsFragil = esFragil;
        ValorDeclarado = valorDeclarado;
        TipoPaquete = tipoPaquete;
        Peso = peso;
        Largo = largo;
        Alto = alto;
        Ancho = ancho;
    }

    public string? CodigoPaquete
    {
        get => _CodigoPaquete;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Código de paquete obligatorio.");

            if (value.Length != 8)
                throw new ArgumentException("El código debe tener 8 caracteres.");

            _CodigoPaquete = value;
        }
    }

    public string? Contenido
    {
        get => _Contenido;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Contenido obligatorio.");

            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("Contenido inválido.");

            _Contenido = value;
        }
    }

    public bool EsFragil
    {
        get => _EsFragil;
        set => _EsFragil = value;
    }

    public decimal ValorDeclarado
    {
        get => _ValorDeclarado;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "El valor declarado no puede ser negativo.");

            _ValorDeclarado = value;
        }
    }

    public string? TipoPaquete
    {
        get => _TipoPaquete;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tipo de paquete obligatorio.");

            _TipoPaquete = value;
        }
    }

    public double Peso
    {
        get => _Peso;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "El peso debe ser mayor que cero.");

            _Peso = value;
        }
    }

    public double Largo
    {
        get => _Largo;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "El largo debe ser mayor que cero.");

            _Largo = value;
        }
    }

    public double Alto
    {
        get => _Alto;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "El alto debe ser mayor que cero.");

            _Alto = value;
        }
    }

    public double Ancho
    {
        get => _Ancho;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "El ancho debe ser mayor que cero.");

            _Ancho = value;
        }
    }

    // Métodos

    /// <summary>
    /// Calcula el volumen del paquete multiplicando sus tres dimensiones.
    /// </summary>
    /// <returns></returns>

    public double CalcularVolumen()
    {
        return Alto * Ancho * Largo;
    }

    /// <summary>
    /// Muestra en consola la informacion principal del paquete.
    /// </summary>

    public void MostrarInformacionPaquete(int num)
    {
        int longitudDerecha = 27 - num.ToString().Length + 1;
        string Encabezado = $"{new string('=', 5)} Datos del Paquete #{num} {new string('=', longitudDerecha)}";
        Console.WriteLine($"{Encabezado}");
        Console.WriteLine($"Codigo           : {CodigoPaquete}");
        Console.WriteLine($"Contenido        : {Contenido}");
        Console.WriteLine($"Valor Declarado  : {ValorDeclarado}");
        Console.WriteLine($"Tipo Paquete     : {TipoPaquete}");
        Console.WriteLine($"Peso             : {Peso}");
        Console.WriteLine($"Fragil           : {(EsFragil ? "Sí" : "No")}");
        Console.WriteLine($"{new string('=', Encabezado.Length)}\n");
    }

    public decimal CalcularCostoBase(double divisorVolumetrico = 5000)
    {
        decimal tarifaBase = 50.00m; // C$50 córdobas, cubre manejo y etiquetado

        decimal pesoFacturable = (decimal)PesoFacturable(divisorVolumetrico);
        decimal costoPorPeso;

        if (pesoFacturable <= 2.0m)
            costoPorPeso = pesoFacturable * 25.00m;       // C$25/kg
        else if (pesoFacturable <= 10.0m)
            costoPorPeso = (2.0m * 25.00m) + ((pesoFacturable - 2.0m) * 18.00m);
        else
            costoPorPeso = (2.0m * 25.00m) + (8.0m * 18.00m) + ((pesoFacturable - 10.0m) * 12.00m);

        decimal costoTotal = tarifaBase + costoPorPeso;

        if (EsFragil)
            costoTotal += costoTotal * 0.15m;

        return Math.Round(costoTotal, 2);
    }

    /// <summary>
    /// Peso volumétrico estándar usado en la industria: volumen (cm³) / divisor.
    /// Divisor 5000 es el más común para aéreo/terrestre; cada modo puede usar su propio divisor.
    /// </summary>
    public double CalcularPesoVolumetrico(double divisor = 5000)
    {
        return CalcularVolumen() / divisor;
    }

    public double PesoFacturable(double divisor = 5000)
    {
        return Math.Max(Peso, CalcularPesoVolumetrico(divisor));
    }

    /// <summary>
    /// Cargo de seguro proporcional al valor declarado. Práctica estándar: 1%-5% del valor.
    /// </summary>
    public decimal CalcularCargoSeguro(decimal porcentaje = 0.02m)
    {
        decimal minimo = 10.00m; // cargo mínimo por procesar el seguro
        decimal cargo = ValorDeclarado * porcentaje;
        return Math.Round(Math.Max(cargo, minimo), 2);
    }

    /// <summary>
    /// Clasifica el paquete segun su peso en liviano, normal o pesado.
    /// </summary>
    /// <returns>Categoria del paquete segun su peso.</returns>
    public string CategoriaPeso()
    {
        if (Peso <= 1)
            return "Liviano";
        else if (Peso <= 10)
            return "Normal";
        else
            return "Pesado";
    }

}