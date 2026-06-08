/// <summary>
/// Capa de presentación (UI de consola).
/// 
/// Responsabilidades:
///   - Leer datos del usuario por consola usando Validador
///   - Llamar al EnvioService con los datos ya capturados
///   - Mostrar resultados en pantalla
/// 
/// NO tiene lógica de negocio.
/// NO accede a la lista de envíos directamente.
/// Solo conoce al EnvioService.
/// </summary>
public class GestorEnvios
{
    private readonly EnvioService _service;

    public GestorEnvios(EnvioService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public void RegistrarEnvio()
    {
        Console.Clear();
        Console.WriteLine("══ REGISTRAR NUEVO ENVÍO ══\n");

        Console.WriteLine("  Tipo de envío:");
        Console.WriteLine("  1. Terrestre");
        Console.WriteLine("  2. Marítimo");
        Console.WriteLine("  3. Aéreo");
        Console.Write("  Seleccione: ");
        string tipo = Console.ReadLine()?.Trim();

        if (tipo != "1" && tipo != "2" && tipo != "3")
        {
            Console.WriteLine("  Tipo no válido.");
            return;
        }

        string remitente = Validador.LeerNombre("  Remitente      : ");
        string destinatario = Validador.LeerNombre("  Destinatario   : ");
        string origen = Validador.LeerLugar("  Origen         : ");
        string destino = Validador.LeerLugar("  Destino        : ");
        string categoria = Validador.LeerCategoria();

        Console.Write("\n  Cuántos paquetes tiene el envío: ");
        int.TryParse(Console.ReadLine()?.Trim(), out int cantidadPaquetes);

        if (cantidadPaquetes <= 0)
        {
            Console.WriteLine("  Cantidad no válida.");
            return;
        }

        List<Paquete> paquetes = LeerPaquetes(cantidadPaquetes);

        Envio envio = null;

        try
        {
            if (tipo == "1")
            {
                Console.WriteLine("\n  === Datos del Envío Terrestre ===");
                Console.Write("  Placa del camión: ");
                string placa = Console.ReadLine()?.Trim();
                Console.Write("  Ruta            : ");
                string ruta = Console.ReadLine()?.Trim();
                Console.Write("  Distancia (km)  : ");
                int.TryParse(Console.ReadLine()?.Trim(), out int km);

                envio = _service.RegistrarTerrestre(remitente, destinatario, origen, destino, categoria, paquetes, km, placa, ruta);
            }
            else if (tipo == "2")
            {
                Console.WriteLine("\n  === Datos del Envío Marítimo ===");
                Console.Write("  Nombre del barco  : ");
                string barco = Console.ReadLine()?.Trim();
                Console.Write("  Puerto de origen  : ");
                string puertoOrigen = Console.ReadLine()?.Trim();
                Console.Write("  Puerto de destino : ");
                string puertoDestino = Console.ReadLine()?.Trim();
                Console.Write("  Días de navegación: ");
                int.TryParse(Console.ReadLine()?.Trim(), out int dias);

                envio = _service.RegistrarMaritimo(remitente, destinatario, origen, destino, categoria, paquetes, barco, puertoOrigen, puertoDestino, dias);
            }
            else
            {
                Console.WriteLine("\n  === Datos del Envío Aéreo ===");
                Console.Write("  Número de vuelo      : ");
                string vuelo = Console.ReadLine()?.Trim();
                Console.Write("  Aeropuerto de origen : ");
                string aerOrigen = Console.ReadLine()?.Trim();
                Console.Write("  Aeropuerto de destino: ");
                string aerDestino = Console.ReadLine()?.Trim();

                envio = _service.RegistrarAereo(remitente, destinatario, origen, destino, categoria, paquetes, vuelo, aerOrigen, aerDestino);
            }

            Console.WriteLine($"\n  Envío registrado. Número de guía asignado: {envio.NumeroGuia}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  Error al registrar el envío: {ex.Message}");
        }
    }

    public void MostrarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE ENVIOS ===\n");

        List<Envio> envios = _service.ObtenerTodos();

        if (envios.Count == 0)
        {
            Console.WriteLine("  No hay envíos registrados.");
            return;
        }

        Console.WriteLine($"  Total: {envios.Count} envío(s)");
        Console.WriteLine(new string('-', 50));

        foreach (Envio envio in envios)
        {
            envio.MostrarInformacion();
            Console.WriteLine(new string('-', 50));
        }
    }

