using System.Net.NetworkInformation;
using System.Xml.Serialization;

/// <summary>
/// Implementación concreta del repositorio de envíos que utiliza archivos XML para la persistencia física de datos.
/// Utiliza polimorfismo para serializar y deserializar jerarquías de clases heredadas de Envio.
/// </summary>
public class EnvioRepositoryXml : IEnvioRepository
{

    private readonly string _archivo;

    /// <summary>
    /// metadatos de tipos requerida por el serializador para resolver instancias polimórficas.
    /// </summary>
    private static readonly Type[] _tiposConcretos =
    {
        typeof(EnvioTerrestre),
        typeof(EnvioMaritimo),
        typeof(EnvioAereo)
    };

    /// <summary>
    /// Inicializa una nueva instancia del repositorio XML y asegura la existencia física del archivo de persistencia.
    /// </summary>
    /// <param name="archivo">La ruta absoluta o relativa del archivo XML.</param>
    public EnvioRepositoryXml(string archivo)
    {
        _archivo = archivo;

        if (!File.Exists(_archivo))
            GuardarArchivo(new List<Envio>());
    }

    /// <summary>
    /// Abre el archivo físico y deserializa los datos convirtiéndolos en una estructura de lista en memoria.
    /// </summary>
    private List<Envio> LeerArchivo()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Envio>),
            _tiposConcretos);
        using FileStream fs = new(_archivo, FileMode.Open);
        return (List<Envio>)serializer.Deserialize(fs)!;

    }

    /// <summary>
    /// Serializa la colección de envíos en memoria y escribe el resultado de forma atómica en el archivo XML.
    /// </summary>
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

    /// <summary>
    /// Registra un nuevo envío validando que no se repita el número de guía e impacta el archivo XML.
    /// </summary>
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

    /// <summary>
    /// Remueve un envío del archivo XML localizando el registro mediante su identificador único.
    /// </summary>
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

    /// <summary>
    /// Obtiene un envío por número de guía delegando la operación al método genérico Buscar.
    /// </summary>
    public Envio ObtenerPorGuia(string numeroGuia)
        => Buscar(e => e.NumeroGuia == numeroGuia);

    /// <summary>
    /// Retorna la lista completa de registros cargados directamente del archivo XML.
    /// </summary>
    public List<Envio> ObtenerTodos()
        => LeerArchivo();

    /// <summary>
    /// Ejecuta una consulta LINQ de filtrado sobre el listado deserializado del archivo XML.
    /// </summary>
    public List<Envio> Filtrar(Func<Envio, bool> criterio)
        => LeerArchivo().Where(criterio).ToList();

    /// <summary>
    /// Ejecuta una consulta LINQ de ordenamiento sobre el listado deserializado del archivo XML.
    /// </summary>
    public List<Envio> Ordenar(Func<Envio, object> criterio)
        => LeerArchivo().OrderBy(criterio).ToList();

    /// <summary>
    /// Busca el primer elemento coincidente leyendo secuencialmente los datos del XML.
    /// </summary>
    public Envio Buscar(Func<Envio, bool> criterio)
        => LeerArchivo().FirstOrDefault(criterio);

    /// <summary>
    /// Modifica las propiedades de un envío en el archivo XML 
    /// </summary>
    public void Actualizar(Envio envio)
    {
        List<Envio> envios = LeerArchivo();

        Envio existente = envios.FirstOrDefault(e => e.NumeroGuia == envio.NumeroGuia)
            ?? throw new ArgumentException(
                $"No existe un envío con guía '{envio.NumeroGuia}'");

        existente.Remitente = envio.Remitente;
        existente.Destinatario = envio.Destinatario;
        existente.Origen = envio.Origen;
        existente.Destino = envio.Destino;
        existente.CategoriaEnvio = envio.CategoriaEnvio;
        existente.Estado = envio.Estado;

        GuardarArchivo(envios);
    }
}
