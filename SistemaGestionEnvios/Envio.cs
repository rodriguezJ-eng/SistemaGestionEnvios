// Clase principal, clase abstracta
using System.Runtime.CompilerServices;

public abstract class Envio
{
    // TODO:
    // definir los atributos(ENCAPSULAR) y propiedades
    private string _numeroPaquete;
    private string _remitente;
    private string _destinatario;
    private string _estado;
    private DateTime _fechaRegistro;
    // Envio tiene un paquete ? 
    private Paquete _paquete;  // envio.Paquete.Peso

    // definir el constructor
    protected Envio(string numeroPaquete, string remitente, string destinatario, string estado, DateTime fechaRegistro, Paquete paquete)
    {
        NumeroPaquete = numeroPaquete;
        Remitente = remitente;
        Destinatario = destinatario;
        Estado = estado;
        FechaRegistro = fechaRegistro;
        Paquete = paquete;
    }

    public string NumeroPaquete { get => _numeroPaquete; set => _numeroPaquete = value; }
    public string Remitente { get => _remitente; set => _remitente = value; }
    public string Destinatario { get => _destinatario; set => _destinatario = value; }
    public string Estado { get => _estado; set => _estado = value; }
    public DateTime FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
    public Paquete Paquete { get => _paquete; set => _paquete = value; }



    // definir métodos abstractos o virtuales

    public abstract double CalcularCosto();
    public abstract string TipoEnvio();

    public virtual void MostrarInfo()
    {
        Console.WriteLine($"  Código        : {_numeroPaquete}");
        Console.WriteLine($"  Tipo          : {TipoEnvio()}");
        Console.WriteLine($"  Remitente     : {_remitente}");
        Console.WriteLine($"  Destinatario  : {_destinatario}");
        Console.WriteLine($"  Estado        : {_estado}");
        Console.WriteLine($"  Fecha Reg.    : {_fechaRegistro:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"  Costo         : ${CalcularCosto():F2}");
        Console.WriteLine("===== Paquete ==============================");
        _paquete.MostrarInfo();
    }

}