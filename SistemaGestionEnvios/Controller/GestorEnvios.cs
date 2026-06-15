using System.Security.Cryptography.X509Certificates;

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
        UI_Sistema.UI_RegistrarNuevoEnvio();
        string? tipo = Console.ReadLine()?.Trim();

        if (tipo != "1" && tipo != "2" && tipo != "3")
        {
            Console.WriteLine("  Tipo no válido.");
            return;
        }

        UI_Sistema.UI_DatosEnvioBasicos();
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
                UI_Sistema.UI_DatosEnvioTerrestre();
                string? placa = Validador.LeerPlaca();
                string ruta = Validador.LeerTexto("  Ruta            : ", 3, 100);
                int km = Validador.LeerEnteroPositivo("  Distancia (km)  : ");

                envio = _service.RegistrarTerrestre(remitente, destinatario, origen, destino, categoria, paquetes, km, placa, ruta);
            }
            else if (tipo == "2")
            {
                UI_Sistema.UI_DatosEnvioMaritimo();
                string barco = Validador.LeerNombre("  Nombre del barco  : ");
                string puertoOrigen = Validador.LeerLugar("  Puerto de origen  : ");
                string puertoDestino = Validador.LeerLugar("  Puerto de destino : ");
                int dias = Validador.LeerEnteroPositivo("  Días de navegación: ");

                envio = _service.RegistrarMaritimo(remitente, destinatario, origen, destino, categoria, paquetes, barco, puertoOrigen, puertoDestino, dias);
            }
            else
            {
                UI_Sistema.UI_DatosEnvioAereo();
                string vuelo = Validador.LeerTexto("  Número de vuelo      : ", 3, 15);
                string aerOrigen = Validador.LeerLugar("  Aeropuerto de origen :  ");
                string aerDestino = Validador.LeerLugar("  Aeropuerto de destino:  ");
                envio = _service.RegistrarAereo(remitente, destinatario, origen, destino, categoria, paquetes, vuelo, aerOrigen, aerDestino);
            }
            UI_Sistema.UI_DatosGeneralesEnvio(envio);

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

        Console.WriteLine($"  Total: {envios.Count} envío(s)\n");

        int contador = 1;
        foreach (Envio envio in envios)
        {
            string Encabezado = $"{new string('-', 10)} ENVIO #{contador} | NUMERO DE GUIA: {envio.NumeroGuia} {new string('-', 10)}";
            Console.WriteLine($"\n{Encabezado}");
            envio.MostrarInformacion();
                Console.WriteLine(new string('-', Encabezado.Length));
            contador++;
        }
    }
       
    public void BuscarEnvio()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR ENVIO POR NÚMERO DE GUÍA ===\n");
        Console.Write("  Número de guía: ");
        string? guia = Console.ReadLine()?.Trim();

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
        UI_Sistema.UI_FiltrarEnvios();
        string? opcion = Console.ReadLine()?.Trim();

        List<Envio> resultado = null;

        switch (opcion)
        {
            case "1":
                Console.Write("  Tipo (Terrestre / Maritimo / Aereo): ");
                string? tipo = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.TipoEnvio().Equals(tipo, StringComparison.OrdinalIgnoreCase));
                break;

            case "2":
                Console.Write("  Estado: ");
                string? estado = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                break;

            case "3":
                Console.Write("  Categoría: ");
                string? categoria = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.CategoriaEnvio.Equals(categoria, StringComparison.OrdinalIgnoreCase));
                break;

            case "4":
                Console.Write("  Remitente (o parte del nombre): ");
                string? remitente = Console.ReadLine()?.Trim();
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
        UI_Sistema.UI_OrdenarEnvios();
        string? opcion = Console.ReadLine()?.Trim();

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

        Console.Write("  Numero de guia a modificar: ");
        string? guia = Console.ReadLine()?.Trim();

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
        string? remitente = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(remitente)) envio.Remitente = remitente;

        string? destinatario = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destinatario)) envio.Destinatario = destinatario;

        Console.Write($"  Origen [{envio.Origen}]: ");
        string? origen = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(origen)) envio.Origen = origen;


        Console.Write($"  Destino [{envio.Destino}]: ");
        string? destino = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destino)) envio.Destino = destino;

        Console.Write($"  Categoria [{envio.CategoriaEnvio}]: ");
        string? categoria = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(categoria)) envio.CategoriaEnvio = categoria;

        try
        {
            _service.Modificar(guia, remitente, destinatario, origen, destino, categoria);

            Console.Write("\n  Actualizar estado? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {


                envio.ActualizarEstado();  // muestra submenú de estados en consola
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

        Console.Write("  Numero de guia a eliminar: ");
        string? guia = Console.ReadLine()?.Trim();

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
            Console.WriteLine("  Eliminacion cancelada.");
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
            UI_Sistema.UI_DatosPaqueteDelEnvio(i);
            string codigoPaquete = Validador.LeerCodigoPaquete();
            string contenido = Validador.LeerTexto("  Contenido      : ", 3, 100);
            bool esFragil = Validador.LeerSiNo("  Es frágil? (s/n): ");
            decimal valorDeclarado = Validador.LeerDecimalPositivo("  Valor declarado: ");
            double peso = Validador.LeerDoublePositivo("  Peso (kg)      : ");
            double alto = Validador.LeerDoublePositivo("  Alto (cm)      : ");
            double ancho = Validador.LeerDoublePositivo("  Ancho (cm)     : ");
            double largo = Validador.LeerDoublePositivo("  Largo (cm)     : ");

            string tipoPaquete = Validador.CalcularTipoPaquete(peso, alto, ancho, largo);

            Console.WriteLine($"  Tipo calculado: {tipoPaquete}");

            Paquete paquete = new Paquete( codigoPaquete,contenido,esFragil,valorDeclarado,tipoPaquete,peso,largo,alto,ancho);

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
