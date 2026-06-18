/// <summary>
/// Capa de lógica de negocio (Service) encargada de coordinar los flujos operacionales del sistema,
/// la validación de reglas transaccionales y la comunicación segura con la capa de persistencia abstracta.
/// </summary>
public class EnvioService
{
    private readonly IEnvioRepository _repository;

    /// <summary>Inicializa el servicio inyectando la abstracción del repositorio bajo el principio de inversión de dependencias.</summary>
    /// <param name="repository">La implementación concreta de acceso a datos.</param>
    public EnvioService(IEnvioRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Instancia un envío de tipo Terrestre bajo el estado inicial 'Pendiente' 
    /// y lo persiste en el sistema.
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
    /// Instancia un envío de tipo Marítimo bajo el estado inicial 'Pendiente' 
    /// y lo persiste en el sistema.
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
    /// Instancia un envío de tipo Aéreo bajo el estado inicial 'Pendiente' 
    /// y lo persiste en el sistema.
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
    /// Solicita al repositorio el listado global de todos los envíos registrados.
    /// </summary>
    public List<Envio> ObtenerTodos()
        => _repository.ObtenerTodos();

    /// <summary>
    /// Solicita al repositorio la localización de un registro mediante su clave primaria.
    /// </summary>
    public Envio BuscarPorGuia(string numeroGuia)
        => _repository.ObtenerPorGuia(numeroGuia);


    /// <summary>
    /// Transfiere el criterio de filtrado dinámico hacia la infraestructura del repositorio.
    /// </summary>
    public List<Envio> Filtrar(Func<Envio, bool> criterio)
        => _repository.Filtrar(criterio);

    /// <summary>
    /// Transfiere el criterio de ordenamiento dinámico hacia la infraestructura del repositorio.
    /// </summary>
    public List<Envio> Ordenar(Func<Envio, object> criterio)
        => _repository.Ordenar(criterio);

    /// <summary>
    /// Modifica de forma segura los atributos descriptivos de un envío.
    /// Realiza una operación atómica simulando un rollback de base de datos en memoria si la persistencia falla.
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
    /// Ordena la remoción definitiva de un registro delegando el identificador lógico al repositorio.
    /// </summary>
    public void Eliminar(string numeroGuia)
    {
        Envio envio = _repository.ObtenerPorGuia(numeroGuia)
            ?? throw new InvalidOperationException($"No existe un envío con guía {numeroGuia}.");

        _repository.Eliminar(envio);
    }

    /// <summary>
    /// Calcula la magnitud numérica de la colección actual de envíos registrados.
    /// </summary>
    public int ContarEnvios()
        => _repository.ObtenerTodos().Count;
}
