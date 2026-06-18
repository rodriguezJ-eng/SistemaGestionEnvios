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
        Console.WriteLine($"Peso             : {Peso}kg");
        Console.WriteLine($"Fragil           : {(EsFragil ? "Sí" : "No")}");
        Console.WriteLine($"{new string('=', Encabezado.Length)}\n");
    }

    /// <summary>
    /// calcula el costo base de un paquete individual utilidzando tarifas escalonadas por peso y recarga por fragiilidad
    /// </summary>
    /// <param name="divisorVolumetrico"></param>
    /// <returns> El costo base total del paquete en unidaddes monetarioas redondeado a 2 decimales</returns>
    public decimal CalcularCostoBase(double divisorVolumetrico = 5000)
    {
        // tarifa fija de apertura (costos operativos mínimos: manejo, sistema y etiquetado)
        decimal tarifaBase = 50.00m; 

        // determinación del peso mayor entre el peso real de bascula y el peso volumétrico
        decimal pesoFacturable = (decimal)PesoFacturable(divisorVolumetrico);
        decimal costoPorPeso;

        // Aplicación de las tarifas
        if (pesoFacturable <= 2.0m) 
            //Tramo 1 paquetes ligeris (0.1kg a 2.0kg) tarifa estandar completa
            costoPorPeso = pesoFacturable * 25.00m;       // C$25/kg
        else if (pesoFacturable <= 10.0m)
            // Tramo 2 paquete medianos De 2 kg a 10 kg C
            // Los primeros 2 kg a tarifa estándar y el excedente a tarifa con descuento.
            costoPorPeso = (2.0m * 25.00m) + ((pesoFacturable - 2.0m) * 18.00m);
        else
            // Tramo 3: Paquetes pesados (Más de 10 kg).
            // Bloques fijos para los primeros 10 kg y tarifa de superdescuento para el exceso masivo.
            costoPorPeso = (2.0m * 25.00m) + (8.0m * 18.00m) + ((pesoFacturable - 10.0m) * 12.00m);

        // Consolidado del costo operativo y el costo por peso
        decimal costoTotal = tarifaBase + costoPorPeso;

        // Recargo por fragilidad, hay un incremento del 15% sobre el subtotal por manejo especial de la mercancia
        if (EsFragil)
            costoTotal += costoTotal * 0.15m;

        return Math.Round(costoTotal, 2);
    }

    /// <summary>
    /// Calcula el Peso teórico del paquete con base el espacio físico que ocupa (volumen en cm^3)
    /// Divisor 5000 es el más común para aéreo/terrestre; cada modo puede usar su propio divisor.
    /// </summary>
    /// /// <param name="divisor">El factor de conversión logística (por defecto 5000 para estándar aéreo/terrestre).</param>
    /// <returns>El peso volumétrico calculado en kilogramos (kg).</returns>
    public double CalcularPesoVolumetrico(double divisor = 5000)
    {
        // Fórmula estándar internacional de la industria : Volumen / Divisor
        return CalcularVolumen() / divisor;
    }

    /// <summary>
    /// Evalúa y selecciona el peso definitivo sobre el cual se aplicará el cobro de la tarifa.
    /// </summary>
    /// <param name="divisor">El factor de conversión empleado para el cálculo del peso volumétrico (por defecto 5000).</param>
    /// <returns>El valor máximo (en kg) entre el peso real de la báscula y el peso por dimensiones.</returns>
    public double PesoFacturable(double divisor = 5000)
    {
        // Se factura el impacto que resulte mayor para el transporte
        return Math.Max(Peso, CalcularPesoVolumetrico(divisor));
    }

    /// <summary>
    /// Calcula el costo del seguro de protección de la mercancía de manera proporcional a su valor declaradol.
    /// </summary>
    /// <param name="porcentaje">La tasa de riesgo aplicable expresada en decimal (por defecto 0.02 correspondiente al 2%).</param>
    /// <returns>El costo final del seguro, asegurando una tasa de cobro mínima obligatoria de C$10.00.</returns>
    public decimal CalcularCargoSeguro(decimal porcentaje = 0.02m)
    {
        decimal minimo = 10.00m; // cargo mínimo por procesar el seguro de C$10
        decimal cargo = ValorDeclarado * porcentaje;
        // Retorna el valor más alto entre el cálculo porcentual y la tarifa mínima requerida
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