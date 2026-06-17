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

    /// <summary>
    /// Método para realizar un nuevo envío.
    /// </summary>
    public void RegistrarEnvio() // listo en gestor
    {
        UI_RegistrarEnvio.UI_RegistrarNuevoEnvio();
        string? tipo = Console.ReadLine()?.Trim();

        if (tipo != "1" && tipo != "2" && tipo != "3")
        {
            UI_Alerta.MostrarAdvertencia("Tipo no válido.", true, false);
            return;
        }

        UI_RegistrarEnvio.UI_DatosEnvioBasicos();
        string categoria = Validador.LeerCategoria();
        string remitente = Validador.LeerNombre("  Remitente      : ");
        string destinatario = Validador.LeerNombre("  Destinatario   : ");
        string origen = Validador.LeerLugar("  Origen         : ");
        string destino = Validador.LeerLugar("  Destino        : ");

        UI_RegistrarEnvio.UI_CuantosPaquetes();
        int.TryParse(Console.ReadLine()?.Trim(), out int cantidadPaquetes);

        if (cantidadPaquetes <= 0)
        {
            UI_Alerta.MostrarAdvertencia("Cantidad no válida.", true, false);
            return;
        }

        List<Paquete> paquetes = LeerPaquetes(cantidadPaquetes);

        Envio envio = null;

        try
        {
            if (tipo == "1")
            {
                UI_RegistrarEnvio.UI_DatosEnvioTerrestre();
                string? placa = Validador.LeerPlaca();
                string ruta = Validador.LeerTexto("  Ruta            : ", 3, 100);
                int km = Validador.LeerEnteroPositivo("  Distancia (km)  : ");

                envio = _service.RegistrarTerrestre(remitente, destinatario, origen, destino, categoria, paquetes, km, placa, ruta);
            }
            else if (tipo == "2")
            {
                UI_RegistrarEnvio.UI_DatosEnvioMaritimo();
                string barco = Validador.LeerNombre("  Nombre del barco  : ");
                string puertoOrigen = Validador.LeerLugar("  Puerto de origen  : ");
                string puertoDestino = Validador.LeerLugar("  Puerto de destino : ");
                int dias = Validador.LeerEnteroPositivo("  Días de navegación: ");

                envio = _service.RegistrarMaritimo(remitente, destinatario, origen, destino, categoria, paquetes, barco, puertoOrigen, puertoDestino, dias);
            }
            else
            {
                UI_RegistrarEnvio.UI_DatosEnvioAereo();
                string vuelo = Validador.LeerTexto("  Número de vuelo      : ", 3, 15);
                string aerOrigen = Validador.LeerLugar("  Aeropuerto de origen :  ");
                string aerDestino = Validador.LeerLugar("  Aeropuerto de destino:  ");
                envio = _service.RegistrarAereo(remitente, destinatario, origen, destino, categoria, paquetes, vuelo, aerOrigen, aerDestino);
            }
            UI_RegistrarEnvio.UI_DatosGeneralesEnvio(envio);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al registrar el envío: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Método para Mostrar lo Envíos en la consola 
    /// </summary>
    public void MostrarEnvios() // listo en gestor
    {
        UI_MostrarEnvios.UI_MostrarEnviosTitulo();

        List<Envio> envios = _service.ObtenerTodos();

        if (envios.Count == 0)
        {
            UI_Alerta.MostrarAdvertencia("No hay envíos registrados.", true, false);
            return;
        }

        UI_MostrarEnvios.UI_MostrarEnviosListados(envios);
    }

    /// <summary>
    /// Busca un Envío por su número de guia
    /// </summary>    
    public void BuscarEnvio()
    {
        UI_BuscarEnvio.UI_Titulo();
        string? guia = Console.ReadLine()?.Trim();

        Envio encontrado = _service.BuscarPorGuia(guia);

        if (encontrado == null)
        {
            UI_Alerta.MostrarAdvertencia("No se encontró ningún envío con ese número de guía.", true, false);
            return;
        }

        UI_BuscarEnvio.UI_EnvioEncontrado(encontrado);
    }

    /// <summary>
    /// Filtra los envios en categorias como 
    /// su Tipo: (Terrestre / Maritimo / Aereo)
    /// El estado (Pendiente,Cancelado,Entregado)
    /// La Categoria (Nacional/Internacional)
    /// Por Remitente
    /// </summary>
    public void FiltrarEnvios() //listo
    {
        UI_FiltrarEnvios.UI_Menu();
        string? opcion = Console.ReadLine()?.Trim();

        List<Envio> resultado = null;

        switch (opcion)
        {
            case "1":
                UI_FiltrarEnvios.UI_OpcionBusqueda(1);
                string? tipo = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.TipoEnvio().Equals(tipo, StringComparison.OrdinalIgnoreCase));
                break;

            case "2":
                UI_FiltrarEnvios.UI_OpcionBusqueda(2);
                string? estado = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                break;

            case "3":
                UI_FiltrarEnvios.UI_OpcionBusqueda(3);
                string? categoria = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.CategoriaEnvio.Equals(categoria, StringComparison.OrdinalIgnoreCase));
                break;

            case "4":
                UI_FiltrarEnvios.UI_OpcionBusqueda(4);
                string? remitente = Console.ReadLine()?.Trim();
                resultado = _service.Filtrar(e => e.Remitente.Contains(remitente, StringComparison.OrdinalIgnoreCase));
                break;

            default:
                UI_Alerta.MostrarAdvertencia("Opción no válida.", true, false);
                return;
        }

        UI_Sistema.MostrarResultados(resultado);
    }

    public void OrdenarEnvios() // listo
    {
        UI_OrdenarEnvios.UI_Menu();
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
                UI_Alerta.MostrarAdvertencia("Opción no válida.", true, false);
                return;
        }

        UI_Sistema.MostrarResultados(resultado);
    }

    public void ModificarEnvio() // listo
    {
        UI_ModificarEnvio.Menu();
        string? guia = Console.ReadLine()?.Trim();

        Envio envio = _service.BuscarPorGuia(guia);

        if (envio == null)
        {
            UI_Alerta.MostrarAdvertencia("Envío no encontrado.", true, false);
            return;
        }

        UI_ModificarEnvio.MostrarDatosActuales(envio);
        UI_ModificarEnvio.TituloFormulario();

        UI_ModificarEnvio.Formulario(1, envio);
        string? remitente = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(remitente)) envio.Remitente = remitente;

        UI_ModificarEnvio.Formulario(2, envio);
        string? destinatario = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destinatario)) envio.Destinatario = destinatario;

        UI_ModificarEnvio.Formulario(3, envio);
        string? origen = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(origen)) envio.Origen = origen;

        UI_ModificarEnvio.Formulario(4, envio);
        string? destino = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(destino)) envio.Destino = destino;

        UI_ModificarEnvio.Formulario(5, envio);
        string? categoria = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(categoria)) envio.CategoriaEnvio = categoria;

        try
        {
            _service.Modificar(guia, remitente, destinatario, origen, destino, categoria);

            UI_ModificarEnvio.DeseaModificarEstado();
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {


                envio.ActualizarEstado();  // muestra submenú de estados en consola
                // el Service no necesita hacer nada más (ya es la misma referencia)
            }
            UI_Alerta.MostrarExito("  Envío modificado correctamente.", true, true);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error: {ex.Message}", true, false);
        }
    }

    public void EliminarEnvio()
    {
        UI_EliminarEnvio.Titulo();

        UI_EliminarEnvio.PedirNumeroGuia();
        string? guia = Console.ReadLine()?.Trim();

        Envio envio = _service.BuscarPorGuia(guia);

        if (envio == null)
        {
            UI_Alerta.MostrarAdvertencia("Envío no encontrado.", true, false);
            return;
        }
            
        UI_EliminarEnvio.DatosDelEnvio(envio);

        UI_EliminarEnvio.PreguntaDeSeguridadParaInseguros();
        if (Console.ReadLine()?.Trim().ToLower() != "s")
        {
            UI_Alerta.MostrarExito("  Eliminacion cancelada.", true, false);
            return;
        }

        try
        {
            _service.Eliminar(guia);
            UI_Alerta.MostrarExito("Envío eliminado exitosamente.", true, false);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error: {ex.Message}", true, false);
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
            UI_RegistrarEnvio.UI_DatosPaqueteDelEnvio(i);
            string codigoPaquete = Validador.LeerCodigoPaquete();
            string contenido = Validador.LeerTexto("  Contenido      : ", 3, 100);
            bool esFragil = Validador.LeerSiNo("  Es frágil? (s/n): ");
            decimal valorDeclarado = Validador.LeerDecimalPositivo("  Valor declarado: ");
            double peso = Validador.LeerDoublePositivo("  Peso (kg)      : ");
            double alto = Validador.LeerDoublePositivo("  Alto (cm)      : ");
            double ancho = Validador.LeerDoublePositivo("  Ancho (cm)     : ");
            double largo = Validador.LeerDoublePositivo("  Largo (cm)     : ");

            string tipoPaquete = Validador.CalcularTipoPaquete(peso, alto, ancho, largo);

            UI_RegistrarEnvio.UI_DatoDelTipoDePaquete(tipoPaquete);

            Paquete paquete = new Paquete( codigoPaquete,contenido,esFragil,valorDeclarado,tipoPaquete,peso,largo,alto,ancho);

            paquetes.Add(paquete);
        }

        return paquetes;
    }

    /// <summary>
    /// Expone el conteo de envíos al Program.cs para el encabezado del menú.
    /// </summary>
    public int ContarEnvios() => _service.ContarEnvios();
}