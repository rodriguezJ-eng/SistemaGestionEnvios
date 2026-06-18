/// <summary>
/// Define las operaciones de acceso a datos para Envio.
/// Cualquier implementación futura (XML, base de datos, etc) debe respetar esta interfaz.
/// El Service solo conoce esta interfaz, nunca la implementación concreta.
/// </summary>
public interface IEnvioRepository
{
    /// <summary>Inserta un nuevo envío en la persistencia de datos.</summary>
    /// <param name="envio">El objeto Envio a registrar.</param>
    void Agregar(Envio envio);

    /// <summary>Actualiza las propiedades de un envío existente en el almacenamiento.</summary>
    /// <param name="envio">El objeto Envio con los datos modificados.</param>
    void Actualizar(Envio envio);

    /// <summary>Remueve un registro de envío del sistema de almacenamiento.</summary>
    /// <param name="envio">El objeto Envio que se desea eliminar.</param>
    void Eliminar(Envio envio);

    /// <summary>Recupera un envío específico buscando por su identificador único (Número de Guía).</summary>
    /// <param name="numeroGuia">El código alfanumérico de la guía de envío.</param>
    /// <returns>La instancia del Envio encontrado, o null si no se halla coincidencia.</returns>
    Envio ObtenerPorGuia(string numeroGuia);

    /// <summary>Extrae la totalidad de los envíos registrados en el sistema de almacenamiento.</summary>
    /// <returns>Una lista de objetos que heredan de la clase base Envio.</returns>
    List<Envio> ObtenerTodos();

    /// <summary>Filtra la colección de envíos basándose en una expresión condicional lógica.</summary>
    /// <param name="criterio">Un delegado que evalúa cada envío devolviendo un valor booleano.</param>
    /// <returns>Una lista de envíos que cumplen con la condición evaluada.</returns>
    List<Envio> Filtrar(Func<Envio, bool> criterio);

    /// <summary>Ordena la colección de envíos basándose en una propiedad o criterio específico.</summary>
    /// <param name="criterio">Un delegado que extrae la propiedad de ordenamiento del objeto Envio.</param>
    /// <returns>Una lista de envíos ordenada de manera secuencial.</returns>
    List<Envio> Ordenar(Func<Envio, object> criterio);

    /// <summary>Busca el primer envío que cumpla de manera exacta con una condición lógica.</summary>
    /// <param name="criterio">Un delegado de evaluación condicional.</param>
    /// <returns>El primer objeto Envio coincidente, o null en caso contrario.</returns>
    Envio Buscar(Func<Envio, bool> criterio);
}
