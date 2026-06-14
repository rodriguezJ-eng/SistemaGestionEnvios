/// <summary>
/// Implementación en memoria del repositorio de envíos.
/// Esta es la única clase que sabe donde y como se almacenan los datos.
/// Si mañana se cambia a XML o base de datos, solo se reemplaza esta clase
/// por una nueva implementación de IEnvioRepository - el resto del sistema no cambia.
/// </summary>
public class EnvioRepository : IEnvioRepository
{
    // La lista vive aquí y solo aquí. Nadie más la toca directamente.
    private readonly List<Envio> _envios = new List<Envio>();

    public void Agregar(Envio envio)
    {
        if (envio == null)
            throw new ArgumentNullException(nameof(envio), "El envío no puede ser nulo.");

        _envios.Add(envio);
    }

    public void Eliminar(Envio envio)
    {
        if (envio == null)
            throw new ArgumentNullException(nameof(envio), "El envío no puede ser nulo.");

        _envios.Remove(envio);
    }

    public Envio ObtenerPorGuia(string numeroGuia)
        => Buscar(e => e.NumeroGuia == numeroGuia);

    public List<Envio> ObtenerTodos()
        => new List<Envio>(_envios); // devuelve copia para que nadie modifique la lista interna

    public List<Envio> Filtrar(Func<Envio, bool> criterio)
        => _envios.Where(criterio).ToList();

    public List<Envio> Ordenar(Func<Envio, object> criterio)
        => _envios.OrderBy(criterio).ToList();

    public Envio Buscar(Func<Envio, bool> criterio)
        => _envios.FirstOrDefault(criterio);
}
