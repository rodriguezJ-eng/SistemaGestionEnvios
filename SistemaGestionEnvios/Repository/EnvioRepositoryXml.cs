using System.Net.NetworkInformation;
using System.Xml.Serialization;

/// <summary>
/// Implementación en memoria del repositorio de envíos.
/// Esta es la única clase que sabe donde y como se almacenan los datos.
/// Si mañana se cambia a XML o base de datos, solo se reemplaza esta clase
/// por una nueva implementación de IEnvioRepository - el resto del sistema no cambia.
/// </summary>
public class EnvioRepositoryXml : IEnvioRepository
{

    private readonly string _archivo;

    // Tipos concretos que heredan de Envio: necesario para que XmlSerializer trabaje con el polimirfismo
    private static readonly Type[] _tiposConcretos =
    {
        typeof(EnvioTerrestre),
        typeof(EnvioMaritimo),
        typeof(EnvioAereo)
    };

    public EnvioRepositoryXml(string archivo)
    {
        _archivo = archivo;

        if (!File.Exists(_archivo))
            GuardarArchivo(new List<Envio>());
    }

    private List<Envio> LeerArchivo()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Envio>),
            _tiposConcretos);
        using FileStream fs = new(_archivo, FileMode.Open);
        return (List<Envio>)serializer.Deserialize(fs)!;

    }

    private void GuardarArchivo(List<Envio> envios)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Envio>),
                _tiposConcretos);
            using FileStream fs = new(_archivo, FileMode.Create);
            serializer.Serialize(fs, envios);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new IOException("No tiene permisos para escribir el archivo de envíos.", ex);
        }
    }

    public void Agregar(Envio envio)
    {
        if (envio == null)
            throw new ArgumentNullException(nameof(envio), "El envío no puede ser nulo.");

        List<Envio> envios = LeerArchivo();

        if (envios.Any(e => e.NumeroGuia == envio.NumeroGuia))
            throw new InvalidOperationException(
                $"Ya existe un envío con la guía {envio.NumeroGuia}");

        envios.Add(envio);
        GuardarArchivo(envios);
    }

    public void Eliminar(Envio envio)
    {
        if (envio == null)
            throw new ArgumentNullException(nameof(envio), "El envío no puede ser nulo.");

        List<Envio> envios = LeerArchivo();
        Envio existente = envios.FirstOrDefault(e => e.NumeroGuia == envio.NumeroGuia)
            ?? throw new ArgumentException($"No existe un envío con guía {envio.NumeroGuia}");

        envios.Remove(existente);
        GuardarArchivo(envios);
    }

    public Envio ObtenerPorGuia(string numeroGuia)
        => Buscar(e => e.NumeroGuia == numeroGuia);

    public List<Envio> ObtenerTodos()
        => LeerArchivo();

    public List<Envio> Filtrar(Func<Envio, bool> criterio)
        => LeerArchivo().Where(criterio).ToList();

    public List<Envio> Ordenar(Func<Envio, object> criterio)
        => LeerArchivo().OrderBy(criterio).ToList();

    public Envio Buscar(Func<Envio, bool> criterio)
        => LeerArchivo().FirstOrDefault(criterio);
}
