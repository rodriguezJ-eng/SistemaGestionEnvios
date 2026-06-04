// Program.cs - Solo maneja el menú y delega todo al GestorEnvios
GestorEnvios gestor = new GestorEnvios("envios.xml");

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
        case "4": gestor.ModificarEnvio(); Pausa(); break;
        case "5": gestor.EliminarEnvio(); Pausa(); break;
        case "0":
            salir = true;
            Console.WriteLine("\n  Hasta luego.\n");
            break;
        default:
            Console.WriteLine("\n Opción no válida.");
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
    Console.WriteLine($"║  Envíos registrados                          ║");
    Console.WriteLine("╠══════════════════════════════════════════════╣");
    Console.WriteLine("║  1. Registrar envío                          ║");
    Console.WriteLine("║  2. Mostrar todos los envíos                 ║");
    Console.WriteLine("║  3. Buscar envío                             ║");
    Console.WriteLine("║  4. Modificar envío                          ║");
    Console.WriteLine("║  5. Eliminar envío                           ║");
    Console.WriteLine("║  0. Salir                                    ║");
    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.Write("\n  Seleccione una opción: ");
}

void Pausa()
{
    Console.Write("\n  Presione Enter para continuar...");
    Console.ReadLine();
}