
public class GestorEnvios
{
    // Atributos
    private List<Envio> _Envios;
    private string _RutaEnvio;

    // Constructor
    public GestorEnvios(List<Envio> envios, string rutaEnvio)
    {
        Envios = envios;
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
    public void RegistrarEnvio()
    {

    }

    public List<T> MostrarEnvios()
    {
        return MostrarEnvios;
    }

    public Envio BuscarEnvio(int numeroGuia)
    {
        return BuscarEnvio(1);
    }

    public void ModificarEnvio(int numeroGuia, DateTime fechaEnvio)
    {

    }

    public void ModificarEnvio(int numeroGuia, string origen)
    {

    }

    public void ModificarEnvio(int numeroGuia, string destino)
    {

    }

    public void ModificarEnvio(int numeroGuia)
    {

    }

    public void ModificarEnvio(int numeroGuia, List<Paquete> paquetes)
    {

    }

    public void ModificarEnvio(int numeroGuia, string tipoEnvio)
    {

    }

    public void EliminarEnvio()
    {

    }

    public void GuardarArchivo()
    {

    }

    public void CargarArchivo()
    {

    }

}