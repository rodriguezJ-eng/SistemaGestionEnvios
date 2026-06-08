/// <summary>
/// Define las operaciones de acceso a datos para Envio.
/// Cualquier implementación futura (XML, base de datos, etc) debe respetar esta interfaz.
/// El Service solo conoce esta interfaz, nunca la implementación concreta.
/// </summary>
public interface IEnvioRepository
{
    void Agregar(Envio envio);
    void Eliminar(Envio envio);
    Envio ObtenerPorGuia(string numeroGuia);
    List<Envio> ObtenerTodos();
    List<Envio> Filtrar(Func<Envio, bool> criterio);
    List<Envio> Ordenar(Func<Envio, object> criterio);
    Envio Buscar(Func<Envio, bool> criterio);
}
