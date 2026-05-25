public sealed class EnvioMaritimo : Envio
{
    // Atributos
    private string _NombreBarco;
    private string _PuertoOrigen;
    private string _PuertoDestino;
    private int _DiasNavegacion;

    // Constructor

    public EnvioMaritimo(string nombreBarco, string puertoOrigen, string puertoDestino, int diasNavegacion)
    {
        NombreBarco = nombreBarco;
        PuertoOrigen = puertoOrigen;
        PuertoDestino = puertoDestino;
        DiasNavegacion = diasNavegacion;
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

    public string Puertoestino
    {
        get { return _PuertoDestino; }
        set { _PuertoDestino = value; }
    }

    public int DiasNavegacion
    {
        get { return _DiasNavegacion; }
        set { _DiasNavegacion = value; }
    }
    public override void ActualizarEstado();

    public override decimal CalcularCostoTotal();

    protected override void GenerearNumeroGuia();

    public override string ObtenerInformacion();

    public override List<T> TipoEnvio();

    public override string CalcularTiempoEntrega();
}