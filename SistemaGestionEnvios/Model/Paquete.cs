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

    public void MostrarInformacionPaquete()
    {
        Console.WriteLine("===== Paquete ==============================");
        Console.WriteLine($"Codigo           : {CodigoPaquete}");
        Console.WriteLine($"Contenido        : {Contenido}");
        Console.WriteLine($"Valor Declarado  : {ValorDeclarado}");
        Console.WriteLine($"Tipo Paquete     : {TipoPaquete}");
        Console.WriteLine($"Peso             : {Peso}");
        Console.WriteLine($"Fragil           : {(EsFragil ? "Sí" : "No")}");
        Console.WriteLine("===================================\n");
    }

    /// <summary>
    /// Calcula el costo base del paquete segun su peso. Agrega un recargo si es fragil.
    /// </summary>
    /// <returns>Costo base como valor decimal.</returns>
    public decimal CalcularCostoBase()
    {
        decimal costo = (decimal)Peso * 10;

        if (EsFragil)
        {
            costo += 50;
        }

        return costo;
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