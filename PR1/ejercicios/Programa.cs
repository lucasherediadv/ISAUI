using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Declarar el vector de nombres
        string[] nombres = { "Ana", "Luis", "Carlos", "María", "Pedro", "Sofía", "Jorge", "Lucía", "Elena", "Diego" };
        int numeroDeAlumnos = nombres.Length;

        // Declara los vectores para las calificaciones
        double[] parcial1 = new double[numeroDeAlumnos];
        double[] parcial2 = new double[numeroDeAlumnos];
        double[] recuperatorio = new double[numeroDeAlumnos];
        double[] notaFinal = new double[numeroDeAlumnos];

        // Pedir calificaciones al usuario
        for (int i = 0; i < numeroDeAlumnos; i++)
        {
            Console.WriteLine($"\n--- Ingresar calificaciones para {nombres[i]} ---");

            Console.Write("Ingrese la nota del Parcial 1: ");
            parcial1[i] = Convert.ToDouble(Console.ReadLine());

            Console.Write("Ingrese la nota del Parcial 2: ");
            parcial2[i] = Convert.ToDouble(Console.ReadLine());

            // Pedir nota de recuperatorio si es necesario
            if (parcial1[i] < 4 || parcial2[i] < 4)
            {
                Console.Write("Ingrese la nota del Recuperatorio: ");
                recuperatorio[i] = Convert.ToDouble(Console.ReadLine());
            }
            else
            {
                recuperatorio[i] = -1; // Usamos -1 para indicar que no hay recuperatorio
            }
        }

        // Calcular nota final y clasificar a los alumnos
        int promocionados = 0;
        int regulares = 0;
        int libres = 0;

        List<string> alumnosPromocionados = new List<string>();
        List<string> alumnosRegulares = new List<string>();
        List<string> alumnosLibres = new List<string>();

        for (int i = 0; i < numeroDeAlumnos; i++)
        {
            // Lógica para la nota final
            if (recuperatorio[i] != -1)
            {
                // Si hay recuperatorio, la nota más baja entre Parcial 1, Parcial 2 y Recuperatorio se descarta
                // y se promedian las dos notas más altas.
                double[] notas = { parcial1[i], parcial2[i], recuperatorio[i] };
                Array.Sort(notas);
                notaFinal[i] = (notas[1] + notas[2]) / 2;
            }
            else
            {
                // Si no hay recuperatorio, se promedian los dos parciales
                notaFinal[i] = (parcial1[i] + parcial2[i]) / 2;
            }

            // Clasificar al alumno según la nota final
            if (notaFinal[i] >= 7)
            {
                promocionados++;
                alumnosPromocionados.Add(nombres[i]);
            }
            else if (notaFinal[i] >= 1 && notaFinal[i] < 7)
            {
                regulares++;
                alumnosRegulares.Add(nombres[i]);
            }
            else
            {
                libres++;
                alumnosLibres.Add(nombres[i]);
            }
        }

        // Imprimir los resultados
        Console.WriteLine("\n========================================");
        Console.WriteLine("        LISTADO DE RESULTADOS");
        Console.WriteLine("========================================");

        // Imprimir listado de alumnos
        Console.WriteLine("\n--- Alumnos Promocionados ---");
        foreach (string alumno in alumnosPromocionados)
        {
            Console.WriteLine(alumno);
        }

        Console.WriteLine("\n--- Alumnos Regulares ---");
        foreach (string alumno in alumnosRegulares)
        {
            Console.WriteLine(alumno);
        }

        Console.WriteLine("\n--- Alumnos Libres ---");
        foreach (string alumno in alumnosLibres)
        {
            Console.WriteLine(alumno);
        }

        // Imprimir conteo de alumnos
        Console.WriteLine("\n--- Conteo de Alumnos ---");
        Console.WriteLine($"Total de alumnos promocionados: {promocionados}");
        Console.WriteLine($"Total de alumnos regulares: {regulares}");
        Console.WriteLine($"Total de alumnos libres: {libres}");

        // Calcular y imprimir porcentajes
        double porcentajePromocionados = (double)promocionados / numeroDeAlumnos * 100;
        double porcentajeRegulares = (double)regulares / numeroDeAlumnos * 100;
        double porcentajeLibres = (double)libres / numeroDeAlumnos * 100;

        Console.WriteLine("\n--- Porcentajes ---");
        Console.WriteLine($"Porcentaje de promocionados: {porcentajePromocionados:F2}%");
        Console.WriteLine($"Porcentaje de regulares: {porcentajeRegulares:F2}%");
        Console.WriteLine($"Porcentaje de libres: {porcentajeLibres:F2}%");
    }
}