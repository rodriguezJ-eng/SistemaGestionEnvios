// Clase principal, clase abstracta
using System.Data;
using System.Runtime.CompilerServices;

abstract class Envio
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
    protected Envio(string _NumeroGuia, DateTime _FechaEnvio, string _Origen, string _Destino, string _Estado, List<Paquete> _Paquetes, string _CategoriaEnvio, string _Remitente, string _Destinatario)
    {
        NumeroGuia = _NumeroGuia;
        FechaEnvio = _FechaEnvio;
        Origen = _Origen;
        Destino = _Destino;
        Estado = _Estado;
        Paquetes = _Paquetes;
        CategoriaEnvio = _CategoriaEnvio;
        Remitente = _Remitente;
        Destinatario = _Destinatario;
    }

    // Propiedades

    public string NumeroGuia
    {
        get { return _NumeroGuia; }
        set { _NumeroGuia = value; }
    }

    public string FechaEnvio
    {
        get { return _FechaEnvio.ToString("dd/MM/yyyy HH:mm"); }
        set
        {
            if (DateTime.TryParse(value, out DateTime fecha))
            {
                _FechaEnvio = fecha;
            }
            else
            {
                throw new ArgumentException("Fecha de envío no válida. Use el formato dd/MM/yyyy HH:mm");
            }
        }
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
        Console.WriteLine($"  Código        : {NumeroPaquete}");
        Console.WriteLine($"  Tipo          : {TipoEnvio()}");
        Console.WriteLine($"  Remitente     : {Remitente}");
        Console.WriteLine($"  Destinatario  : {Destinatario}");
        Console.WriteLine($"  Estado        : {Estado}");
        Console.WriteLine($"  Fecha Reg.    : {FechaRegistro}");
        Console.WriteLine($"  Costo         : ${CalcularCostoTotal():F2}");
        Console.WriteLine("===== Paquete ==============================");
    }

    public abstract void ActualizarEstado();

    public abstract Decimal CalcularCostoTotal();

    protected abstract void GenerearNumeroGuia();

    public abstract string ObtenerInformacion();

    public abstract List<T> TipoEnvio();

    public abstract string CalcularTiempoEntrega();
}