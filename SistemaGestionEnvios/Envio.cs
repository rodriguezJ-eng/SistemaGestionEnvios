// Clase principal, clase abstracta
using System;
using System.Collections.Generic;

public abstract class Envio
{
    //Atributos 
    private string _NumeroGuia;
    private DateTime _FechaEnvio;
    private string _Origen;
    private string _Destino;
    private string _Estado;
    private List<Paquete> _Paquetes; // Un envío puede contener uno o varios paquetes
    private string _CategoriaEnvio;
    private string _Remitente;
    private string _Destinatario;

    // definir el constructor
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

    // Propiedades

    public string NumeroGuia
    {
        get { return _NumeroGuia; }
        set { _NumeroGuia = value; }
    }

    public DateTime FechaEnvio
    {
        get { return _FechaEnvio; }
        set { _FechaEnvio = value; }
    }

    public string Origen
    {
        get { return _Origen; }
        set { _Origen = value; }
    }

    public string Destino
    {
        get { return _Destino; }
        set { _Destino = value; }
    }

    public string Estado
    {
        get { return _Estado; }
        set { _Estado = value; }
    }

    public List<Paquete> Paquetes
    {
        get { return _Paquetes; }
        set { _Paquetes = value; }
    }

    public string CategoriaEnvio
    {
        get { return _CategoriaEnvio; }
        set { _CategoriaEnvio = value; }
    }

    public string Remitente
    {
        get { return _Remitente; }
        set { _Remitente = value; }
    }

    public string Destinatario
    {
        get { return _Destinatario; }
        set { _Destinatario = value; }
    }

    // definir métodos abstractos o virtuales
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

    public abstract void ActualizarEstado();

    public abstract decimal CalcularCostoTotal();

    protected abstract void GenerarNumeroGuia();

    public abstract string ObtenerInformacion();

    public abstract string TipoEnvio();

    public abstract string CalcularTiempoEntrega();
}