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

    public override void ActualizarEstado();

    public override decimal CalcularCostoTotal();

    protected override void GenerarNumeroGuia();

    public override string ObtenerInformacion();

    public override string TipoEnvio();

    public override string CalcularTiempoEntrega();
}