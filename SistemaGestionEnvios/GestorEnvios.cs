/// <summary>
/// Clase encargada de gestioanr la coleccion de envios
/// Contiene la logica de negocio y la interaccion con el usuario desde consola
/// </summary>

public class GestorEnvios
{
    // Lista donde ser almacenan todos los envios registrados del sistema
    private List<Envio> _Envios;

    // ruta del archivo donde se guardan y cargan los datos de los envios
    private string _RutaEnvio;

    public List<Envio> Envios
    {
        get { return _Envios; }
        set { _Envios = value; }
    }

    public string RutaEnvio
    {
        get { return _RutaEnvio; }
        set { _RutaEnvio = value; }
    }

    public GestorEnvios(string rutaEnvio)
    {
        Envios = new List<Envio>();
        RutaEnvio = rutaEnvio; // Cabe aclarar que al referirse a ruta nos referimos a la ruta donde los datos seran guardados
    }

    // METODOS CON DELEGADOS Func (nuevo commit)

    /// <summary>
    /// Filtra la lista de envios según un criterio personalizado usando un delegado de Func
    /// permite filtrar por cualquier condición: tipo, estado, categoria, etc.
    /// </summary>
    /// <param name="criterio">Función que recibe un Envio y retorna true si cumple la condición.</param>
    /// <returns>Lista de envios que cumple el criterio.</returns>
    public List<Envio> Filtrar(Func<Envio, bool> criterio)
    { return _Envios.Where(criterio).ToList(); }

    /// <summary>
    /// Ordena la lista de envios según un campo especificado usando un delegado Func
    /// permite ordenar por cualquier propiedad: Fecha, numero de guia, estado, etc.
    /// </summary>
    /// <param name="criterio">Función que indica el campo por el cual ordenar.</param>
    /// <returns>Lista de envios ordenada.</returns>
    public List<Envio> Ordenar(Func<Envio, object> criterio)
    {
        return _Envios.OrderBy(criterio).ToList();
    }

    /// <summary>
    /// Busca el primer envio que cumpla el criterio indicado usando un delegado Func.
    /// Uso interno para buscar por cualquier condición.
    /// </summary>
    /// <param name="criterio">Función que recibe un Envio y retorna true si es el buscado</param>
    /// <returns>El primer Envio que cumple el criterio, o null si no existe.</returns>
    public Envio Buscar(Func<Envio, bool> criterio)
    { return _Envios.FirstOrDefault(criterio); }

    /// <summary>
    /// Pide los datos por consola, 
    /// crea los paquetes y registra 
    /// un nuevo envio en la lista.
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
        string tipo = Console.ReadLine()?.Trim();

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

            Console.Write("  Placa del camión: ");
            string placa = Console.ReadLine()?.Trim();

            Console.Write("  Ruta            : ");
            string ruta = Console.ReadLine()?.Trim();

            Console.Write("  Distancia (km)  : ");
            int.TryParse(Console.ReadLine()?.Trim(), out int km);

            envio = new EnvioTerrestre(
                numeroGuia, DateTime.Now, origen, destino, "Pendiente",
                paquetes, categoria, remitente, destinatario,
                km, placa, ruta
            );
        }

        else if (tipo == "2")
        {
            Console.WriteLine("\n=== Datos del Envío Marítimo ===");

            Console.Write("  Nombre del barco  : ");
            string barco = Console.ReadLine()?.Trim();

            Console.Write("  Puerto de origen  : ");
            string puertoOrigen = Console.ReadLine()?.Trim();

            Console.Write("  Puerto de destino : ");
            string puertoDestino = Console.ReadLine()?.Trim();

            Console.Write("  Días de navegación: ");
            int.TryParse(Console.ReadLine()?.Trim(), out int dias);

            envio = new EnvioMaritimo(
                numeroGuia, DateTime.Now, origen, destino, "Pendiente",
                paquetes, categoria, remitente, destinatario,
                barco, puertoOrigen, puertoDestino, dias
            );
        }

        else
        {
            Console.WriteLine("\n===Datos del Envío Aéreo ===");

            Console.Write("  Número de vuelo      : ");
            string vuelo = Console.ReadLine()?.Trim();

            Console.Write("  Aeropuerto de origen : ");
            string aerOrigen = Console.ReadLine()?.Trim();

            Console.Write("  Aeropuerto de destino: ");
            string aerDestino = Console.ReadLine()?.Trim();

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
        string guia = Console.ReadLine()?.Trim();

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
        string guia = Console.ReadLine()?.Trim();

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
        string remitente = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(remitente)) envio.Remitente = remitente;

        Console.Write($"  Destinatario [{envio.Destinatario}]: ");
        string destinatario = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destinatario)) envio.Destinatario = destinatario;

        Console.Write($"  Origen [{envio.Origen}]: ");
        string origen = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(origen)) envio.Origen = origen;

        Console.Write($"  Destino [{envio.Destino}]: ");
        string destino = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destino)) envio.Destino = destino;

        Console.Write($"  Categoria [{envio.CategoriaEnvio}]: ");
        string categoria = Console.ReadLine()?.Trim();
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
        string guia = Console.ReadLine()?.Trim();

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