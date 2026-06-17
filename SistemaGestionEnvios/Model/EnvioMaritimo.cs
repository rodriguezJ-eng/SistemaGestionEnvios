using System;
using System.Collections.Generic;

public sealed class EnvioMaritimo : Envio
{
    // Atributos
    private string _NombreBarco;
    private string _PuertoOrigen;
    private string _PuertoDestino;
    private int _DiasNavegacion;

    // Requerido por XmlSerializer 
    public EnvioMaritimo() : base()
    {
        _NombreBarco = string.Empty;
        _PuertoOrigen = string.Empty;
        _PuertoDestino = string.Empty;
    }

    // Constructor

    public EnvioMaritimo(
    DateTime fechaEnvio,
    string origen,
    string destino,
    string estado,
    List<Paquete> paquetes,
    string categoriaEnvio,
    string remitente,
    string destinatario, 
    string nombreBarco, 
    string puertoOrigen, 
    string puertoDestino, 
    int diasNavegacion) : base(fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        NombreBarco = nombreBarco;
        PuertoOrigen = puertoOrigen;
        PuertoDestino = puertoDestino;
        DiasNavegacion = diasNavegacion;
        GenerarNumeroGuia();
    }

    // Propiedades
    public string NombreBarco
    {
        get => _NombreBarco;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nombre del barco obligatorio.");

            if (value.Length < 3 || value.Length > 50)
                throw new ArgumentException("Nombre del barco inválido.");

            _NombreBarco = value;
        }
    }

    public string PuertoOrigen
    {
        get => _PuertoOrigen;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Puerto de origen obligatorio.");

            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("Puerto de origen inválido.");

            _PuertoOrigen = value;
        }
    }

    public string PuertoDestino
    {
        get => _PuertoDestino;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Puerto de destino obligatorio.");

            if (value.Length < 3 || value.Length > 100)
                throw new ArgumentException("Puerto de destino inválido.");

            _PuertoDestino = value;
        }
    }

    public int DiasNavegacion
    {
        get => _DiasNavegacion;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Los días de navegación deben ser mayores que cero.");

            _DiasNavegacion = value;
        }
    }

    public override string TipoEnvio() => "Maritimo";

    /// <summary>
    /// Genera el número de guía con prefijo MAR, timestamp y 4 dígitos aleatorios.
    /// </summary>
    protected override void GenerarNumeroGuia()
    {
        string randomNumb = new Random().Next(1000, 9999).ToString();
        NumeroGuia = $"MAR-{DateTime.Now:yyyyMMddHHmmss}-{randomNumb}";
    }

    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;
        decimal volumenTotalM3 = 0;

        foreach (Paquete p in Paquetes)
        {
            costo += p.CalcularCostoBase();
            costo += p.CalcularCargoSeguro(0.01m); // 1% marítimo (más lento, pero más estable)
            volumenTotalM3 += (decimal)(p.CalcularVolumen() / 1_000_000); // cm³ a m³
        }

        decimal tarifaPorM3 = 3500.00m; // C$3500 por m³, tarifa de flete marítimo típica
        decimal costoFlete = Math.Max(volumenTotalM3 * tarifaPorM3, 1500.00m); // mínimo de flete

        decimal costoTransito = DiasNavegacion * 80.00m; // costo menor, por combustible/operación diaria

        return Math.Round(costo + costoFlete + costoTransito, 2);
    }

    public override string CalcularTiempoEntrega() => $"{DiasNavegacion} dia(s) de navegacion";

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

    public override void MostrarInformacionEnvio()
    {
        base.MostrarInformacionEnvio();
        string Encabezado = $"{new string('=', 5)} Datos Marítimos {new string('=', 32)}";
        Console.WriteLine($"{Encabezado}");
        Console.WriteLine($"  Barco         : {NombreBarco}");
        Console.WriteLine($"  Puerto Origen : {PuertoOrigen}");
        Console.WriteLine($"  Puerto Destino: {PuertoDestino}");
        Console.WriteLine($"  Dias Naveg.   : {DiasNavegacion} dias");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
        Console.WriteLine($"{new string('=', Encabezado.Length)}\n");
    }
}