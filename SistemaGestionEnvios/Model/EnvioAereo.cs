using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Representa un envío logístico gestionado por transporte aéreo.
/// Esta clase es sellada para evitar herencia adicional y optimizar el rendimiento.
/// </summary>
public sealed class EnvioAereo : Envio
{
    private string _NumeroVuelo;
    private string _AeropuertoOrigen;
    private string _AeropuertoDestino;

    /// <summary>
    /// Inicializa una nueva instancia de la clase con valores predeterminados.
    /// Requerido para los procesos de serialización y deserialización.
    /// </summary>
    public EnvioAereo() : base()
    {
        _NumeroVuelo = string.Empty;
        _AeropuertoOrigen = string.Empty;
        _AeropuertoDestino = string.Empty;
    }


    public EnvioAereo(
    DateTime fechaEnvio,
    string origen,
    string destino,
    string estado,
    List<Paquete> paquetes,
    string categoriaEnvio,
    string remitente,
    string destinatario, 
    string numeroVuelo,
    string aeropuertoOrigen,  
    string aeropuertoDestino) : base(fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        NumeroVuelo = numeroVuelo;
        AeropuertoOrigen = aeropuertoOrigen;
        AeropuertoDestino = aeropuertoDestino;
        GenerarNumeroGuia();
    }

    // Propiedades


    public string NumeroVuelo
    {
        get => _NumeroVuelo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Número de vuelo obligatorio.");

            if (value.Length < 3 || value.Length > 15)
                throw new ArgumentException("El número de vuelo debe tener entre 3 y 15 caracteres.");

            _NumeroVuelo = value;
        }
    }
    public string AeropuertoOrigen
    {
        get => _AeropuertoOrigen;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Aeropuerto de origen obligatorio.");

            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("El aeropuerto de origen debe tener entre 3 y 100 caracteres.");

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && c != ' ')
                    throw new ArgumentException("El aeropuerto de origen solo puede contener letras y espacios.");
            }

            _AeropuertoOrigen = value;
        }
    }

    public string AeropuertoDestino
    {
        get => _AeropuertoDestino;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Aeropuerto de destino obligatorio.");

            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("El aeropuerto de destino debe tener entre 3 y 100 caracteres.");

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && c != ' ')
                    throw new ArgumentException("El aeropuerto de destino solo puede contener letras y espacios.");
            }

            _AeropuertoDestino = value;
        }
    }

    /// <summary>
    /// Devuelve la etiqueta descriptiva del tipo de transporte actual.
    /// </summary>
    /// <returns>Cadena de texto "Aereo".</returns>

    public override string TipoEnvio() => "Aereo";


    /// <summary>
    /// Genera de forma única el número de guía concatenando el prefijo "AER", la marca de tiempo exacta del sistema y 4 dígitos aleatorios.
    /// </summary>
    protected override void GenerarNumeroGuia()
    {   
        string randomNumb = new Random().Next(1000,9999).ToString();
        NumeroGuia = $"AER-{DateTime.Now:yyyyMMddHHmmss}-{randomNumb}";
    }

    /// <summary>
    /// Calcula el costo total del flete aplicando un divisor volumétrico estricto de 6000 para el aire, 
    /// un recargo del 3% sobre el valor declarado y una tasa de fragilidad del 15%.
    /// </summary>
    /// <returns>Monto total en moneda local (C$) con dos decimales redondeados.</returns>
    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;
        
        // Procesamiento individual de cada paquete dentro del contenedor o guía aérea
        foreach (Paquete p in Paquetes)
        {
            // Se utiliza un divisor volumétrico más estricto (6000) debido a la limitación crítica de espacio en cabina de carga
            decimal pesoFacturable = (decimal)p.PesoFacturable(divisor: 6000);

            decimal tarifaAerea = pesoFacturable * 180.00m; // C$180/kg facturable, tarifa internacional típica

            costo += tarifaAerea;
            // Se aplica una tasa del 3% (0.03) sobre el valor declarado,
            // justificado por el alto valor comercial y la prioridad de la mercancía que viaja por este medio.
            costo += p.CalcularCargoSeguro(0.03m);

            // Si el paquete es delicado,
            // se añade un 15% extra calculado estrictamente sobre el costo del flete aéreo de ese paquete.
            if (p.EsFragil)
                costo += tarifaAerea * 0.15m;
        }

        return Math.Round(costo, 2);
    }

    /// <summary>
    /// Obtiene una estimación del tiempo de entrega de la carga aérea.
    /// </summary>
    /// <returns>Cadena con el rango estimado en días.</returns>
    public override string CalcularTiempoEntrega()
    {
        return "1 a 3 dias habiles";
    }

    public override void ActualizarEstado()
    {
        Console.WriteLine("  Estados disponibles:");
        Console.WriteLine("  1. Pendiente");
        Console.WriteLine("  2. En transito");
        Console.WriteLine("  3. Entregado");
        Console.WriteLine("  4. Cancelado");
        Console.Write("  Seleccione: ");
        string opcion = Console.ReadLine()?.Trim();

        switch (opcion)
        {
            case "1": Estado = "Pendiente"; break;
            case "2": Estado = "En transito"; break;
            case "3": Estado = "Entregado"; break;
            case "4": Estado = "Cancelado"; break;
            default: Console.WriteLine("  Opcion no valida."); return;
        }
        Console.WriteLine($"  Estado actualizado a: {Estado}");
    }

    /// <summary>
    /// Imprime en la consola el desglose de datos del flete base junto a los atributos específicos del vuelo aéreo.
    /// </summary>
    public override void MostrarInformacionEnvio()
    {
        base.MostrarInformacionEnvio();
        string Encabezado = $"{new string('=', 5)} Datos Aéreos {new string('=', 35)}";
        Console.WriteLine($"{Encabezado}");
        Console.WriteLine($"  Vuelo         : {NumeroVuelo}");
        Console.WriteLine($"  Aerop. Origen : {AeropuertoOrigen}");
        Console.WriteLine($"  Aerop. Destino: {AeropuertoDestino}");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
        Console.WriteLine($"{new string('=', Encabezado.Length)}\n");
    }
}
