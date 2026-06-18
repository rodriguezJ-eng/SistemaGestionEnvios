using System;
using System.Collections.Generic;

/// <summary>
/// Representa un envío logístico a nivel continental o nacional mediante vehículos de carga vial.
/// Esta clase es sellada.
/// </summary>
public sealed class EnvioTerrestre : Envio
{
    private int _DistanciaKm;
    private string _PlacaCamion;
    private string _Direccion;

    /// <summary>
    /// Inicializa una nueva instancia de la clase con valores por defecto.
    /// Requerido por el motor de serialización XML.
    /// </summary>
    public EnvioTerrestre() : base()
    {
        _PlacaCamion = string.Empty;
        _Direccion = string.Empty;
    }


    public EnvioTerrestre(
    DateTime fechaEnvio,
    string origen,
    string destino,
    string estado,
    List<Paquete> paquetes,
    string categoriaEnvio,
    string remitente,
    string destinatario, 
    int distanciaKm, 
    string placaCamion, 
    string direccion) : base(fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        DistanciaKm = distanciaKm;
        PlacaCamion = placaCamion;
        Direccion = direccion;
        GenerarNumeroGuia();
    }

    public int DistanciaKm
    {
        get => _DistanciaKm;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "La distancia debe ser mayor que cero.");

            _DistanciaKm = value;
        }
    }
    public string PlacaCamion
    {
        get => _PlacaCamion;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Placa obligatoria.");

            if (value.Length < 6 || value.Length > 10)
                throw new ArgumentException("La placa debe tener entre 6 y 10 caracteres.");

            _PlacaCamion = value;
        }
    }

    public string Direccion
    {
        get => _Direccion;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Dirección obligatoria.");

            if (value.Length < 3)
                throw new ArgumentException("La dirección debe tener al menos 3 caracteres.");

            if (value.Length > 100)
                throw new ArgumentException("La dirección no puede superar los 100 caracteres.");

            _Direccion = value;
        }
    }

    /// <summary>
    /// Devuelve la etiqueta descriptiva del tipo de transporte actual.
    /// </summary>
    /// <returns>Cadena de texto "Terrestre".</returns>
    public override string TipoEnvio() => "Terrestre";

    /// <summary>
    /// Genera el código alfanumérico único para transportes viales con prefijo "TER".
    /// </summary>
    protected override void GenerarNumeroGuia()
    {
        string randomNumb = new Random().Next(1000, 9999).ToString();
        NumeroGuia = $"TER-{DateTime.Now:yyyyMMddHHmmss}-{randomNumb}";
    }

    /// <summary>
    /// Calcula el costo basándose en una tasa de seguro del 1.5% y un algoritmo exponencial amortiguado 
    /// que evalúa la distancia recorrida multiplicada por el peso facturable total acumulado de la carga.
    /// </summary>
    /// <returns>Costo total calculado.</returns>
    public override decimal CalcularCostoTotal()
    {
        decimal costoPaquetes = 0;
        decimal pesoFacturableTotal = 0;

        // Procesamiento y acumulación de cada paquete individual 
        foreach (Paquete p in Paquetes)
        {
            // Acumulación de los costos base 
            // prima de seguro terrestre se aplica tasa del 1.5% sobre el valor declarado
            costoPaquetes += p.CalcularCostoBase();
            costoPaquetes += p.CalcularCargoSeguro(0.015m); // 1.5% terrestre (menor riesgo)

            // Consolidación peso factorible
            pesoFacturableTotal += (decimal)p.PesoFacturable();
        }

        // Tarifa de flete: C$8 por km
        decimal tarifaPorKmPorKg = 8.00m;

        // Multiplica la distancia por la tarifa y el peso acumulado.
        // Se utiliza Math.Max para asegurar que el peso mínimo multiplicador sea 1m y evitar fletes en cero.
        // Se divide entre 10m como factor de amortiguación para evitar un escalado excesivo en rutas de larga distancia.
        decimal costoDistancia = DistanciaKm * tarifaPorKmPorKg * Math.Max(pesoFacturableTotal, 1m) / 10m;
        

        return Math.Round(costoPaquetes + costoDistancia, 2);
    }

    /// <summary>
    /// Actualiza el estado operativo actual mediante la lectura interactiva de flujos de consola.
    /// </summary>
    public override void ActualizarEstado()
    {
        Console.WriteLine("  Estados disponibles:");
        Console.WriteLine("  1. Pendiente");
        Console.WriteLine("  2. En transito");
        Console.WriteLine("  3. Entregado");
        Console.WriteLine("  4. Cancelado");
        Console.Write("  Seleccione: ");
        string? opcion = Console.ReadLine()?.Trim();

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
    /// Muestra la información del envío anexando el módulo de datos de carreteras y destinos viales.
    /// </summary>
    public override void MostrarInformacionEnvio()
    {
        base.MostrarInformacionEnvio();
        string Encabezado = $"{new string('=', 5)} Datos Terrestre {new string('=', 32)}";
        Console.WriteLine($"{Encabezado}");
        Console.WriteLine($"  Placa Camion  : {PlacaCamion}");
        Console.WriteLine($"  Dirección     : {Direccion}");
        Console.WriteLine($"  Distancia     : {DistanciaKm} km");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
        Console.WriteLine($"{new string('=', Encabezado.Length)}\n");
    }

    /// <summary>
    /// Estima los días hábiles requeridos de acuerdo a rangos fijos de kilometraje vial.
    /// </summary>
    /// <returns>Rango de días en formato de texto legible.</returns>
    public override string CalcularTiempoEntrega()
    {
    
        if (DistanciaKm <= 100)
            return "1 día";

        if (DistanciaKm <= 500)
            return "2 a 3 días";

        if (DistanciaKm <= 1000)
            return "4 a 5 días";

        return "Más de 5 días";
    }
}
