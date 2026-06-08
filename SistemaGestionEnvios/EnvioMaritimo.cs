using System;
using System.Collections.Generic;

public sealed class EnvioMaritimo : Envio
{
    // Atributos
    private string _NombreBarco;
    private string _PuertoOrigen;
    private string _PuertoDestino;
    private int _DiasNavegacion;

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
        get { return _NombreBarco; }
        set { _NombreBarco = value; }
    }

    public string PuertoOrigen
    {
        get { return _PuertoOrigen; }
        set { _PuertoOrigen = value; }
    }

    public string PuertoDestino
    {
        get { return _PuertoDestino; }
        set { _PuertoDestino = value; }
    }

    public int DiasNavegacion
    {
        get { return _DiasNavegacion; }
        set { _DiasNavegacion = value; }
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
        foreach (Paquete p in Paquetes)
            costo += p.CalcularCostoBase();

        costo += DiasNavegacion * 15000;
        return costo;

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

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine($"  Barco         : {NombreBarco}");
        Console.WriteLine($"  Puerto Origen : {PuertoOrigen}");
        Console.WriteLine($"  Puerto Destino: {PuertoDestino}");
        Console.WriteLine($"  Dias Naveg.   : {DiasNavegacion} dias");
        Console.WriteLine($"  Tiempo Entrega: {CalcularTiempoEntrega()}");
    }
}