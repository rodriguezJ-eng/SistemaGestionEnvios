using System;
using System.Collections.Generic;

public sealed class EnvioTerrestre : Envio
{
    // Atributos
    private int _DistanciaKm;
    private string _PlacaCamion;
    private string _Ruta;

    // Requerido por XmlSerializer
    public EnvioTerrestre() : base()
    {
        _PlacaCamion = string.Empty;
        _Ruta = string.Empty;
    }

    // Constructor

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
    string ruta) : base(fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        DistanciaKm = distanciaKm;
        PlacaCamion = placaCamion;
        Ruta = ruta;
        GenerarNumeroGuia();
    }

    // Propiedades
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

    public string Ruta
    {
        get => _Ruta;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ruta obligatoria.");

            if (value.Length < 3)
                throw new ArgumentException("La ruta debe tener al menos 3 caracteres.");

            if (value.Length > 100)
                throw new ArgumentException("La ruta no puede superar los 100 caracteres.");

            _Ruta = value;
        }
    }

    public override string TipoEnvio() => "Terrestre";

    /// <summary>
    /// Genera el número de guía con prefijo TER, timestamp y 4 dígitos aleatorios.
    /// </summary>
    protected override void GenerarNumeroGuia()
    {
        string randomNumb = new Random().Next(1000, 9999).ToString();
        NumeroGuia = $"TER-{DateTime.Now:yyyyMMddHHmmss}-{randomNumb}";
    }

    /// <summary>
    /// Calcula el costo: costo base de paquetes + Tarifa por km
    /// </summary>
    /// <returns></returns>
    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;
        foreach (Paquete p in Paquetes)
        {
            costo += p.CalcularCostoBase();
        }

        costo += DistanciaKm * 500;
        return costo;
    }

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


    // Muestra la informacion del envio incluyendo datos propios
    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine($"  Placa Camion  : {PlacaCamion}");
        Console.WriteLine($"  Ruta          : {Ruta}");
        Console.WriteLine($"  Distancia     : {DistanciaKm} km");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
    }

    
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
