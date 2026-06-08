// Program.cs
// Conecta las 3 capas: crea el Repository, se lo pasa al Service, y el Service al GestorEnvios.
// Si mañana se cambia a XML, solo cambia la línea del Repository — nada más.

IEnvioRepository repository = new EnvioRepositoryMemoria();
EnvioService service = new EnvioService(repository);
GestorEnvios gestor = new GestorEnvios(service);

bool salir = false;
while (!salir)
{
    MostrarMenu();
    string opcion = Console.ReadLine()?.Trim();

    switch (opcion)
    {
        case "1": gestor.RegistrarEnvio(); Pausa(); break;
        case "2": gestor.MostrarEnvios(); Pausa(); break;
        case "3": gestor.BuscarEnvio(); Pausa(); break;
        case "4": gestor.FiltrarEnvios(); Pausa(); break;
        case "5": gestor.OrdenarEnvios(); Pausa(); break;
        case "6": gestor.ModificarEnvio(); Pausa(); break;
        case "7": gestor.EliminarEnvio(); Pausa(); break;
        case "0":
            salir = true;
            Console.WriteLine("\n  Hasta luego.\n");
            break;
        default:
            Console.WriteLine("\n  Opción no válida.");
            Pausa();
            break;
    }
}

void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════════════╗");
    Console.WriteLine("║      SISTEMA DE GESTIÓN DE ENVÍOS            ║");
    Console.WriteLine("╠══════════════════════════════════════════════╣");
    Console.WriteLine($"║  Envíos registrados: {gestor.ContarEnvios(),-25}║");
    Console.WriteLine("╠══════════════════════════════════════════════╣");
    Console.WriteLine("║  1. Registrar envío                          ║");
    Console.WriteLine("║  2. Mostrar todos los envíos                 ║");
    Console.WriteLine("║  3. Buscar envío por número de guía          ║");
    Console.WriteLine("║  4. Filtrar envíos                           ║");
    Console.WriteLine("║  5. Ordenar envíos                           ║");
    Console.WriteLine("║  6. Modificar envío                          ║");
    Console.WriteLine("║  7. Eliminar envío                           ║");
    Console.WriteLine("║  0. Salir                                    ║");
    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.Write("\n  Seleccione una opción: ");
}

void Pausa()
{
    Console.Write("\n  Presione Enter para continuar...");
    Console.ReadLine();
}