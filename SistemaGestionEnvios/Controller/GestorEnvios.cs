using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Controlador principal de la interfaz de usuario para la gestión de envíos.
/// Controla los flujos de captura de datos por consola, invoca las operaciones del servicio de negocio
/// y coordina la visualización de resultados, alertas y errores al usuario.
/// </summary>
public class GestorEnvios
{
    private readonly EnvioService _service;

    /// <summary>
    /// Inicializa una nueva instancia del gestor inyectando el servicio de negocio correspondiente.
    /// </summary>
    /// <param name="service">Instancia de la capa de servicios logísticos.</param>
    public GestorEnvios(EnvioService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Conduce el flujo interactivo por consola para registrar un nuevo envío.
    /// Captura datos básicos, clasifica por tipo de transporte (Terrestre, Marítimo o Aéreo),
    /// solicita la carga útil (paquetes) e invoca la persistencia del servicio.
    /// </summary>
    public void RegistrarEnvio() // listo en gestor
    {
        UI_RegistrarEnvio.UI_RegistrarNuevoEnvio();
        string? tipo = Console.ReadLine()?.Trim();

        // Validación inicial del tipo de transporte seleccionado
        if (tipo != "1" && tipo != "2" && tipo != "3")
        {
            UI_Alerta.MostrarAdvertencia("Tipo no válido.", true, false);
            return;
        }

        // Captura y validación de datos comunes del envío
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

        // Carga secuencial de la lista de bultos a transportar
        List<Paquete> paquetes = LeerPaquetes(cantidadPaquetes);
        Envio envio = null;

        try
        {
            // Segmentación del flujo y captura de datos específicos según la modalidad logística
            if (tipo == "1")
            {
                UI_RegistrarEnvio.UI_DatosEnvioTerrestre();
                string? placa = Validador.LeerPlaca();
                string direccion = Validador.LeerTexto("  Dirección       : ", 3, 100);
                int km = Validador.LeerEnteroPositivo("  Distancia (km)  : ");

                envio = _service.RegistrarTerrestre(remitente, destinatario, origen, destino, categoria, paquetes, km, placa, direccion);
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
            // resumen general tras la inserción exitosa
            UI_RegistrarEnvio.UI_DatosGeneralesEnvio(envio);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al registrar el envío: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Consulta el listado global de registros al servicio y coordina su despliegue  en la consola.
    /// Muestra una advertencia controlada si el repositorio se encuentra vacío.
    /// </summary>
    public void MostrarEnvios() 
    {
        try
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
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al mostrar los envíos: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Solicita al usuario un identificador único por consola y despliega la información detallada 
    /// de la guía localizada por el servicio de negocio.
    /// </summary>
    public void BuscarEnvio()
    {
        try
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
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al buscar el envío: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Despliega el menú de filtrado y procesa la búsqueda  mediante 
    /// expresiones Lambda enviadas directamente a la capa de abstracción de datos.
    /// </summary>
    public void FiltrarEnvios() //listo
    {
        try
        {
            UI_FiltrarEnvios.UI_Menu();
            string? opcion = Console.ReadLine()?.Trim();

            List<Envio> resultado = null;

            switch (opcion)
            {
                case "1":
                    UI_FiltrarEnvios.UI_OpcionBusqueda(1);
                    string? tipo = Console.ReadLine()?.Trim();
                    // Filtro polimórfico por tipo de envío (Terrestre, Marítimo o Aéreo)
                    resultado = _service.Filtrar(e => e.TipoEnvio().Equals(tipo, StringComparison.OrdinalIgnoreCase));
                    break;

                case "2":
                    UI_FiltrarEnvios.UI_OpcionBusqueda(2);
                    string? estado = Console.ReadLine()?.Trim();
                    // Filtro operacional por estado (Pendiente, En Tránsito, Entregado, Cancelado)
                    resultado = _service.Filtrar(e => e.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                    break;

                case "3":
                    UI_FiltrarEnvios.UI_OpcionBusqueda(3);
                    string? categoria = Console.ReadLine()?.Trim();
                    // Filtro geográfico por categoría de envío (Nacional o Internacional)
                    resultado = _service.Filtrar(e => e.CategoriaEnvio.Equals(categoria, StringComparison.OrdinalIgnoreCase));
                    break;

                case "4":
                    UI_FiltrarEnvios.UI_OpcionBusqueda(4);
                    string? remitente = Console.ReadLine()?.Trim();
                    // Filtro predictivo por coincidencia parcial de texto en la entidad Remitente
                    resultado = _service.Filtrar(e => e.Remitente.Contains(remitente, StringComparison.OrdinalIgnoreCase));
                    break;

                default:
                    UI_Alerta.MostrarAdvertencia("Opción no válida.", true, false);
                    return;
            }
            UI_Sistema.MostrarResultados(resultado);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al filtrar los envíos: {ex.Message}", true, false);
        }   
    }

    /// <summary>
    /// Despliega el menú de ordenamiento y devuelve la colección de datos organizada de forma secuencial,
    /// aplicando operaciones de inversión en memoria para listados de naturaleza descendente (ej. fecha y costos).
    /// </summary>
    public void OrdenarEnvios() // listo
    {
        try
        {
            UI_OrdenarEnvios.UI_Menu();
            string? opcion = Console.ReadLine()?.Trim();

            List<Envio> resultado = null;

            switch (opcion)
            {
                case "1":
                    // Ordenamiento cronológico descendente (De más reciente a más antiguo)
                    resultado = _service.Ordenar(e => e.FechaEnvio).AsEnumerable().Reverse().ToList();
                    break;
                case "2":
                    // Ordenamiento alfanumérico secuencial por Número de Guía
                    resultado = _service.Ordenar(e => e.NumeroGuia);
                    break;
                case "3":
                    // Ordenamiento alfabético por Estado Operacional
                    resultado = _service.Ordenar(e => e.Estado);
                    break;
                case "4":
                    // Ordenamiento financiero descendente (De mayor costo a menor flete)
                    resultado = _service.Ordenar(e => e.CalcularCostoTotal()).AsEnumerable().Reverse().ToList();
                    break;
                default:
                    UI_Alerta.MostrarAdvertencia("Opción no válida.", true, false);
                    return;
            }

            UI_Sistema.MostrarResultados(resultado);
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error al ordenar los envíos: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Conduce el formulario de actualización de datos de un envío.
    /// Captura las modificaciones opcionales de los campos de texto e interactúa de manera 
    /// segura con el proceso transaccional de rollback expuesto por el servicio de negocio.
    /// </summary>
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

        // Carga visual de datos actuales para servir de referencia al usuario
        UI_ModificarEnvio.MostrarDatosActuales(envio);
        UI_ModificarEnvio.TituloFormulario();

        UI_ModificarEnvio.Formulario(1, envio);
        string? remitente = Console.ReadLine()?.Trim();

        UI_ModificarEnvio.Formulario(2, envio);
        string? destinatario = Console.ReadLine()?.Trim();

        UI_ModificarEnvio.Formulario(3, envio);
        string? origen = Console.ReadLine()?.Trim();

        UI_ModificarEnvio.Formulario(4, envio);
        string? destino = Console.ReadLine()?.Trim();

        UI_ModificarEnvio.Formulario(5, envio);
        string? categoria = Console.ReadLine()?.Trim();

        try
        {
            UI_ModificarEnvio.DeseaModificarEstado();

            string? respuesta = Console.ReadLine()?.Trim().ToLower();

            if (respuesta != "s" && respuesta != "n")
            {
                UI_Alerta.MostrarError("Debe ingresar unicamente 's' o 'n'", true, false);
                return;
            }

            // Invocación segura de actualización de campos básicos de la guía
            _service.Modificar(guia, remitente, destinatario, origen, destino, categoria);

            // Actualización secundaria controlada por reglas transaccionales de cambio de estado
            if (respuesta == "s")
            {
                string nuevoEstado = UI_ModificarEnvio.LeerNuevoEstado();
                _service.ActualizarEstado(guia, nuevoEstado);
            }
                //envio.ActualizarEstado();
            
        }
        catch (Exception ex)
        {
            UI_Alerta.MostrarError($"Error: {ex.Message}", true, false);
        }
    }

    /// <summary>
    /// Conduce el flujo de remoción física o lógica de un registro.
    /// Requiere la confirmación explícita del operador en consola antes de ejecutar la eliminación permanente.
    /// </summary>
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
    /// método privado de lectura repetitiva. Se encarga exclusivamente del escaneo, validación 
    /// y cálculo automatizado de propiedades físicas y volumétricas para cada bulto ingresado.
    /// </summary>
    /// <param name="cantidad">El número total de paquetes asociados al envío.</param>
    /// <returns>Una colección genérica tipo List cargada con los objetos Paquete instanciados.</returns>
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

            // Cálculo automatizado del tipo de paquete (Caja, Sobre, Bulto Grande, etc.) por dimensiones
            string tipoPaquete = Validador.CalcularTipoPaquete(peso, alto, ancho, largo);
            UI_RegistrarEnvio.UI_DatoDelTipoDePaquete(tipoPaquete);

            // Instanciación directa del modelo de datos de infraestructura
            Paquete paquete = new Paquete( codigoPaquete,contenido,esFragil,valorDeclarado,tipoPaquete,peso,largo,alto,ancho);
            paquetes.Add(paquete);
        }

        return paquetes;
    }

    /// <summary>
    /// Expone de manera directa el conteo global de registros de envíos,
    /// facilitando la actualización de encabezados de menú en Program.cs.
    /// </summary>
    /// <returns>La cantidad total de registros almacenados de tipo entero (int).</returns>
    public int ContarEnvios() => _service.ContarEnvios();
}