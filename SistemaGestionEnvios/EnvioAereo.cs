public sealed class EnvioAereo : Envio
{
    // Atributos
    private string _NumeroVuelo;
    private string _AeropuertoOrigen;
    private string _AeropuertoDestino;

    // Constructor

    public EnvioAereo(string numeroVuelo, string aeropuertoOrigen,  string aeropuertoDestino)
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

    public override void ActualizarEstado();

    public override decimal CalcularCostoTotal();

    protected override void GenerearNumeroGuia();

    public override string ObtenerInformacion();

    public override List<T> TipoEnvio();

    public override string CalcularTiempoEntrega();
}
