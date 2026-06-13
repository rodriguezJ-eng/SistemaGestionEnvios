public class GestorEnvios
{
    private List<Envio> _Envios;
    private string _RutaEnvio;

    public List<Envio> Envios
    {
        get => _Envios;
        set => _Envios = value ?? throw new ArgumentException("Lista inválida.");
    }

    public string RutaEnvio
    {
        get => _RutaEnvio;
        set => _RutaEnvio = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Ruta obligatoria.")
            : value;
    }

    public GestorEnvios(string rutaEnvio)
    {
        Envios = new List<Envio>();
        RutaEnvio = rutaEnvio; // Cabe aclarar que al referirse a ruta nos referimos a la ruta donde los datos seran guardados
    }

    /// <summary>
    /// Pide los datos por consola, crea los paquetes y registra un nuevo envio en la lista.
    /// </summary>
    public void RegistrarEnvio()
    {
        Console.Clear();
        Console.WriteLine("══ REGISTRAR NUEVO ENVÍO ══\n");

        Console.WriteLine("  Tipo de envío:");
        Console.WriteLine("  1. Terrestre");
        Console.WriteLine("  2. Marítimo");
        Console.WriteLine("  3. Aéreo");
        Console.Write("  Seleccione: ");
        string? tipo = Console.ReadLine()?.Trim();

        if (tipo != "1" && tipo != "2" && tipo != "3")
        {
            Console.WriteLine(" Tipo no válido.");
            return;
        }

        // Datos comunes del envío
        string numeroGuia = Validador.LeerNumeroGuia();

        string remitente = Validador.LeerNombre("  Remitente      : ");

        string destinatario = Validador.LeerNombre("  Destinatario   : ");

        string origen = Validador.LeerLugar("  Origen         : ");

        string destino = Validador.LeerLugar("  Destino        : ");

        string categoria = Validador.LeerCategoria(); 

        // SE PREGUNTA CUANTOS PAQUETES TIENE EL ENVIO
        Console.WriteLine("\n Cuantos paquetes tiene el envio: ");
        int.TryParse(Console.ReadLine()?.Trim(), out int cantidadPaquetes);

        if(cantidadPaquetes <= 0)
        {
            Console.WriteLine("Cantidad no válida.");
            return;
        }
        // creación de una lista vacía que acumula paquetes
        List<Paquete> paquetes = new List<Paquete>();

        for (int i = 1; i <= cantidadPaquetes; i++)
        {
            string codigoPaquete = Validador.LeerCodigoPaquete();

            string contenido = Validador.LeerTexto("  Contenido      : ", 3, 100);

            bool esFragil = Validador.LeerSiNo("  Es fragil? (s/n): ");

            decimal valorDeclarado = Validador.LeerDecimalPositivo("  Valor declarado: ");

            string tipoPaquete = Validador.LeerTipoPaquete();

            double peso = Validador.LeerDoublePositivo("  Peso (kg)      : ");
            
            double alto = Validador.LeerDoublePositivo("  Alto (cm)      : ");

            double ancho = Validador.LeerDoublePositivo("  Ancho (cm)     : ");

            double largo = Validador.LeerDoublePositivo("  Largo (cm)     : ");
            // Se crea el paquete y se agrega a la lista
            Paquete paquete = new Paquete(codigoPaquete, contenido, esFragil,
                                          valorDeclarado, tipoPaquete, peso, largo, alto, ancho);
            paquetes.Add(paquete);

        }

        // Datos específicos según el tipo de envío

        Envio envio = null;

        if (tipo == "1")
        {
            Console.WriteLine("\n  === Datos del Envío Terrestre ===");

            string? placa = Validador.LeerPlaca();
            string ruta = Validador.LeerTexto("  Ruta            : ", 3, 100);
            int km = Validador.LeerEnteroPositivo("  Distancia (km)  : ");

            envio = new EnvioTerrestre(
                numeroGuia, DateTime.Now, origen, destino, "Pendiente",
                paquetes, categoria, remitente, destinatario,
                km, placa, ruta
            );
    }

        else if (tipo == "2")
        {
            Console.WriteLine("\n=== Datos del Envío Marítimo ===");

            string barco = Validador.LeerNombre("  Nombre del barco  : "   );

            string puertoOrigen = Validador.LeerLugar("  Puerto de origen  : ");

            string puertoDestino = Validador.LeerLugar("  Puerto de destino : ");

            int dias = Validador.LeerEnteroPositivo("  Días de navegación: ");

            envio = new EnvioMaritimo(
                numeroGuia, DateTime.Now, origen, destino, "Pendiente",
                paquetes, categoria, remitente, destinatario,
                barco, puertoOrigen, puertoDestino, dias
            );
        }

        else
        {
            Console.WriteLine("\n=== Datos del Envío Aéreo ===");

            string vuelo = Validador.LeerTexto("  Número de vuelo      : ", 3, 15);

            string aerOrigen = Validador.LeerLugar("  Aeropuerto de origen :  ");

            string aerDestino = Validador.LeerLugar("  Aeropuerto de destino:  ");

            envio = new EnvioAereo(
                numeroGuia, DateTime.Now, origen, destino, "Pendiente",
                paquetes, categoria, remitente, destinatario,
                vuelo, aerOrigen, aerDestino
            );
        }

        Envios.Add(envio);
        Console.WriteLine("\n Envío registrado exitosamente.");
    }

    /// <summary>
    /// Muestra en consola la informacion de todos los envios registrados.
    /// </summary>
    public void MostrarEnvios()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE ENVIOS ===\n");

        if ( Envios.Count == 0 )
        {
            Console.WriteLine("No hay envios registrados.");
            return ;
        }

        Console.WriteLine($"Total: {Envios.Count} envio(s)");
        Console.WriteLine(new string('-', 50));

        foreach (Envio envio in Envios) 
        {
            envio.MostrarInformacion();
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// Busca un envio por su numero de guia y lo retorna. Uso interno del gestor.
    /// </summary>
    /// <param name="numeroGuia">Numero de guia del envio a buscar.</param>
    /// <returns>El objeto Envio encontrado, o null si no existe.</returns>
    public Envio BuscarEnvio(string numeroGuia)
    {
        foreach (Envio envio in Envios)
        {
            if (envio.NumeroGuia == numeroGuia)
            {
                return envio;
            }
        }
        return null;
    }

    /// <summary>
    /// Pide el numero de guia por consola y muestra la informacion del envio encontrado.
    /// Sobrecarga del metodo BuscarEnvio para uso directo desde el menu.
    /// </summary>
    public void BuscarEnvio()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR ENVIO ===");

        Console.WriteLine("Numero de guia: ");
        string? guia = Console.ReadLine()?.Trim();

        Envio encontrado = BuscarEnvio(guia);
        
        if (encontrado == null)
        {
            Console.WriteLine("\n No se encontro ningun envio con ese número de guia.");
            return;
        }

        Console.WriteLine("\n == Envio encontrado");
        encontrado.MostrarInformacion();
    }

    /// <summary>
    /// Pide el numero de guia por consola, muestra los datos actuales
    /// y permite al usuario modificar los campos del envio.
    /// </summary>

    public void ModificarEnvio()
    {
        Console.Clear();
        Console.WriteLine("== MODIFICAR ENVIO ==\n");

        Console.Write("  Numero de guia a modificar: ");
        string? guia = Console.ReadLine()?.Trim();

        Envio envio = BuscarEnvio(guia);

        if (envio == null)
        {
            Console.WriteLine("\n  Envio no encontrado.");
            return;
        }

        Console.WriteLine("\n  Datos actuales:");
        envio.MostrarInformacion();

        Console.WriteLine("\n  Nuevos datos (Enter para conservar el actual):\n");

        Console.Write($"  Remitente [{envio.Remitente}]: ");
        string? remitente = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(remitente)) envio.Remitente = remitente;

        Console.Write($"  Destinatario [{envio.Destinatario}]: ");
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

        Console.Write("\n  Actualizar estado? (s/n): ");
        if (Console.ReadLine()?.Trim().ToLower() == "s")
            envio.ActualizarEstado();

        Console.WriteLine("\n  Envio modificado correctamente.");
    }


    /// <summary>
    /// Pide el numero de guia por consola y elimina el envio de la lista previa confirmacion.
    /// </summary>

    public void EliminarEnvio()
    {
        Console.Clear();
        Console.WriteLine("== ELIMINAR ENVIO ==\n");

        Console.Write("  Numero de guia a eliminar: ");
        string? guia = Console.ReadLine()?.Trim();

        Envio envio = BuscarEnvio(guia);

        if (envio == null)
        {
            Console.WriteLine("\n  Envio no encontrado.");
            return;
        }

        Console.WriteLine("\n  Datos del envio a eliminar:");
        envio.MostrarInformacion();

        Console.Write("\n  Confirma la eliminacion? (s/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "s")
        {
            Console.WriteLine("  Eliminacion cancelada.");
            return;
        }

        Envios.Remove(envio);
        Console.WriteLine("\n  Envio eliminado exitosamente.");
    }

    /// <summary>
    /// Guarda la lista de envios en el archivo indicado por RutaEnvio.
    /// </summary>
    public void GuardarArchivo()
    {

    }

    /// <summary>
    /// Carga la lista de envios desde el archivo indicado por RutaEnvio.
    /// </summary>
    public void CargarArchivo()
    {
    
    }

}