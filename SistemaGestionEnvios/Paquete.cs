public class Paquete
{
    // atributos 
    private string _descripcion;
    private double _peso;
    private double _largo;
    private double _alto;
    private double _ancho;
    private bool _esFragil;

    public Paquete(string descripcion, double peso, double largo, double alto, double ancho, bool esFragil)
    {
        Descripcion = descripcion;
        Peso = peso;
        Largo = largo;
        Alto = alto;
        Ancho = ancho;
        EsFragil = esFragil;
    }

    // propiedades (aún faltan validar)
    public string Descripcion { get => _descripcion; set => _descripcion = value; }
    public double Peso { get => _peso; set => _peso = value; }
    public double Largo { get => _largo; set => _largo = value; }
    public double Alto { get => _alto; set => _alto = value; }
    public double Ancho { get => _ancho; set => _ancho = value; }
    public bool EsFragil { get => _esFragil; set => _esFragil = value; }

    // Métodos

    public double CalcularVolumen()
    {
        return _alto * _ancho * _largo;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Descripción : {_descripcion}");
        Console.WriteLine($"Peso        : {_peso}");
        Console.WriteLine($"Dimesiones  : {_alto} x {_ancho} x {_largo}");
        Console.WriteLine($"Volumen     : {CalcularVolumen()} cm^3");
        Console.WriteLine($"Fragil      : {(_esFragil ? "Sí" : "No")}");
    }
}