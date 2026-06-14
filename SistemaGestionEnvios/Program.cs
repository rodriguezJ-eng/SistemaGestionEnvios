// Program.cs
// Conecta las 3 capas: crea el Repository, se lo pasa al Service, y el Service al GestorEnvios.
// Si mañana se cambia a XML, solo cambia la línea del Repository — nada más.

IEnvioRepository repository = new EnvioRepository();
EnvioService service = new EnvioService(repository);
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
            Console.WriteLine("\n  Hasta luego.\n");
            break;
        default:
            Console.WriteLine("\n  Opción no válida.");
            UI_Sistema.UI_Pausa();
            break;
    }
}