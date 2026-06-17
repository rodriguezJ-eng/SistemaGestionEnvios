using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public sealed class EnvioAereo : Envio
{
    // Atributos
    private string _NumeroVuelo;
    private string _AeropuertoOrigen;
    private string _AeropuertoDestino;

    // Requerido por XmlSerializer
    public EnvioAereo() : base()
    {
        _NumeroVuelo = string.Empty;
        _AeropuertoOrigen = string.Empty;
        _AeropuertoDestino = string.Empty;
    }

    // Constructor

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

    public override string TipoEnvio() => "Aereo";


    /// <summary>
    /// Genera el numero de guia con el prefijo AER, y 4 dijitos aleatorios.
    /// </summary>
    protected override void GenerarNumeroGuia()
    {   
        string randomNumb = new Random().Next(1000,9999).ToString();
        NumeroGuia = $"AER-{DateTime.Now:yyyyMMddHHmmss}-{randomNumb}";
    }

    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;

        foreach (Paquete p in Paquetes)
        {
            // Divisor más estricto para aéreo: el espacio en avión es más caro
            decimal pesoFacturable = (decimal)p.PesoFacturable(divisor: 6000);
            decimal tarifaAerea = pesoFacturable * 180.00m; // C$180/kg facturable, tarifa internacional típica

            costo += tarifaAerea;
            costo += p.CalcularCargoSeguro(0.03m); // 3% aéreo (mayor valor de mercadería típica)

            if (p.EsFragil)
                costo += tarifaAerea * 0.15m;
        }

        return Math.Round(costo, 2);
    }

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
