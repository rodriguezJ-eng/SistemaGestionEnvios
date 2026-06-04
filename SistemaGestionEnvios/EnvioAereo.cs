using System;
using System.Collections.Generic;

public sealed class EnvioAereo : Envio
{
    // Atributos
    private string _NumeroVuelo;
    private string _AeropuertoOrigen;
    private string _AeropuertoDestino;

    // Constructor

    public EnvioAereo(
    string numeroGuia,
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
    string aeropuertoDestino) : base(numeroGuia, fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
    {
        NumeroVuelo = numeroVuelo;
        AeropuertoOrigen = aeropuertoOrigen;
        AeropuertoDestino = aeropuertoDestino;
    }

    // Propiedades
    public string NumeroVuelo
    {
        get { return _NumeroVuelo; }
        set { _NumeroVuelo = value; }
    }

    public string AeropuertoOrigen
    {
        get { return _AeropuertoOrigen;}
        set { _AeropuertoOrigen = value; }
    }

    public string AeropuertoDestino
    {
        get { return _AeropuertoDestino;}
        set { _AeropuertoDestino = value; }
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

    public override decimal CalcularCostoTotal()
    {
        decimal costo = 0;
        foreach (Paquete p in Paquetes)
            costo += p.CalcularCostoBase();

        costo *= 2.5m;
        return costo;

    }

    public override string CalcularTiempoEntrega()
    {
        return "1 a 3 dias habiles";
    }

    public override string ObtenerInformacion()
    {
        return $"[{TipoEnvio()}] Guia: {NumeroGuia} | Vuelo: " +
            $"{NumeroVuelo} | {AeropuertoOrigen} -> {AeropuertoDestino}";
    }

    public override string TipoEnvio()
    {
        return "Aereo";
    }

    protected override void GenerarNumeroGuia()
    {
        NumeroGuia = $"AER-{DateTime.Now:yyyyMMddHHmmss}";
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine($"  Vuelo         : {NumeroVuelo}");
        Console.WriteLine($"  Aerop. Origen : {AeropuertoOrigen}");
        Console.WriteLine($"  Aerop. Destino: {AeropuertoDestino}");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
    }

}
