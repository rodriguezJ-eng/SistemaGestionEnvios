/// <summary>
/// Clase base abstracta que representa un envio generico.
/// Define los atributos y comportamientos comunes a todos los tipos de envio.
/// </summary>
using System;
using System.Xml.Serialization;

[XmlInclude(typeof(EnvioTerrestre))]
[XmlInclude(typeof(EnvioMaritimo))]
[XmlInclude(typeof(EnvioAereo))]
public abstract class Envio
{
    private string _NumeroGuia;
    private DateTime _FechaEnvio;
    private string _Origen;
    private string _Destino;
    private string _Estado;
    private List<Paquete> _Paquetes; // Un envío puede contener uno o varios paquetes
    private string _CategoriaEnvio;
    private string _Remitente;
    private string _Destinatario;

    // Constructor vacio para el Xml
    protected Envio()
    {
        _NumeroGuia = string.Empty;
        _Origen = string.Empty;
        _Destino = string.Empty;
        _Estado = string.Empty;
        _CategoriaEnvio = string.Empty;
        _Remitente = string.Empty;
        _Destinatario = string.Empty;
        _Paquetes = new List<Paquete>();
    }

    public Envio(DateTime fechaEnvio, string origen, string destino, string estado, List<Paquete> paquetes, string categoriaEnvio, string remitente, string destinatario)
    {
        FechaEnvio = fechaEnvio;
        Origen = origen;
        Destino = destino;
        Estado = estado;
        Paquetes = paquetes;
        CategoriaEnvio = categoriaEnvio;
        Remitente = remitente;
        Destinatario = destinatario;
        // Nota: GenerearNumeroGuia() se llama desde el constructuro de cada clase hija
        // Despues de que base() asigna los demás campos.
    }

    public string? NumeroGuia
    {
        get { return _NumeroGuia; }
        // El setter es internal: solo las clases del mismo ensamblado (las hijas) pueden asignarlo.
        // El usuario nunca lo toca directamente.
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El número de guía es obligatorio.");
            _NumeroGuia = value.Trim();
        }

    }
    public DateTime FechaEnvio
    {
        get => _FechaEnvio;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("La fecha de envío no puede ser futura.");

            _FechaEnvio = value;
        }
    }
    public string? Origen
    {
        get => _Origen;
        set => _Origen = ValidarLugar(value, "origen");
    }
    public string Destino
    {
        get => _Destino;
        set => _Destino = ValidarLugar(value, "destino");
    }

    public string Estado
    {
        get => _Estado;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Estado obligatorio.");

            _Estado = value;
        }
    }

    public List<Paquete> Paquetes
    {
        get => _Paquetes;
        set
        {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Debe existir al menos un paquete.");

            _Paquetes = value;
        }
    }
    public string CategoriaEnvio
    {
        get => _CategoriaEnvio;
        set
        {
            if (value != "Nacional" && value != "Internacional")
                throw new ArgumentException("Categoría inválida.");

            _CategoriaEnvio = value;
        }
    }
    public string Remitente
    {
        get => _Remitente;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Remitente obligatorio.");

            _Remitente = value;
        }
    }
    public string Destinatario

    {
        get { return _Destinatario; }
        set { _Destinatario = value; }
    }

    private string ValidarLugar(string value, string campo)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{campo} obligatorio.");

        if (value.Length < 3)
            throw new ArgumentException($"{campo} debe tener al menos 3 caracteres.");

        if (value.Length > 100)
            throw new ArgumentException($"{campo} no puede superar los 100 caracteres.");

        foreach (char c in value)
        {
            if (!char.IsLetter(c) && c != ' ')
                throw new ArgumentException($"{campo} solo puede contener letras y espacios.");
        }

        return value;
    }

    /// <summary>
    /// Muestra en consola toda la informacion del envio y sus paquetes.
    /// </summary>

    public virtual void MostrarInformacionEnvio()
    {
        Console.WriteLine("\n===== Datos del Envio ================================");
        Console.WriteLine($"  Código        : {NumeroGuia}");
        Console.WriteLine($"  Tipo          : {TipoEnvio()}");
        Console.WriteLine($"  Remitente     : {Remitente}");
        Console.WriteLine($"  Destinatario  : {Destinatario}");
        Console.WriteLine($"  Estado        : {Estado}");
        Console.WriteLine($"  Fecha Registro.    : {FechaEnvio.ToString("dd/MM/yyyy HH:mm")}");
        Console.WriteLine($"  Costo         : ${CalcularCostoTotal():F2}");
        Console.WriteLine("======================================================\n");
        foreach (Paquete paquete in Paquetes)
        {
            paquete.MostrarInformacionPaquete(Paquetes.IndexOf(paquete) + 1);
        }
    }

    /// <summary>
    /// Permite al usuario actualizar el estado del envio desde consola.
    /// </summary>
    public abstract void ActualizarEstado();

    /// <summary>
    /// Calcula el costo total del envio segun el tipo y los paquetes que contiene.
    /// </summary>
    /// <returns>Costo total como valor decimal.</returns>
    public abstract decimal CalcularCostoTotal();

    /// <summary>
    /// Genera y asigna el numero de guia automaticamente con el prefijo del tipo de envio.
    /// Debe llamarse al final del constructor de cada clase hija
    /// Formato: PREFIJO-yyyyMMddHHmmss-XXXX (XXXX = 4 dijitos aleatorios para evitar conflictos)
    /// </summary>
    protected abstract void GenerarNumeroGuia();

    /// <summary>
    /// Retorna el tipo de envio: Terrestre, Maritimo o Aereo.
    /// </summary>
    /// <returns>Nombre del tipo de envio.</returns>
    public abstract string TipoEnvio();


    /// <summary>
    /// Calcula y retorna el tiempo estimado de entrega segun el tipo de envio.
    /// </summary>
    /// <returns>Descripcion del tiempo estimado de entrega.</returns>
    public abstract string CalcularTiempoEntrega();
}
