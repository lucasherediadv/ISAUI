using System;

public class MultiplosVector
{
    public static void Main(string[] args)
    {
        // Solicitar el numero al usuario
        Console.Write("Por favor, ingresa un numero entero: ");

        // Convertir la entrada de texto a un numero entero
        int numero;
        bool esNumeroValido = int.TryParse(Console.ReadLine(), out numero);

        // Verificar si la entrada es un numero valido
        if (!esNumeroValido)
        {
            Console.WriteLine("Entrada no valida. Por favor, ingresa un numero entero.");
            return; // Salir del programa si la entrada no es un numero
        }

        // Crea el vector de 10 elementos
        int[] multiplos = new int[10];

        // Calcular los primeros diez multiplos
        for (int i = 0; i < 10; i++)
        {
            multiplos[i] = numero * (i + 1);
        }

        // Mostrar el resultado
        Console.WriteLine($"Los primeros 10 multiplos de {numero} son:");

        for (int i = 0; i < multiplos.Length; i++)
        {
            Console.WriteLine(multiplos[i]);
        }
    }
}
