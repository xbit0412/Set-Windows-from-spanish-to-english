using System; // tipos basicos
using System.IO; // archivos, directorios, streams
using System.Collections.Generic; // listas, diccionarios, colas y pilas
using System.Linq; // lenguaje sql
using System.Net.Http; // peticiones http y APIS web
using System.Threading; // hilos, sincronizacion de hilos, periodicidad
using System.Threading.Tasks; // programacion asincrona y tareas
// A partir de aqui los using que no vienen por defecto en la plantilla CLI
using System.Diagnostics; // Utilizado para la ejecucion y monitorizacion de subprocesos


//Creo los metodos despues el main por organizacion
class Funcion1 // Creo la clase Funcion1
{
    internal static void Set_English() // Creo metodo interno estatico y que no devuelve valor (void) y lo llamo Set_English
    {
        // Genero en variable Cadena_comandos de tipo string es decir como texto
        /*@
        Instala paquete idioma completo ingles britanico
        Fuerza idioma ingles al usuario de forma temporal para la sesion actual
        Establece ingles predeterminado para aplicaciones actuales o futuras cuentas de usuario
        Establece el formato regional de ESPAÑA (inserte meme de Dragonite español mucho español y muy español)
        Establece que la region del sistema sea leida por las aplicaciones para España
        Establece la geolocalizacion a España para las apps
        Fuerza que los idiomas del usuario seran por preferencia 1 ingles uk y 2 español
        */
        string Cadena_comandos = @"
          Install-Language en-GB
          Set-WinUILanguageOverride -Language en-GB
          Set-SystemPreferredUILanguage -Language en-GB
          Set-Culture es-ES
          Set-WinSystemLocale es-ES
          Set-WinHomeLocation -GeoId 217
          Set-WinUserLanguageList en-GB,es-ES -Force
          ";

        try // Se usa Try que permite capturar errores (try/catch)
        {
            // Configura como se creara la ventana de powershell
            ProcessStartInfo Powershell_ventana = new ProcessStartInfo()
            {
                FileName = "powershell.exe", // Ejecuta powershell del sistema Windows, como esta puesto es asume ruta absoluta/parcial
                Arguments = $"-NoProfile -Command \"{Cadena_comandos}\"", // No se le agrega argumentos, se le pasa el string completo
                Verb = "runas", // Ask for admin (UAC) once
                UseShellExecute = true, // Permite usar runas para elevacion de privilegios
                RedirectStandardOutput = false, // Output se muestra en el propio powershell que se crea
                RedirectStandardError = false, // Errores se muestran en el propio powershell que se crea
                CreateNoWindow = false // Muestra la ventana de powershell todo el rato, permite ver el progreso
            };

            // Inicializa el proceso de elevacion
            Process? p = Process.Start(Powershell_ventana); // Si el usuario cancela la elevacion de privilegios UAC devuelve null
            if (p != null) // Si la ventana powershell creada NO es null
            {
                p.WaitForExit(); // Esperamos a que la ventana de powershell creada termine
            }
            else // Si la ventana powershell creada SI es null
            {
                Console.WriteLine("Has puesto que no al preguntar por elevacion UAC o algo ha salido mal con la ejecucion del powershell. Ejecución cancelada volviendo al menu principal."); // Muestra mensaje de error si mendrugo da a no en el UAC
                return; // Salimos del metodo que nos deberia devolver a main program
            }
        }
        catch (Exception Error_ejecucion) // Atrapa en Error_ejecucion el output del error
        {
            Console.Clear();
            Console.WriteLine("\nError ejecutando el script de PowerShell: "); // Muestro un mensaje de error @policia socorro
            Console.WriteLine(Error_ejecucion.Message); // Muestro el error en la consola del programa
            return; // Salimos del metodo que nos deberia devolver a main program
        }

        // Se asume que todos los comandos ok si se llega aqui
        Console.Clear();
        Console.WriteLine("\nDebes reiniciar Windows para que se hagan correctamente todos los cambios");
        Console.WriteLine("Ha finalizado la ejecucion, presiona cualquier tecla para volver");
        Console.ReadKey(true); // Espera a una pulsacion teclado por parte del usuario
        Console.Clear(); // Limpiamos CLI antes de ir a main
    }
}


namespace winconfigchangeproject // nombre del proyecto
{
    internal class Program // nombre del fichero .cs para main
    {
        static void Main(string[] args)
        {   
          while(true)
          {
            // Solicitamos al usuario que introduzca un input y mostramos al usuario las opciones disponibles
            Console.WriteLine("Selecciona una opcion introduciendo el numero");
            Console.WriteLine("0. Salir");
            Console.WriteLine("1. Pon Windows en ingles en-GB pero manten el formato regional de España (REQUIERE ELEVACION UAC)");
            // Introducimos en la variable Seleccion_usuario el numero introducido y lo convertimos explicitamente a int8 
            UInt32 Seleccion_usuario = Convert.ToUInt32(Console.ReadLine()); 
           
              if (Seleccion_usuario == 0)
                    {
                      return;                     
                    }
              else if (Seleccion_usuario == 1)
              {
                Funcion1.Set_English();
              }
              else
              {
                Console.Clear(); // Limpiar cli para priorizar error
                Console.WriteLine("\nNo has seleccionado el programa correcto\n"); //Mostrar el error en una nueva linea y crear una nueva al final
              }

        }
    }
}
}