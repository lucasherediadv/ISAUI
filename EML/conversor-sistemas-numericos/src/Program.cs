// Program.cs
// Esta es la clase principal que inicia la ejecución del programa.
// Se encarga de coordinar el flujo de la aplicación y manejar los errores principales.

using System;

public class Program
{
    /// <summary>
    /// Punto de entrada principal del programa.
    /// Inicia un bucle infinito para permitir múltiples conversiones hasta que el usuario decida salir.
    /// </summary>
    public static void Main()
    {
        // El bucle 'while' permite que el programa se ejecute continuamente.
        while (true)
        {
            try
            {
                // Paso 1: Selecciona el sistema numérico de origen.
                SistemaNumerico origen = InterfazUsuario.SeleccionarSistema("entrada");

                // Paso 2: Pide al usuario que ingrese el número en el sistema de origen.
                string numeroStr = InterfazUsuario.PedirNumero(origen);

                // Paso 3: Selecciona el sistema numérico de destino, excluyendo el sistema de origen.
                SistemaNumerico destino = InterfazUsuario.SeleccionarSistema("salida", origen);

                // Paso 4: Realiza la conversión del número.
                string resultado = ConversorNumerico.Convertir(numeroStr, origen, destino);

                // Paso 5: Muestra el resultado de la conversión al usuario.
                InterfazUsuario.MostrarResultado(numeroStr, origen, destino, resultado);
            }
            // Captura la excepción cuando el usuario decide salir del programa.
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nSaliendo del programa. ¡Gracias por usar el conversor!\n");
                break; // Sale del bucle 'while'.
            }
            // Captura las excepciones de argumentos inválidos o de formato.
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                InterfazUsuario.MostrarError(ex.Message);
            }

            // Pregunta al usuario si desea realizar otra conversión y sale si la respuesta es negativa.
            if (!InterfazUsuario.ContinuarPrograma())
            {
                break;
            }
        }
    }
}
