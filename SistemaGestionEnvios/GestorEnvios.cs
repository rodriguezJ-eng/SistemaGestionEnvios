using System;
using System.Collections.Generic;

public class GestorEnvios
{
    // Atributos
    private List<Envio> _Envios;
    private string _RutaEnvio;

    // Constructor
    public GestorEnvios(string rutaEnvio)
    {
        Envios = new List<Envio>();
        RutaEnvio = rutaEnvio;
    }

    // propiedades
    public List<Envio> Envios
    {
        get { return _Envios; }
        set { _Envios = value; }
    }

    public string RutaEnvio
    {
        get { return _RutaEnvio; }
        set { _RutaEnvio = value; }
    }

    // Metodos
    public void RegistrarEnvio(Envio envio)
    {
        Envios.Add(envio);
    }

    public List<Envio> MostrarEnvios()
    {
        return Envios;
    }

    public Envio BuscarEnvio(string numeroGuia)
    {
        foreach (Envio envio in Envios)
        {
            if (envio.NumeroGuia == numeroGuia)
            {
                return envio;
            }
        }

        return null;
    }

    public void ModificarEnvio(
    string numeroGuia,
    string? origen = null,
    string? destino = null,
    string? estado = null,
    string? remitente = null,
    string? destinatario = null,
    string? categoriaEnvio = null
    )
    {
        Envio envio = BuscarEnvio(numeroGuia);

        if (envio != null)
        {
            if (origen != null)
                envio.Origen = origen;

            if (destino != null)
                envio.Destino = destino;

            if (estado != null)
                envio.Estado = estado;

            if (remitente != null)
                envio.Remitente = remitente;

            if (destinatario != null)
                envio.Destinatario = destinatario;

            if (categoriaEnvio != null)
                envio.CategoriaEnvio = categoriaEnvio;

            Console.WriteLine("Envío modificado correctamente.");
        }
        else
        {
            Console.WriteLine("Envío no encontrado.");
        }

    }

    public void EliminarEnvio(string numeroGuia)
    {
        Envio envio = BuscarEnvio(numeroGuia);

        if (envio != null)
        {
            Envios.Remove(envio);
        }
    }

    public void GuardarArchivo()
    {

    }

    public void CargarArchivo()
    {
    
    }

}