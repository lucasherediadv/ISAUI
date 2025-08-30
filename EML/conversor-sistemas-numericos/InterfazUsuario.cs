// InterfazUsuario.cs
// Esta clase estática se encarga de toda la interacción con el usuario.
// Maneja la entrada y salida de datos a través de la consola.

using System;

public static class InterfazUsuario
{
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
            Console.WriteLine($"\nSeleccione el sistema de {tipo}: ");

            // Define las opciones de sistemas numéricos disponibles.
            var opciones = new (SistemaNumerico sistema, string nombre)[]
            {
                (SistemaNumerico.Decimal, "Decimal"),
                (SistemaNumerico.Binario, "Binario"),
                (SistemaNumerico.Octal, "Octal"),
                (SistemaNumerico.Hexadecimal, "Hexadecimal")
            };

            for (int i = 0; i < opciones.Length; i++)
            {
                // Muestra la opción solo si no es el sistema excluido.
                if (opciones[i].sistema != sistemaExcluido)
                {
                    Console.WriteLine($"{opciones[i].sistema.GetHashCode()}) {opciones[i].nombre}");
                }
            }

            Console.WriteLine("\n(Escriba 'salir' para finalizar la ejecución del programa)");
            Console.Write("\n> ");

            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                MostrarError("La entrada no puede estar vacía.");
                continue;
            }

            string entradaProcesada = entrada.Trim().ToLower();

            if (entradaProcesada == "salir")
            {
                throw new OperationCanceledException();
            }

            if (int.TryParse(entradaProcesada, out int opcion))
            {
                // Valida que la opción esté en el rango correcto y no sea el sistema excluido.
                if (opcion >= 1 && opcion <= 4 && (SistemaNumerico)opcion != sistemaExcluido)
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
            Console.Write($"Ingrese el número en {sistema}\n-> ");
            string? numeroStr = Console.ReadLine()?.Trim();

            // Valida si la entrada es nula o vacía.
            if (string.IsNullOrEmpty(numeroStr))
            {
                MostrarError("La entrada no puede estar vacía.");
                continue;
            }

            // Intenta validar el número con la lógica del ConversorNumerico.
            try
            {
                ConversorNumerico.ValidarNumeroParaBase(numeroStr, sistema);
                return numeroStr; // Devuelve el número si la validación es exitosa.
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
            catch (FormatException ex)
            {
                MostrarError(ex.Message);
            }
        }
    }

    /// <summary>
    /// Muestra el resultado de la conversión en la consola.
    /// </summary>
    /// <param name="resultado">La cadena de texto que contiene el resultado a mostrar.</param>
    public static void MostrarResultado(string resultado)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nResultado: {resultado}\n");
        Console.ResetColor();
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
            Console.WriteLine("\n¿Desea realizar otra conversión? (sí/no)");
            Console.Write("> ");
            string? respuesta = Console.ReadLine()?.Trim().ToLower();

            if (respuesta == "si" || respuesta == "s" || respuesta == "sí")
            {
                return true;
            }
            else if (respuesta == "no" || respuesta == "n")
            {
                Console.WriteLine("Saliendo del programa. ¡Hasta pronto!");
                return false;
            }
            else
            {
                MostrarError("Respuesta no válida. Por favor, escriba 'sí' o 'no'.");
            }
        }
    }
}
