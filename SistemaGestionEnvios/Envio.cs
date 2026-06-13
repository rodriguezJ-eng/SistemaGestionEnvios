/// <summary>
/// Clase base abstracta que representa un envio generico.
/// Define los atributos y comportamientos comunes a todos los tipos de envio.
/// </summary>
using System;
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

    public Envio(string numeroGuia, DateTime fechaEnvio, string origen, string destino, string estado, List<Paquete> paquetes, string categoriaEnvio, string remitente, string destinatario)
    {
        NumeroGuia = numeroGuia;
        FechaEnvio = fechaEnvio;
        Origen = origen;
        Destino = destino;
        Estado = estado;
        Paquetes = paquetes;
        CategoriaEnvio = categoriaEnvio;
        Remitente = remitente;
        Destinatario = destinatario;
    }

    public string? NumeroGuia
    {
        get => _NumeroGuia;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Número de guía obligatorio.");

            if (value.Length != 10)
                throw new ArgumentException("Debe tener exactamente 10 dígitos.");

            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("Solo se permiten números.");
            }

            _NumeroGuia = value;
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

    public virtual void MostrarInformacion()
    {
        Console.WriteLine($"  Código        : {NumeroGuia}");
        Console.WriteLine($"  Tipo          : {TipoEnvio()}");
        Console.WriteLine($"  Remitente     : {Remitente}");
        Console.WriteLine($"  Destinatario  : {Destinatario}");
        Console.WriteLine($"  Estado        : {Estado}");
        Console.WriteLine($"  Fecha Reg.    : {FechaEnvio.ToString("dd/MM/yyyy HH:mm")}");
        Console.WriteLine($"  Costo         : ${CalcularCostoTotal():F2}");
        Console.WriteLine("===== Paquete ==============================");
        foreach (Paquete paquete in Paquetes)
        {
            paquete.MostrarInformacion();
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
    /// Genera el numero de guia automaticamente con el prefijo del tipo de envio.
    /// </summary>
    protected abstract void GenerarNumeroGuia();

    /// <summary>
    /// Retorna un resumen del envio en una sola linea de texto.
    /// </summary>
    /// <returns>Cadena con la informacion resumida del envio.</returns>
    public abstract string ObtenerInformacion();

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

