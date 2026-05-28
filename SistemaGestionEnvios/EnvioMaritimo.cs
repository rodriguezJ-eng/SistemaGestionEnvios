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
    string numeroGuia,
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
    int diasNavegacion) : base(numeroGuia, fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
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
    public override void ActualizarEstado();

    public override decimal CalcularCostoTotal();

    protected override void GenerarNumeroGuia();

    public override string ObtenerInformacion();

    public override string TipoEnvio();

    public override string CalcularTiempoEntrega();
}