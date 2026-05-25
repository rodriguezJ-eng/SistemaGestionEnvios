public sealed class EnvioTerrestre : Envio
{
    // Atributos
    private int _DistanciaKm;
    private string _PlacaCamion;
    private string _Ruta;

    // Constructor

    public EnvioTerrestre(int distanciaKm, string placaCamion, string ruta)
    {
        DistanciaKm = distanciaKm;
        PlacaCamion = placaCamion;
        Ruta = ruta;
    }

    // Propiedades
    public string DistanciaKm
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

    public override void ActualizarEstado();

    public override decimal CalcularCostoTotal();

    protected override void GenerearNumeroGuia();

    public override string ObtenerInformacion();

    public override List<T> TipoEnvio();

    public override string CalcularTiempoEntrega();
}