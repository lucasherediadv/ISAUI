using System;

public class SumaVectores
{
    public static void Main(string[] args)
    {
        // Inicializa una instancia de la clase Random para generar
        // numeros aleatorios
        Random generadorAleatorio = new Random();


        // Declara los tres vectores de 10 elementos cada uno
        int[] vector1 = new int[10];
        int[] vector2 = new int[10];
        int[] vectorSuma = new int[10];

        // Llena los dos primeros vecotres con numeros aleatorios
        // y calcula la suma
        for (int i = 0; i < 10; i++)
        {
            // Genera un numero aleatorio entre 1 y 100
            vector1[i] = generadorAleatorio.Next(1, 101);
            vector2[i] = generadorAleatorio.Next(1, 101);

            // Calcula la suma de los elementos correspondientes
            vectorSuma[i] = vector1[i] + vector2[i];
        }

        // Imprime los tres vectores
        Console.WriteLine("--- Vectores generados y su suma ---");

        // Imprime el primer vector
        Console.WriteLine("\nVector 1:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Elemento {i}: {vector1[i]}");
        }

        // Imprime el segundo vector
        Console.WriteLine("\nVector 2:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Elemento {i}: {vector2[i]}");
        }

        // Imprime el vector de la suma
        Console.WriteLine("\nVector suma:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Elemento {i}: {vectorSuma[i]}");
        }

        Console.Write("\n");
    }
}