    public void BuscarEnvio()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR ENVIO POR NÚMERO DE GUÍA ===\n");
        Console.Write("  Número de guía: ");
        string guia = Console.ReadLine()?.Trim();

        Envio encontrado = _service.BuscarPorGuia(guia);

        if (encontrado == null)
        {
            Console.WriteLine("\n  No se encontró ningún envío con ese número de guía.");
            return;
        }

        Console.WriteLine("\n  Envío encontrado:");
        encontrado.MostrarInformacion();
    }

    public void FiltrarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== FILTRAR ENVÍOS ===\n");
        Console.WriteLine("  Filtrar por:");
        Console.WriteLine("  1. Tipo de envío (Terrestre / Marítimo / Aéreo)");
        Console.WriteLine("  2. Estado (Pendiente / En transito / Entregado / Cancelado)");
        Console.WriteLine("  3. Categoría de envío");
        Console.WriteLine("  4. Remitente");
        Console.Write("\n  Seleccione: ");
        string opcion = Console.ReadLine()?.Trim();

        List<Envio> resultado = null;

        switch (opcion)
        {
            case "1":
                Console.Write("  Tipo (Terrestre / Maritimo / Aereo): ");
                string tipo = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.TipoEnvio().Equals(tipo, StringComparison.OrdinalIgnoreCase));
                break;

            case "2":
                Console.Write("  Estado: ");
                string estado = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                break;

            case "3":
                Console.Write("  Categoría: ");
                string categoria = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.CategoriaEnvio.Equals(categoria, StringComparison.OrdinalIgnoreCase));
                break;

            case "4":
                Console.Write("  Remitente (o parte del nombre): ");
                string remitente = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.Remitente.Contains(remitente, StringComparison.OrdinalIgnoreCase));
                break;

            default:
                Console.WriteLine("  Opción no válida.");
                return;
        }

        MostrarResultados(resultado);
    }

    public void OrdenarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== ORDENAR ENVÍOS ===\n");
        Console.WriteLine("  Ordenar por:");
        Console.WriteLine("  1. Fecha de registro (más reciente primero)");
        Console.WriteLine("  2. Número de guía");
        Console.WriteLine("  3. Estado");
        Console.WriteLine("  4. Costo total (mayor a menor)");
        Console.Write("\n  Seleccione: ");
        string opcion = Console.ReadLine()?.Trim();

        List<Envio> resultado = null;

        switch (opcion)
        {
            case "1":
                resultado = _service.Ordenar(e => e.FechaEnvio).AsEnumerable().Reverse().ToList();
                break;
            case "2":
                resultado = _service.Ordenar(e => e.NumeroGuia);
                break;
            case "3":
                resultado = _service.Ordenar(e => e.Estado);
                break;
            case "4":
                resultado = _service.Ordenar(e => e.CalcularCostoTotal()).AsEnumerable().Reverse().ToList();
                break;
            default:
                Console.WriteLine("  Opción no válida.");
                return;
        }

        MostrarResultados(resultado);
    }

    public void ModificarEnvio()
    {
        Console.Clear();
        Console.WriteLine("== MODIFICAR ENVIO ==\n");
        Console.Write("  Número de guía a modificar: ");
        string guia = Console.ReadLine()?.Trim();

        Envio envio = _service.BuscarPorGuia(guia);

        if (envio == null)
        {
            Console.WriteLine("\n  Envío no encontrado.");
            return;
        }

        Console.WriteLine("\n  Datos actuales:");
        envio.MostrarInformacion();
        Console.WriteLine("\n  Nuevos datos (Enter para conservar el actual):\n");

        Console.Write($"  Remitente [{envio.Remitente}]: ");
        string remitente = Console.ReadLine()?.Trim();

        Console.Write($"  Destinatario [{envio.Destinatario}]: ");
        string destinatario = Console.ReadLine()?.Trim();

        Console.Write($"  Origen [{envio.Origen}]: ");
        string origen = Console.ReadLine()?.Trim();

        Console.Write($"  Destino [{envio.Destino}]: ");
        string destino = Console.ReadLine()?.Trim();

        Console.Write($"  Categoría [{envio.CategoriaEnvio}]: ");
        string categoria = Console.ReadLine()?.Trim();

        try
        {
            _service.Modificar(guia, remitente, destinatario, origen, destino, categoria);

            Console.Write("\n  Actualizar estado? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                envio.ActualizarEstado();  // muestra submenú de estados en consola
                // una vez que ActualizarEstado() asigna el nuevo estado al objeto,
                // el Service no necesita hacer nada más (ya es la misma referencia)
            }

            Console.WriteLine("\n  Envío modificado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  Error: {ex.Message}");
        }
    }

    public void EliminarEnvio()
    {
        Console.Clear();
        Console.WriteLine("== ELIMINAR ENVIO ==\n");
        Console.Write("  Número de guía a eliminar: ");
        string guia = Console.ReadLine()?.Trim();

        Envio envio = _service.BuscarPorGuia(guia);

        if (envio == null)
        {
            Console.WriteLine("\n  Envío no encontrado.");
            return;
        }

        Console.WriteLine("\n  Datos del envío a eliminar:");
        envio.MostrarInformacion();

        Console.Write("\n  Confirma la eliminación? (s/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "s")
        {
            Console.WriteLine("  Eliminación cancelada.");
            return;
        }

        try
        {
            _service.Eliminar(guia);
            Console.WriteLine("\n  Envío eliminado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  Error: {ex.Message}");
        }
    }

    /// <summary>
    /// solo GestorEnvios sabe cómo pedirle paquetes al usuario.
    /// </summary>
    private List<Paquete> LeerPaquetes(int cantidad)
    {
        List<Paquete> paquetes = new List<Paquete>();

        for (int i = 1; i <= cantidad; i++)
        {
            Console.WriteLine($"\n  -- Paquete {i} --");
            string codigoPaquete = Validador.LeerCodigoPaquete();
            string contenido = Validador.LeerTexto("  Contenido      : ", 3, 100);
            bool esFragil = Validador.LeerSiNo("  Es frágil? (s/n): ");
            decimal valorDeclarado = Validador.LeerDecimalPositivo("  Valor declarado: ");
            string tipoPaquete = Validador.LeerTipoPaquete();
            double peso = Validador.LeerDoublePositivo("  Peso (kg)      : ");
            double alto = Validador.LeerDoublePositivo("  Alto (cm)      : ");
            double ancho = Validador.LeerDoublePositivo("  Ancho (cm)     : ");
            double largo = Validador.LeerDoublePositivo("  Largo (cm)     : ");

            Paquete paquete = new Paquete(codigoPaquete, contenido, esFragil,
                                          valorDeclarado, tipoPaquete, peso, largo, alto, ancho);
            paquetes.Add(paquete);
        }

        return paquetes;
    }

    private void MostrarResultados(List<Envio> lista)
    {
        Console.WriteLine();

        if (lista == null || lista.Count == 0)
        {
            Console.WriteLine("  No se encontraron envíos con ese criterio.");
            return;
        }

        Console.WriteLine($"  {lista.Count} envío(s) encontrado(s):");
        Console.WriteLine(new string('-', 50));

        foreach (Envio envio in lista)
        {
            envio.MostrarInformacion();
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// Expone el conteo de envíos al Program.cs para el encabezado del menú.
    /// </summary>
    public int ContarEnvios() => _service.ContarEnvios();
}
