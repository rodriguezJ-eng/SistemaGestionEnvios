/// <summary>
/// Capa de lógica de negocio para la gestión de envíos.
/// Recibir datos ya validados y construir los objetos Envio correctos
/// Delegar el almacenamiento al IEnvioRepository
/// 
/// NO sabe nada de consola, archivos ni bases de datos.
/// Solo conoce la interfaz IEnvioRepository, no la implementación concreta.
/// </summary>
public class EnvioService
{
    private readonly IEnvioRepository _repository;

    // El repository se inyecta el Service no crea ni conoce la implementación concreta.
    // Esto permite cambiar EnvioRepository por EnvioRepositoryXml sin tocar el Service.
    public EnvioService(IEnvioRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Registra un nuevo envío terrestre.
    ///  El número de guía lo genera el constructor.
    /// </summary>
    public EnvioTerrestre RegistrarTerrestre(
        string remitente, string destinatario,
        string origen, string destino, string categoria,
        List<Paquete> paquetes,
        int distanciaKm, string placaCamion, string direccion)
    {
        var envio = new EnvioTerrestre(
            DateTime.Now, origen, destino, "Pendiente",
            paquetes, categoria, remitente, destinatario,
            distanciaKm, placaCamion, direccion
        );
        _repository.Agregar(envio);
        return envio;
    }

    /// <summary>
    /// Registra un nuevo envío marítimo.
    /// </summary>
    public EnvioMaritimo RegistrarMaritimo(
        string remitente, string destinatario,
        string origen, string destino, string categoria,
        List<Paquete> paquetes,
        string nombreBarco, string puertoOrigen, string puertoDestino, int diasNavegacion)
    {
        var envio = new EnvioMaritimo(
            DateTime.Now, origen, destino, "Pendiente",
            paquetes, categoria, remitente, destinatario,
            nombreBarco, puertoOrigen, puertoDestino, diasNavegacion
        );
        _repository.Agregar(envio);
        return envio;
    }

    /// <summary>
    /// Registra un nuevo envío aéreo.
    /// </summary>
    public EnvioAereo RegistrarAereo(
        string remitente, string destinatario,
        string origen, string destino, string categoria,
        List<Paquete> paquetes,
        string numeroVuelo, string aeropuertoOrigen, string aeropuertoDestino)
    {
        var envio = new EnvioAereo(
            DateTime.Now, origen, destino, "Pendiente",
            paquetes, categoria, remitente, destinatario,
            numeroVuelo, aeropuertoOrigen, aeropuertoDestino
        );
        _repository.Agregar(envio);
        return envio;
    }

    /// <summary>
    /// Retorna todos los envíos registrados.
    /// </summary>
    public List<Envio> ObtenerTodos()
        => _repository.ObtenerTodos();

    /// <summary>
    /// Busca un envío por su número de guía. Retorna null si no existe.
    /// </summary>
    public Envio BuscarPorGuia(string numeroGuia)
        => _repository.ObtenerPorGuia(numeroGuia);

    /// <summary>
    /// Filtra envíos usando un criterio Func. La lógica de qué filtrar la decide el GestorEnvios,
    /// pero la ejecución del filtro siempre pasa por el repository.
    /// </summary>
    public List<Envio> Filtrar(Func<Envio, bool> criterio)
        => _repository.Filtrar(criterio);

    /// <summary>
    /// Ordena envíos usando un criterio Func.
    /// </summary>
    public List<Envio> Ordenar(Func<Envio, object> criterio)
        => _repository.Ordenar(criterio);

    /// <summary>
    /// Modifica los campos básicos de un envío existente.
    /// Lanza excepción si el envío no existe.
    /// 
    /// Garantiza la atomicidad o se aplican todos los cambio, o ninguno Antes de tocar el objeto real, se guarda su estado Original. 
    /// Si alguna asignación falla, ser revierte todo lo que ya se había alcanzado modificar, luego se relanza la excepción para que 
    /// el GestorEnvios la capture y la aisle
    /// 
    /// Evita que un objeto quede en un estado parcialmente modificado y termine persistiendo en el Xml
    /// </summary>
    public void Modificar(string numeroGuia,
        string remitente, string destinatario,
        string origen, string destino, string categoria)
    {
        Envio envio = _repository.ObtenerPorGuia(numeroGuia)
            ?? throw new InvalidOperationException($"No existe un envío con guía {numeroGuia}.");

        //Un guardado previo del estado original antes de modificar
        string remitenteOriginal = envio.Remitente;
        string destinatarioOriginal = envio.Destinatario;
        string origenOriginal = envio.Origen;
        string destinoOriginal = envio.Destino;
        string categoriaOriginal = envio.CategoriaEnvio;

        try
        {
            if (!string.IsNullOrEmpty(remitente)) envio.Remitente = remitente;
            if (!string.IsNullOrEmpty(destinatario)) envio.Destinatario = destinatario;
            if (!string.IsNullOrEmpty(origen)) envio.Origen = origen;
            if (!string.IsNullOrEmpty(destino)) envio.Destino = destino;
            if (!string.IsNullOrEmpty(categoria)) envio.CategoriaEnvio = categoria;

            _repository.Actualizar(envio);
        }
        catch (Exception ex)
        {
            // Se busca realizar un Rollback

            envio.Remitente = remitenteOriginal;
            envio.Destinatario = destinatarioOriginal;
            envio.Origen = origenOriginal;
            envio.Destino = destinoOriginal;
            envio.CategoriaEnvio = categoriaOriginal;

            throw;
        }


    }

    /// <summary>
    /// Actualiza el estado de un envío. Aplica la regla de negocio
    /// un envío "Entregado" o "Cancelado" no puede cambiar de estado.
    /// </summary>
    public void ActualizarEstado(string numeroGuia, string nuevoEstado)
    {
        Envio envio = _repository.ObtenerPorGuia(numeroGuia)
            ?? throw new InvalidOperationException($"No existe un envío con guía {numeroGuia}.");

        if (envio.Estado == "Entregado" || envio.Estado == "Cancelado")
            throw new InvalidOperationException(
                $"El envío {numeroGuia} ya está en estado '{envio.Estado}' y no puede modificarse.");

        string estadoOriginal = envio.Estado;

        try
        {
            envio.Estado = nuevoEstado;
            _repository.Actualizar(envio);
        }
        catch (ArgumentException)
        {
            envio.Estado = estadoOriginal;
            throw;
        }
    }


/// <summary>
/// Elimina un envío por número de guía.
/// Lanza excepción si el envío no existe.
/// </summary>
public void Eliminar(string numeroGuia)
    {
        Envio envio = _repository.ObtenerPorGuia(numeroGuia)
            ?? throw new InvalidOperationException($"No existe un envío con guía {numeroGuia}.");

        _repository.Eliminar(envio);
    }

    /// <summary>
    /// Retorna el total de envíos registrados.
    /// </summary>
    public int ContarEnvios()
        => _repository.ObtenerTodos().Count;
}
