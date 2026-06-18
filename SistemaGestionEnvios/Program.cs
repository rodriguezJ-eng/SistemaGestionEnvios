// Program.cs
// Conecta las 3 capas: crea el Repository, se lo pasa al Service, y el Service al GestorEnvios.
// Si mañana se cambia a XML, solo cambia la línea del Repository — nada más.

// Instancia la capa de persistencia de datos utilizando el patrón Repository.
// Se inicializa con la implementación XML apuntando al archivo físico "envios.xml".
IEnvioRepository repository = new EnvioRepositoryXml("envios.xml");

// Instancia la capa de lógica de negocio (Service), inyectándole el repositorio.
// Esto permite que el servicio gestione las reglas de negocio independientemente de dónde se guarden los datos.
EnvioService service = new EnvioService(repository);

// Instancia el controlador de la interfaz de usuario (GestorEnvios), inyectándole el servicio.
// El gestor actúa como puente directo entre las vistas de la consola y la lógica de negocio.
GestorEnvios gestor = new GestorEnvios(service);

bool salir = false;
while (!salir)
{
    UI_Sistema.UI_MostrarMenu(gestor);
    string opcion = Console.ReadLine()?.Trim();

    switch (opcion)
    {
        case "1": gestor.RegistrarEnvio(); UI_Sistema.UI_Pausa(); break;
        case "2": gestor.MostrarEnvios(); UI_Sistema.UI_Pausa(); break;
        case "3": gestor.BuscarEnvio(); UI_Sistema.UI_Pausa(); break;
        case "4": gestor.FiltrarEnvios(); UI_Sistema.UI_Pausa(); break;
        case "5": gestor.OrdenarEnvios(); UI_Sistema.UI_Pausa(); break;
        case "6": gestor.ModificarEnvio(); UI_Sistema.UI_Pausa(); break;
        case "7": gestor.EliminarEnvio(); UI_Sistema.UI_Pausa(); break;
        case "0":
            salir = true;
            UI_Sistema.Despedir();
            break;
        default:
            UI_Alerta.MostrarAdvertencia("Opción no válida.");
            break;
    }
}