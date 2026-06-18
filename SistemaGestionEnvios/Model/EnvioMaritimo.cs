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

    /// <summary>
    /// Calcula el costo total de un envío marítimo en función del volumen del paquete (metros cúbicos),
    /// las tarifas de seguro específicas, una regla de flete mínimo y los costos operativos diarios de navegación.
    /// </summary>
    /// <returns>El costo total acumulado del envío marítimo en unidades monetarias (C$), redondeado a 2 decimales.</returns>
    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;
        decimal volumenTotalM3 = 0;

        // procesamiento individual de cada paquete 
        foreach (Paquete p in Paquetes)
        {
            // acumulación del costo base operativo de cada parque 
            costo += p.CalcularCostoBase();

            // prima de seguro, se aplica una tasa baja del %1 debido a que por barco
            // se reduce el riesgo de siniestros por colici´no o manipulación brusca
            costo += p.CalcularCargoSeguro(0.01m); 

            // Conversió métrica, se calcula el volumen individual y se divide entre 1,000,000
            // para transformar las dimensiones de cm^3 a m^3
            volumenTotalM3 += (decimal)(p.CalcularVolumen() / 1_000_000); // cm³ a m³
        }

        // Se multiplica el volumen por la tarifa, pero asegura un cobro
        // minimo de C$ 1,500 para amortizar los costos fijos de consolidación.
        decimal tarifaPorM3 = 3500.00m; // C$3500 por m³, tarifa de flete marítimo típica
        decimal costoFlete = Math.Max(volumenTotalM3 * tarifaPorM3, 1500.00m); // mínimo de flete

        // costos operativos 
        decimal costoTransito = DiasNavegacion * 80.00m; // costo menor, por combustible/operación diaria

        // Costo total 
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