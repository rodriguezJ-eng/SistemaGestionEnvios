using System;
using System.Collections.Generic;

public sealed class EnvioAereo : Envio
{
    // Atributos
    private string _NumeroVuelo;
    private string _AeropuertoOrigen;
    private string _AeropuertoDestino;

    // Constructor

    public EnvioAereo(
    string numeroGuia,
    DateTime fechaEnvio,
    string origen,
    string destino,
    string estado,
    List<Paquete> paquetes,
    string categoriaEnvio,
    string remitente,
    string destinatario, 
    string numeroVuelo,
    string aeropuertoOrigen,  
    string aeropuertoDestino) : base(numeroGuia, fechaEnvio, origen, destino, estado, paquetes, categoriaEnvio, remitente, destinatario)
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

    protected override void GenerarNumeroGuia();

    public override string ObtenerInformacion();

    public override string TipoEnvio();

    public override string CalcularTiempoEntrega();
}
