// InterfazUsuario.cs
// Esta clase estática se encarga de toda la interacción con el usuario.
// Maneja la entrada y salida de datos a través de la consola.

using System;
using System.Linq;

public static class InterfazUsuario
{
    // Variable para controlar si es la primera vez que se muestra el mensaje de salir.
    private static bool _esPrimeraEjecucion = true;

    /// <summary>
    /// Permite al usuario seleccionar un sistema numérico de una lista.
    /// </summary>
    /// <param name="tipo">Una cadena que indica si se selecciona el sistema de 'entrada' o 'salida'.</param>
    /// <param name="sistemaExcluido">Un sistema numérico opcional para excluir de la lista de opciones.</param>
    /// <returns>El sistema numérico seleccionado por el usuario.</returns>
    public static SistemaNumerico SeleccionarSistema(string tipo, SistemaNumerico? sistemaExcluido = null)
    {
        while (true)
        {
            Console.WriteLine($"\nSeleccione el sistema de {tipo} (indique una opción):\n");

            // Define las opciones de sistemas numéricos disponibles.
            var opciones = Enum.GetValues(typeof(SistemaNumerico))
                               .Cast<SistemaNumerico>()
                               .Select(s => (sistema: s, nombre: s.ToString()))
                               .Where(o => o.sistema != sistemaExcluido)
                               .ToList();

            for (int i = 0; i < opciones.Count; i++)
            {
                Console.WriteLine($"{opciones[i].sistema.GetHashCode()}) {opciones[i].nombre}");
            }

            // Muestra el mensaje de 'salir' solo la primera vez.
            if (_esPrimeraEjecucion)
            {
                Console.WriteLine("\nEscriba 'salir' para finalizar la ejecución del programa");
                _esPrimeraEjecucion = false;
            }

            string? entradaProcesada = PedirEntrada();

            if (string.IsNullOrWhiteSpace(entradaProcesada))
            {
                MostrarError("La entrada no puede estar vacía.");
                continue;
            }

            if (entradaProcesada.ToLower() == "salir")
            {
                throw new OperationCanceledException();
            }

            if (int.TryParse(entradaProcesada, out int opcion) && Enum.IsDefined(typeof(SistemaNumerico), opcion))
            {
                if (opciones.Any(o => o.sistema == (SistemaNumerico)opcion))
                {
                    return (SistemaNumerico)opcion;
                }
            }
            MostrarError("Opción inválida. Por favor, elija un número de la lista.");
        }
    }

    /// <summary>
    /// Solicita al usuario que ingrese un número para la conversión.
    /// </summary>
    /// <param name="sistema">El sistema numérico en el que se debe ingresar el número.</param>
    /// <returns>La cadena de texto que contiene el número ingresado por el usuario.</returns>
    public static string PedirNumero(SistemaNumerico sistema)
    {
        while (true)
        {
            Console.Write($"\nIngrese el número en {sistema.ToString().ToLower()}:\n");
            string? numeroStr = PedirEntrada();

            // Llama al método de validación mejorado.
            if (ConversorNumerico.ValidarNumeroParaBase(numeroStr, sistema, out string mensajeError))
            {
                return numeroStr;
            }
            else
            {
                MostrarError(mensajeError);
            }
        }
    }

    /// <summary>
    /// Muestra el resultado de la conversión en la consola.
    /// </summary>
    /// <param name="numeroOriginal">El número que el usuario ingresó.</param>
    /// <param name="origen">El sistema de origen de la conversión.</param>
    /// <param name="destino">El sistema de destino de la conversión.</param>
    /// <param name="resultado">La cadena de texto que contiene el resultado a mostrar.</param>
    public static void MostrarResultado(string numeroOriginal, SistemaNumerico origen, SistemaNumerico destino, string resultado)
    {
        Console.WriteLine();
        Console.Write($"El número {origen.ToString().ToLower()} '{numeroOriginal}' se representa como '");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(resultado);
        Console.ResetColor();
        Console.WriteLine($"' en sistema {destino.ToString().ToLower()}.\n");
    }

    /// <summary>
    /// Muestra un mensaje de error en la consola.
    /// </summary>
    /// <param name="mensaje">La cadena de texto que contiene el mensaje de error.</param>
    public static void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nError: {mensaje}\n");
        Console.ResetColor();
    }

    /// <summary>
    /// Pregunta al usuario si desea realizar otra conversión y valida la respuesta.
    /// </summary>
    /// <returns>Verdadero si el usuario quiere continuar; de lo contrario, falso.</returns>
    public static bool ContinuarPrograma()
    {
        while (true)
        {
            Console.Write("¿Desea realizar otra conversión? (sí/no):\n");
            string? respuesta = PedirEntrada();

            if (respuesta?.ToLower() == "si" || respuesta?.ToLower() == "s" || respuesta?.ToLower() == "sí")
            {
                return true;
            }
            else if (respuesta?.ToLower() == "no" || respuesta?.ToLower() == "n")
            {
                Console.WriteLine("\nSaliendo del programa. ¡Gracias por usar el conversor!\n");
                return false;
            }
            else
            {
                MostrarError("Respuesta inválida. Por favor, escriba 'sí' o 'no'.");
            }
        }
    }

    /// <summary>
    /// Método auxiliar para estandarizar la solicitud de entrada del usuario.
    /// </summary>
    /// <returns>La entrada del usuario como una cadena de texto.</returns>
    private static string PedirEntrada()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write("\n>>> ");
        Console.ResetColor();
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }
}
