/// <summary>
/// Controla los formularios visuales secuenciales necesarios para capturar y construir nuevos objetos de tipo Envio.
/// </summary>
public static class UI_RegistrarEnvio
{
    /// <summary>
    /// Ofrece la lista de selección para instanciar la clase derivada correspondiente en memoria.
    /// </summary>
    public static void UI_RegistrarNuevoEnvio()
    {
        Console.Clear();
        Console.WriteLine("══ REGISTRAR NUEVO ENVÍO ══\n");

        Console.WriteLine("  Tipo de envío:");
        Console.WriteLine("  1. Terrestre");
        Console.WriteLine("  2. Marítimo");
        Console.WriteLine("  3. Aéreo");
        Console.Write("  Seleccione: ");
    }

    /// <summary>
    /// Dibuja la cabecera para los datos globales comunes del contrato base de transportes.
    /// </summary>
    public static void UI_DatosEnvioBasicos()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVIO ══\n");
    }

    /// <summary>
    /// Pide al usuario definir la dimensión del bucle contenedor de paquetes asociados.
    /// </summary>
    public static void UI_CuantosPaquetes()
    {
        Console.Write("\n  ¿Cuántos paquetes incluye el envío?: ");
    }

    /// <summary>
    /// Inicializa la cabecera del formulario intermedio de recolección de métricas de bultos particulares.
    /// </summary>
    /// <param name="numero">Índice secuencial relativo del paquete actual.</param>
    public static void UI_DatosPaqueteDelEnvio(int numero)
    {
        Console.Clear();
        Console.WriteLine($"══ DATOS DEL PAQUETE #{numero} ══\n");
    }

    /// <summary>
    /// Limpia la pantalla para dar paso al formulario exclusivo de atributos viales.
    /// </summary>
    public static void UI_DatosEnvioTerrestre()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO TERRESTRE ══\n");
    }

    /// <summary>
    /// Limpia la pantalla para dar paso al formulario exclusivo de atributos de buques mercantes.
    /// </summary>
    public static void UI_DatosEnvioMaritimo()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO MARÍTIMO ══\n");
    }

    /// <summary>
    /// Imprime el tipo o categoría resultante de evaluar las dimensiones físicas de un bulto.
    /// </summary>
    /// <param name="tipoPaquete">Descripción de tamaño (Pequeño, Mediano, Grande).</param>
    public static void UI_DatoDelTipoDePaquete(string tipoPaquete)
    {
        Console.WriteLine($"  Tipo calculado: {tipoPaquete}");
    }

    /// <summary>
    /// Limpia la pantalla para dar paso al formulario exclusivo de atributos aeronáuticos.
    /// </summary>
    public static void UI_DatosEnvioAereo()
    {
        Console.Clear();
        Console.WriteLine("══ DATOS DEL ENVÍO AÉREO ══\n");
    }

    /// <summary>
    /// Muestra la previsualización final del objeto recién guardado e imprime la alerta de éxito con el código de seguimiento asignado.
    /// </summary>
    /// <param name="envio">Objeto recién persistido.</param>
    public static void UI_DatosGeneralesEnvio(Envio envio)
    {
        Console.Clear();
        Console.WriteLine("══ DATOS GENERALES DEL ENVÍO ══\n");
        envio.MostrarInformacionEnvio();
        UI_Sistema.UI_Pausa();

        // Emisión de banner final notificando la clave única generada por la capa de lógica
        UI_Alerta.MostrarExito($"Envío registrado. Número de guía asignado: {envio.NumeroGuia}", true, false);
    }
}