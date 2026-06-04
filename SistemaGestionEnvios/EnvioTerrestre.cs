using System;
using System.Collections.Generic;

public sealed class EnvioTerrestre : Envio
{
    // Atributos
    private int _DistanciaKm;
    private string _PlacaCamion;
    private string _Ruta;

    // Constructor

    public EnvioTerrestre(
    string numeroGuia,
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
    string ruta) : base(numeroGuia, fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        DistanciaKm = distanciaKm;
        PlacaCamion = placaCamion;
        Ruta = ruta;
    }

    // Propiedades
    public int DistanciaKm
    {
        get { return _DistanciaKm; }
        set { _DistanciaKm = value; }
    }

    public string PlacaCamion
    {
        get { return _PlacaCamion; }
        set { _PlacaCamion = value; }
    }

    public string Ruta
    {
        get { return _Ruta; }
        set { _Ruta = value; }
    }


    public override string TipoEnvio()
    {
        return "Terrestre";
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
    /// Retorna un resumen del envio en una sola linea
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override string ObtenerInformacion()
    {
        return $"[{TipoEnvio()}] Guia: {NumeroGuia} | Ruta: {Ruta} | Placa: {PlacaCamion} | {DistanciaKm} km";
    }
    /// <summary>
    /// Genera un número de guia con prefijo TER
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override void GenerarNumeroGuia()
    {
        NumeroGuia = $"TER-{DateTime.Now:yyyyMMddHHmmss}";
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
        throw new NotImplementedException();
    }
}