using System;

public class AnalisisVectorAleatorio
{
    public static void Main(string[] args)
    {
        // Crea una instancia de la clase Random para generar numeros
        // aleatorios
        Random generadorAleatorio = new Random();

        // Genera un vector de veinte elementos de numero aleatorios
        int[] vectorNumeros = new int[20];

        // Inicializacion de variables para el calculo del mayor, menor
        // y la cuenta.
        vectorNumeros[0] = generadorAleatorio.Next(1, 21);
        int mayorNumero = vectorNumeros[0];
        int menorNumero = vectorNumeros[0];
        int menoresA5 = 0; // Contador para los numeros menores a 5

        // Recorre el vecotr desde el segundo elemento (indice 1) para
        // llenarlo
        for (int i = 1; i < 20; i++)
        {
            // Genera un numero aleatorio entre 1 y 20 (el 21 no se
            // incluye)
            vectorNumeros[i] = generadorAleatorio.Next(1, 21);

            // a) y b) Enctonrar el mayor y el menro numero de la serie
            if (vectorNumeros[i] > mayorNumero)
            {
                mayorNumero = vectorNumeros[i];
            }

            if (vectorNumeros[i] < menorNumero)
            {
                menorNumero = vectorNumeros[i];
            }
        }

        // c) Cuenta cuantos numeros son menores a 5.
        for (int i = 0; i < 20; i++)
        {
            if (vectorNumeros[i] < 5)
            {
                menoresA5++;
            }
        }

        // Calcula el porcentaje de los numeros menores a 5
        double porcentajeMenoresA5 = (double)menoresA5 / 20 * 100;

        // Imprimir los resultados
        Console.WriteLine("--- Resultados ---");
        Console.WriteLine($"a) El mayor número de la serie es: {mayorNumero}");
        Console.WriteLine($"b) El menor número de la serie es: {menorNumero}");
        Console.WriteLine($"c) El porcentaje de números menores a 5 es: {porcentajeMenoresA5:F2}%");

        // Imprime todos los numeros en el formato solicitado
        Console.Write("d) Numeros generados: ");
        for (int i = 0; i < 20; i++)
        {
            Console.Write(vectorNumeros[i]);

            // Agrega el guion solo si no es el ultimo elemento
            if (i < 19)
            {
                Console.Write(" - ");
            }

        }
        Console.WriteLine(); // Salto de linea al final para dejar el cursor limpio
    }
}
